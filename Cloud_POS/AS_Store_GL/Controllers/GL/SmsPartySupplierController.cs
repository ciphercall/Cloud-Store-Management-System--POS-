using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.DataAccess;
using AS_Store_GL.Models;
using AS_Store_GL.Models.ASL;
using AS_Store_GL.Models.DTO;

namespace AS_Store_GL.Controllers.GL
{
    public class SmsPartySupplierController : AppController
    {
        private Store_GL_DbContext db = new Store_GL_DbContext();

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public SmsPartySupplierController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            ViewData["HighLight_Menu_PromotionForm"] = "High Light Menu";
        }





        // GET: /SMS/
        public ActionResult Index()
        {
            WebClient chcksmsqtySndSms = new WebClient();
            Int64 LoggedCompId = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var getSMSUserNamePass = from m in db.AslCompanyDbSet where m.COMPID == LoggedCompId
                                     select new { m.SMSIDP, m.SMSPWP };
            string userName = "", usPss = "";
            foreach (var VARIABLE in getSMSUserNamePass)
            {
                userName = VARIABLE.SMSIDP;
                usPss = VARIABLE.SMSPWP;
            }

            string qty = "";
            if (userName == null || usPss == null)
            {
                qty = "0";
            }
            else
            {
                qty = chcksmsqtySndSms.DownloadString("http://66.45.237.70/balancechk.php?username=" + userName + "&password=" + usPss);
            }
            TempData["CheckSMSQuantity"] = qty;
            return View();
        }






        [HttpPost]
        public ActionResult Index(MailDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.GROUPID == null)
                {
                    ViewBag.SMS_psMessage = "Select group name filed first!";
                    return View("Index");
                }


                WebClient chcksmsqtySndSms = new WebClient();
                //string userName = "asl-sms";
                //string usPss = "asl.123SMS@3412";

                var getSMSUserNamePass = from m in db.AslCompanyDbSet
                                         where m.COMPID == model.COMPID
                                         select new { m.SMSIDP, m.SMSPWP, m.SMSSENDERNM };
                string userName = "", usPss = "", senderName = "";
                foreach (var VARIABLE in getSMSUserNamePass)
                {
                    senderName = VARIABLE.SMSSENDERNM;
                    userName = VARIABLE.SMSIDP;
                    usPss = VARIABLE.SMSPWP;
                }

                if (userName != null && usPss != null && senderName != null)
                {
                    try
                    {
                        string qty = chcksmsqtySndSms.DownloadString("http://66.45.237.70/balancechk.php?username=" + userName + "&password=" + usPss);
                        TempData["CheckSMSQuantity"] = qty;

                        WebClient cardsms = new WebClient();
                        string cashStatus = "";

                        //Current group contact list add in ASL_PEMAIL table. 
                        if (model.GROUPID != null)
                        {
                            var getUploadContactList = (from m in db.PsDbSet where m.COMPID == model.COMPID && m.PSGRID == model.GROUPID  && m.MOBNO!= null select new { m.MOBNO}).ToList();
                            foreach (var addList in getUploadContactList)
                            {
                                if (BdNumberValidation.NumberValidate(addList.MOBNO))
                                {
                                    string mobileNo = addList.MOBNO;
                                    Insert_ASL_PSMS_Table(model, mobileNo);
                                }
                            }
                        }


                        string currentDate = td.ToString("yyyy-MM-dd");
                        DateTime transdate = Convert.ToDateTime(currentDate.Substring(0, 10));
                        Int64 year = Convert.ToInt64(td.ToString("yyyy"));
                        var find_ASLPSMS = (from m in db.SendLogSMSDbSet where m.COMPID == model.COMPID && m.TRANSDT == transdate && m.TRANSYY == year && m.STATUS == "PENDING" select m).ToList();
                        Int64 countSms = 0;
                        foreach (var x in find_ASLPSMS)
                        {
                            if (model.Language == "ENG")
                                cashStatus = cardsms.DownloadString("http://66.45.237.70/api.php?username=" + userName +
                                                   "&password=" + usPss + "&number=" + x.MOBNO + "&sender=" + senderName + "&type=0&message=" + x.MESSAGE);
                            else
                                cashStatus = cardsms.DownloadString("http://66.45.237.70/api.php?username=" + userName +
                                                   "&password=" + usPss + "&number=" + x.MOBNO + "&sender=" + senderName + "&type=2&message=" + x.MESSAGE);

                            if (GetApiSmsResponseCodeMeaning.SmsResponseCodeMeaning(cashStatus) == "Request was successful")
                            {
                                countSms++;
                                Update_ASL_PSMS_Table_SendingEmail(x, model);
                            }
                        }
                        
                        if (countSms != 0)
                        {
                            TempData["SMS_psMessage"] = "Sending Done in " + countSms + " sms.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.SMS_psMessage = "Sms not sent.";
                            return View("Index");
                        }

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                        ViewBag.SMS_psMessage = "Sending Failed!!";
                        return View();
                    }

                }
                else
                {
                    ViewBag.SMS_psMessage = "Your SMS id not registered!!";
                    return View("Index");
                }

            }
            return View();
        }






        public void Insert_ASL_PSMS_Table(MailDTO currentMailBody, string mobileNo)
        {
            ASL_PSMS smsLogData = new ASL_PSMS();
            smsLogData.COMPID = Convert.ToInt64(currentMailBody.COMPID);
            string currentDate = td.ToString("dd-MMM-yyyy");
            smsLogData.TRANSDT = Convert.ToDateTime(currentDate);
            smsLogData.TRANSYY = Convert.ToInt64(td.ToString("yyyy"));

            string year = td.ToString("yyyy");
            string last2Digit_inYEAR = year.Substring(2, 2);
            Int64 max = Convert.ToInt64(currentMailBody.COMPID + last2Digit_inYEAR + "9999");
            try
            {
                Int64 maxTransNO = Convert.ToInt64((from n in db.SendLogSMSDbSet where n.COMPID == currentMailBody.COMPID && n.TRANSYY == smsLogData.TRANSYY select n.TRANSNO).Max());
                if (maxTransNO == 0)
                {
                    smsLogData.TRANSNO = Convert.ToInt64(currentMailBody.COMPID + last2Digit_inYEAR + "0001");
                }
                else if (maxTransNO <= max)
                {
                    smsLogData.TRANSNO = maxTransNO + 1;
                }
            }
            catch
            {
                smsLogData.TRANSNO = Convert.ToInt64(currentMailBody.COMPID + last2Digit_inYEAR + "0001");
            }

            if (mobileNo != null)
            {
                smsLogData.MOBNO = mobileNo;
            }

            smsLogData.LANGUAGE = currentMailBody.Language;
            smsLogData.MESSAGE = currentMailBody.Body;
            smsLogData.STATUS = "PENDING";
            //smsLogData.SENTTM = null;

            smsLogData.USERPC = strHostName;
            smsLogData.INSIPNO = ipAddress.ToString();
            smsLogData.INSTIME = Convert.ToDateTime(td);
            smsLogData.INSUSERID = currentMailBody.INSUSERID;
            smsLogData.INSLTUDE = Convert.ToString(currentMailBody.INSLTUDE);
            db.SendLogSMSDbSet.Add(smsLogData);
            db.SaveChanges();
        }


        public void Update_ASL_PSMS_Table_SendingEmail(ASL_PSMS model, MailDTO currentMailBody)
        {
            var updateTable =
                (from m in db.SendLogSMSDbSet where m.ID == model.ID && m.COMPID == model.COMPID select m).ToList();
            if (updateTable.Count == 1)
            {
                foreach (ASL_PSMS smsLogData in updateTable)
                {
                    smsLogData.STATUS = "SENT";
                    smsLogData.SENTTM = Convert.ToDateTime(td);
                    smsLogData.USERPC = strHostName;
                    smsLogData.UPDIPNO = ipAddress.ToString();
                    smsLogData.UPDTIME = Convert.ToDateTime(td);
                    smsLogData.UPDUSERID = currentMailBody.INSUSERID;
                    smsLogData.UPDLTUDE = Convert.ToString(currentMailBody.INSLTUDE);
                }
                db.SaveChanges();
            }
        }




        



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
