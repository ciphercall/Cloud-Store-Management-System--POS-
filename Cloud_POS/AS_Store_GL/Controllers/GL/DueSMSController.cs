using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.DataAccess;
using AS_Store_GL.Models;
using AS_Store_GL.Models.Store;

namespace AS_Store_GL.Controllers.GL
{
    public class DueSMSController : AppController
    {

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;
        public string transdate;

        Store_GL_DbContext db = new Store_GL_DbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        private Int64 compid;

        public DueSMSController()
        {
            //HttpCookie decodedCookie1 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["CI"]);
            //HttpCookie decodedCookie2 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["UI"]);
            //HttpCookie decodedCookie3 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["UT"]);
            //HttpCookie decodedCookie4 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["UN"]);
            //HttpCookie decodedCookie5 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["US"]);
            //HttpCookie decodedCookie6 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["CS"]);

            //if (System.Web.HttpContext.Current.Request.Cookies["UI"] != null)
            //{
            //    compid = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["CI"].Value);
            //}
            //else
            //{
            //    Index();
            //}
            compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            //td = DateTime.Now;
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            transdate = Convert.ToString(td.ToString("d"));

            ViewData["HighLight_Menu_BillingForm"] = "Heigh Light Menu";
        }


        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(PageModel model, string command)
        {
            Int64 countSms = 0;

            DateTime datetime = Convert.ToDateTime(model.Report_FromDate);
            string transDate = datetime.ToString("yyyy-MM-dd");
            string year = datetime.ToString("yyyy");
            model.StkTransmst.TRANSDT = Convert.ToDateTime(transDate);
            model.StkTransmst.TRANSYY = Convert.ToInt64(year);

            var getSMSUserNamePass = from m in db.AslCompanyDbSet where m.COMPID == model.StkTransmst.COMPID select new { m.SMSIDP, m.SMSPWP, m.SMSSENDERNM };
            string userName = "", usPss = "", senderName = "";
            foreach (var VARIABLE in getSMSUserNamePass)
            {
                senderName = VARIABLE.SMSSENDERNM;
                userName = VARIABLE.SMSIDP;
                usPss = VARIABLE.SMSPWP;
            }

            // Checking Date wise sms group. 
            var searchPreviousData = (from m in db.CustomSmsDbSet
                                      where m.COMPID == model.StkTransmst.COMPID && m.TRANSDT == model.StkTransmst.TRANSDT
                                      && m.TRANSYY == model.StkTransmst.TRANSYY
                                      && m.STATUS == "PENDING" && m.SMSTP == "DUE"
                                      select m).ToList();
            if (searchPreviousData.Count != 0)
            {
                if (userName != null && usPss != null && senderName != null)
                {
                    SendingCustomSMS(model, userName, usPss, senderName);
                    var sendSMScount = (from m in db.CustomSmsDbSet
                        where m.COMPID == model.StkTransmst.COMPID && m.TRANSDT == model.StkTransmst.TRANSDT
                              && m.TRANSYY == model.StkTransmst.TRANSYY
                              && m.STATUS == "SENT" && m.SMSTP == "DUE"
                        select m).ToList();
                    countSms = sendSMScount.Count;
                    TempData["SendingDueSMS"] = "Sending " + countSms + " sms Done.";
                    TempData["PendingDueSMS"] = model;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["SendingDueSMS"] = "Your Sms not registered!!";
                    TempData["PendingDueSMS"] = model;
                    return RedirectToAction("Index");
                }
              
            }




            if (userName != null && usPss != null && senderName != null)
            {
                System.Data.SqlClient.SqlConnection conn =
                    new System.Data.SqlClient.SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
                string query = string.Format(
                    @"select DEBITCD, (Sum(DEBITAMT) - sum(CREDITAMT)) as AMOUNT from GL_Master where COMPID='{0}'
and TRANSDT<='{1}'
and substring(CONVERT(NVARCHAR(20),DEBITCD,103),4,3)='103'
group by DEBITCD
Having (Sum(DEBITAMT) - sum(CREDITAMT)) > 0",
                    model.StkTransmst.COMPID, transDate);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);

                foreach (DataRow row in ds.Rows)
                {
                    model.StkTransmst.PSID = Convert.ToInt64(row["DEBITCD"].ToString());
                    model.StkTransmst.TOTNET = Convert.ToDecimal(row["AMOUNT"].ToString());

                    var findPSID = (from m in db.PsDbSet
                        where
                            m.COMPID == model.StkTransmst.COMPID && m.PSID == model.StkTransmst.PSID && m.MOBNO != null
                        select new {m.MOBNO}).ToList();
                    foreach (var get in findPSID)
                    {
                        if (BdNumberValidation.NumberValidate(get.MOBNO))
                        {
                            string mobileNo = get.MOBNO;
                            Insert_ASL_CSMS_Table(model, mobileNo);
                        }
                    }
                }
                conn.Close();

                SendingCustomSMS(model, userName, usPss, senderName);
                var sendSMScount = (from m in db.CustomSmsDbSet
                    where m.COMPID == model.StkTransmst.COMPID && m.TRANSDT == model.StkTransmst.TRANSDT
                          && m.TRANSYY == model.StkTransmst.TRANSYY
                          && m.STATUS == "SENT" && m.SMSTP == "DUE"
                    select m).ToList();
                countSms = sendSMScount.Count;
                TempData["SendingDueSMS"] = "Sending " + countSms + " sms Done.";
                TempData["PendingDueSMS"] = model;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SendingDueSMS"] = "Your Sms not registered!!";
                TempData["PendingDueSMS"] = model;
                return RedirectToAction("Index");
            }
        }





        // Send custom SMS - saved in ASL_CSMS table
        private void Insert_ASL_CSMS_Table(PageModel currentSmsBody, string mobileNo)
        {
            ASL_CSMS smsLogData = new ASL_CSMS();
            String companyName = "";
            var findComapnyName = (from m in db.AslCompanyDbSet where m.COMPID == currentSmsBody.StkTransmst.COMPID select new { m.COMPNM })
                    .ToList();
            foreach (var getCompanyName in findComapnyName)
            {
                companyName = getCompanyName.COMPNM.ToString();
            }

            Int64 max = Convert.ToInt64(currentSmsBody.StkTransmst.TRANSYY + "9999");
            try
            {
                Int64 maxTransNO = Convert.ToInt64((from n in db.CustomSmsDbSet
                                                    where n.COMPID == currentSmsBody.StkTransmst.COMPID && n.TRANSYY == currentSmsBody.StkTransmst.TRANSYY
                                                     && n.SMSTP == "DUE"
                                                    select n.TRANSNO).Max());
                if (maxTransNO == 0)
                {
                    smsLogData.TRANSNO = Convert.ToInt64(currentSmsBody.StkTransmst.TRANSYY + "0001");
                }
                else if (maxTransNO <= max)
                {
                    smsLogData.TRANSNO = maxTransNO + 1;
                }
            }
            catch
            {
                smsLogData.TRANSNO = Convert.ToInt64(currentSmsBody.StkTransmst.TRANSYY + "0001");
            }


            smsLogData.COMPID = Convert.ToInt64(currentSmsBody.StkTransmst.COMPID);
            smsLogData.TRANSDT = Convert.ToDateTime(currentSmsBody.StkTransmst.TRANSDT);
            smsLogData.TRANSYY = Convert.ToInt64(currentSmsBody.StkTransmst.TRANSYY);
            smsLogData.SMSTP = "DUE";
            if (mobileNo != null)
            {
                smsLogData.MOBNO = mobileNo;
            }
            smsLogData.LANGUAGE = "ENG";
            smsLogData.MESSAGE = "Dear Customer, You current due balance is TK. " + currentSmsBody.StkTransmst.TOTNET + ", Please pay within short possible time, Regards," + companyName + ".";
            smsLogData.STATUS = "PENDING";
            //smsLogData.SENTTM = null;

            smsLogData.USERPC = strHostName;
            smsLogData.INSIPNO = ipAddress.ToString();
            smsLogData.INSTIME = Convert.ToDateTime(td);
            smsLogData.INSUSERID = Convert.ToInt64(currentSmsBody.StkTransmst.INSUSERID);
            smsLogData.INSLTUDE = Convert.ToString(currentSmsBody.StkTransmst.INSLTUDE);
            db.CustomSmsDbSet.Add(smsLogData);
            db.SaveChanges();
        }






        private void SendingCustomSMS(PageModel model, String userName, String usPss, String senderName)
        {
            try
            {
                WebClient cardsms = new WebClient();
                string cashStatus = "";
                Int64 countSms = 0;

                var find_CustomSMS = (from m in db.CustomSmsDbSet
                                      where m.COMPID == model.StkTransmst.COMPID && m.TRANSDT == model.StkTransmst.TRANSDT
                                      && m.TRANSYY == model.StkTransmst.TRANSYY && m.STATUS == "PENDING" && m.SMSTP == "DUE"
                                      select m).ToList();

                foreach (var x in find_CustomSMS)
                {
                    if (x.LANGUAGE == "ENG")
                        cashStatus = cardsms.DownloadString("http://66.45.237.70/api.php?username=" + userName +
                                                "&password=" + usPss + "&number=" + x.MOBNO + "&sender=" + senderName + "&type=0&message=" + x.MESSAGE);
                    else
                        cashStatus = cardsms.DownloadString("http://66.45.237.70/api.php?username=" + userName +
                                                "&password=" + usPss + "&number=" + x.MOBNO + "&sender=" + senderName + "&type=2&message=" + x.MESSAGE);


                    // Send custom SMS - sending sucessfully or not
                    if (GetApiSmsResponseCodeMeaning.SmsResponseCodeMeaning(cashStatus) == "Request was successful")
                    {
                        countSms++;
                        Update_ASL_CSMS_Table_SendingSms(x, model);
                    }
                }

                if (countSms != 0)
                {
                    ViewBag.CustomSMSMessage = "Sending Done.";
                }
                else
                {
                    ViewBag.CustomSMSMessage = "Sms not sent!";
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                ViewBag.CustomSMSMessage = "Sending Failed!!";
            }
        }






        // Send custom SMS - Update in ASL_CSMS table
        private void Update_ASL_CSMS_Table_SendingSms(ASL_CSMS model, PageModel currentSmsBody)
        {
            var updateTable =
                (from m in db.CustomSmsDbSet where m.ID == model.ID && m.COMPID == model.COMPID select m).ToList();
            if (updateTable.Count == 1)
            {
                foreach (ASL_CSMS csmsLogData in updateTable)
                {
                    csmsLogData.STATUS = "SENT";
                    csmsLogData.SENTTM = Convert.ToDateTime(td);
                    csmsLogData.USERPC = strHostName;
                    csmsLogData.UPDIPNO = ipAddress.ToString();
                    csmsLogData.UPDTIME = Convert.ToDateTime(td);
                    csmsLogData.UPDUSERID = Convert.ToInt64(currentSmsBody.StkTransmst.INSUSERID);
                    csmsLogData.UPDLTUDE = Convert.ToString(currentSmsBody.StkTransmst.INSLTUDE);
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
