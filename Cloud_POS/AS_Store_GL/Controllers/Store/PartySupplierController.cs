using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.Models;
using AS_Store_GL.Models.Store;

namespace AS_Store_GL.Controllers.Store
{
    public class PartySupplierController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        Store_GL_DbContext db = new Store_GL_DbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;


        public PartySupplierController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            //td = DateTime.Now;
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            ViewData["HighLight_Menu_BillingForm"] = "Heigh Light Menu";
        }









        public ASL_LOG aslLog = new ASL_LOG();

        public void Insert_PartySupplier_LogData(STK_PS model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.COMPID && n.USERID == model.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.INSIPNO;
            aslLog.LOGLTUDE = model.INSLTUDE;
            aslLog.TABLEID = "STK_PS";

            string type = "";
            var party = Convert.ToInt64(model.COMPID + "103");
            var supplier = Convert.ToInt64(model.COMPID + "203");
            if (party == model.PSGRID)
            {
                type = "Party";
            }
            else if (supplier == model.PSGRID)
            {
                type = "Supplier";
            }
            var findAccountName = from p in db.GlAcchartDbSet where p.COMPID == model.COMPID && p.HEADCD == model.PSGRID && p.ACCOUNTCD == model.PSID select new { p.ACCOUNTNM };
            string accountname = "";
            foreach (var tag in findAccountName)
            {
                accountname = tag.ACCOUNTNM;
            }

            aslLog.LOGDATA = Convert.ToString("Type: " + type + ",\n" + type + " Name: " + accountname + ",\nAdress: " + model.ADDRESS + ",\nTeleNo: " + model.TELNO + ",\nMobile No: " + model.MOBNO + ",\nEmail ID: " + model.EMAIL + ",\nWeb address: " + model.WEBID + ",\nContact person Name: " + model.CPNM + ",\nContact mobile No: " + model.CPCNO + ",\nRemarks: " + model.REMARKS + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        public void Update_PartySupplier_LogData(STK_PS model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.COMPID && n.USERID == model.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.UPDIPNO;
            aslLog.LOGLTUDE = model.UPDLTUDE;
            aslLog.TABLEID = "STK_PS";

            string type = "";
            var party = Convert.ToInt64(model.COMPID + "103");
            var supplier = Convert.ToInt64(model.COMPID + "203");
            if (party == model.PSGRID)
            {
                type = "Party";
            }
            else if (supplier == model.PSGRID)
            {
                type = "Supplier";
            }
            var findAccountName = from p in db.GlAcchartDbSet where p.COMPID == model.COMPID && p.HEADCD == model.PSGRID && p.ACCOUNTCD == model.PSID select new { p.ACCOUNTNM };
            string accountname = "";
            foreach (var tag in findAccountName)
            {
                accountname = tag.ACCOUNTNM;
            }

            aslLog.LOGDATA = Convert.ToString("Type: " + type + ",\n" + type + " Name: " + accountname + ",\nAdress: " + model.ADDRESS + ",\nTeleNo: " + model.TELNO + ",\nMobile No: " + model.MOBNO + ",\nEmail ID: " + model.EMAIL + ",\nWeb address: " + model.WEBID + ",\nContact person Name: " + model.CPNM + ",\nContact mobile No: " + model.CPCNO + ",\nRemarks: " + model.REMARKS + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        public void Delete_PartySupplier_LogData(STK_PS model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.COMPID && n.USERID == model.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.UPDIPNO;
            aslLog.LOGLTUDE = model.UPDLTUDE;
            aslLog.TABLEID = "STK_PS";

            string type = "";
            var party = Convert.ToInt64(model.COMPID + "103");
            var supplier = Convert.ToInt64(model.COMPID + "203");
            if (party == model.PSGRID)
            {
                type = "Party";
            }
            else if (supplier == model.PSGRID)
            {
                type = "Supplier";
            }
            var findAccountName = from p in db.GlAcchartDbSet where p.COMPID == model.COMPID && p.HEADCD == model.PSGRID && p.ACCOUNTCD == model.PSID select new { p.ACCOUNTNM };
            string accountname = "";
            foreach (var tag in findAccountName)
            {
                accountname = tag.ACCOUNTNM;
            }

            aslLog.LOGDATA = Convert.ToString("Type: " + type + ",\n" + type + " Name: " + accountname + ",\nAdress: " + model.ADDRESS + ",\nTeleNo: " + model.TELNO + ",\nMobile No: " + model.MOBNO + ",\nEmail ID: " + model.EMAIL + ",\nWeb address: " + model.WEBID + ",\nContact person Name: " + model.CPNM + ",\nContact mobile No: " + model.CPCNO + ",\nRemarks: " + model.REMARKS + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }







        public ASL_DELETE AslDelete = new ASL_DELETE();

        public void Delete_PartySupplier_DELETE(STK_PS model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("HH:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == model.COMPID && n.USERID == model.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(model.COMPID);
            AslDelete.USERID = model.UPDUSERID;
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = model.UPDIPNO;
            AslDelete.DELLTUDE = model.UPDLTUDE;
            AslDelete.TABLEID = "STK_PS";

            string type = "";
            var party = Convert.ToInt64(model.COMPID + "103");
            var supplier = Convert.ToInt64(model.COMPID + "203");
            if (party == model.PSGRID)
            {
                type = "Party";
            }
            else if (supplier == model.PSGRID)
            {
                type = "Supplier";
            }
            var findAccountName = from p in db.GlAcchartDbSet where p.COMPID == model.COMPID && p.HEADCD == model.PSGRID && p.ACCOUNTCD == model.PSID select new { p.ACCOUNTNM };
            string accountname = "";
            foreach (var tag in findAccountName)
            {
                accountname = tag.ACCOUNTNM;
            }

            AslDelete.DELDATA = Convert.ToString("Type: " + type + ",\n" + type + " Name: " + accountname + ",\nAdress: " + model.ADDRESS + ",\nTeleNo: " + model.TELNO + ",\nMobile No: " + model.MOBNO + ",\nEmail ID: " + model.EMAIL + ",\nWeb address: " + model.WEBID + ",\nContact person Name: " + model.CPNM + ",\nContact mobile No: " + model.CPCNO + ",\nRemarks: " + model.REMARKS + ".");
            AslDelete.USERPC = model.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }



















        //Get 
        public ActionResult Create()
        {
            return View();
        }



        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(STK_PS model)
        {
            if (ModelState.IsValid)
            {
                model.USERPC = strHostName;
                model.INSIPNO = ipAddress.ToString();
                model.INSTIME = Convert.ToDateTime(td);
                model.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                var check_previousDataEntry =
                    (from m in db.PsDbSet
                     where m.COMPID == model.COMPID && m.PSID == model.PSID && m.PSGRID == model.PSGRID
                     select m).ToList();
                if (check_previousDataEntry.Count == 0)
                {
                    Insert_PartySupplier_LogData(model);
                    db.PsDbSet.Add(model);
                    db.SaveChanges();

                    TempData["PartySupplierCreateMessage"] = "Successfully entry ! ";
                    return RedirectToAction("Create");
                }
                else
                {
                    string name = "";
                    var party = Convert.ToInt64(model.COMPID + "103");
                    var supplier = Convert.ToInt64(model.COMPID + "203");
                    if (party == model.PSGRID)
                    {
                        name = "party";
                    }
                    else if (supplier == model.PSGRID)
                    {
                        name = "supplier";
                    }
                    TempData["PartySupplierCreateMessage"] = "This " + name + " name already saved!!!!";
                    return View("Create");
                }

            }
            return View("Create");
        }








        //Get
        public ActionResult Update()
        {
            return View();
        }


        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(STK_PS model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                model.USERPC = strHostName;
                model.UPDIPNO = ipAddress.ToString();
                model.UPDTIME = Convert.ToDateTime(td);
                model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                Update_PartySupplier_LogData(model);
                db.SaveChanges();
                TempData["PartySupplierUpdateMessage"] = "Successfully updated!";
                return RedirectToAction("Update");

            }
            return View("Update");

        }







        //Get
        public ActionResult Delete()
        {
            return View();
        }



        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(STK_PS model)
        {
            if (ModelState.IsValid)
            {
                var checkPrescriptionData = (from m in db.PsDbSet where m.COMPID == model.COMPID && m.PSID == model.PSID select m).ToList();
                if (checkPrescriptionData.Count == 1)
                {
                    STK_PS deleteModel = db.PsDbSet.Find(model.ID, model.COMPID, model.PSID);

                    model.USERPC = strHostName;
                    model.UPDIPNO = ipAddress.ToString();
                    model.UPDTIME = Convert.ToDateTime(td);
                    model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.UPDLTUDE = model.UPDLTUDE;

                    Delete_PartySupplier_DELETE(model);
                    Delete_PartySupplier_LogData(model);
                    db.SaveChanges();

                    db.PsDbSet.Remove(deleteModel);
                    db.SaveChanges();

                    TempData["PartySupplierDeleteMessage"] = "Successfully deleted!";
                    return RedirectToAction("Delete");
                }
            }
            return View();
        }











        //AutoComplete Master Part
        public JsonResult TagSearch_Create(string character, string type, string compid)
        {
            List<string> result = new List<string>();
            Int64 companyID = Convert.ToInt16(compid);
            Int64 psGid = Convert.ToInt64(type);
            var tags = from p in db.GlAcchartDbSet
                       where p.COMPID == companyID && p.HEADCD == psGid
                       select new { p.ACCOUNTNM };

            foreach (var tag in tags)
            {
                result.Add(tag.ACCOUNTNM.ToString());
            }


            return this.Json(result.Where(t => t.StartsWith(character)), JsonRequestBehavior.AllowGet);
        }




        //AutoComplete  Master Part
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword_Create(string changedText, string type, string compid)
        {
            var companyID = Convert.ToInt16(compid);
            String itemId = "";
            Int64 psGid = Convert.ToInt64(type);

            List<string> list = new List<string>();
            var tags = from p in db.GlAcchartDbSet
                       where p.COMPID == companyID && p.HEADCD == psGid
                       select new { p.ACCOUNTNM };
            foreach (var tag in tags)
            {
                list.Add(tag.ACCOUNTNM.ToString());
            }

            var rt = list.Where(t => t.StartsWith(changedText)).ToList();

            int lenChangedtxt = changedText.Length;

            Int64 cont = rt.Count();
            Int64 k = 0;
            string status = "";
            if (changedText != "" && cont != 0)
            {
                while (status != "no")
                {
                    k = 0;
                    foreach (var n in rt)
                    {
                        string ss = Convert.ToString(n);
                        string subss = "";
                        if (ss.Length >= lenChangedtxt)
                        {
                            subss = ss.Substring(0, lenChangedtxt);
                            subss = subss.ToUpper();
                        }
                        else
                        {
                            subss = "";
                        }


                        if (subss == changedText.ToUpper())
                        {
                            k++;
                        }
                        if (k == cont)
                        {
                            status = "yes";
                            lenChangedtxt++;
                            if (ss.Length > lenChangedtxt - 1)
                            {
                                changedText = changedText + ss[lenChangedtxt - 1];
                            }

                        }
                        else
                        {
                            status = "no";
                            //lenChangedtxt--;
                        }

                    }

                }
                if (lenChangedtxt == 1)
                {
                    itemId = changedText.Substring(0, lenChangedtxt);
                }

                else
                {
                    itemId = changedText.Substring(0, lenChangedtxt - 1);
                }
            }
            else
            {
                itemId = changedText;
            }

            String accountCD = "";

            var findAccountName_AccountCd =
                (from m in db.GlAcchartDbSet
                 where m.COMPID == companyID && m.ACCOUNTNM == changedText && m.HEADCD == psGid
                 select m).ToList();
            if (findAccountName_AccountCd.Count != 0)
            {
                foreach (var glAcchart in findAccountName_AccountCd)
                {
                    accountCD = Convert.ToString(glAcchart.ACCOUNTCD);
                }
            }




            var result = new { ACCOUNTNM = itemId, ACCOUNTCD = accountCD };
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public JsonResult TagSearch_Update_Delete(string character, string type, string compid)
        {
            List<string> result = new List<string>();
            Int64 companyID = Convert.ToInt16(compid);
            Int64 psGid = Convert.ToInt64(type);
            var tags = (from pS in db.PsDbSet
                        where pS.COMPID == companyID && pS.PSGRID == psGid
                        select new { pS.PSID }).ToList();

            foreach (var tag in tags)
            {
                var check_GLAcchart =
                    (from m in db.GlAcchartDbSet
                     where m.COMPID == companyID && m.HEADCD == psGid && m.ACCOUNTCD == tag.PSID
                     select new { m.ACCOUNTNM }).ToList();
                foreach (var a in check_GLAcchart)
                {
                    result.Add(a.ACCOUNTNM.ToString());
                }
            }


            return this.Json(result.Where(t => t.StartsWith(character)), JsonRequestBehavior.AllowGet);
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword_Update_Delete(string changedText, string type, string compid)
        {
            var companyID = Convert.ToInt16(compid);
            String itemId = "";
            Int64 psGid = Convert.ToInt64(type);

            List<string> list = new List<string>();

            var tags = (from pS in db.PsDbSet
                        where pS.COMPID == companyID && pS.PSGRID == psGid
                        select new { pS.PSID }).ToList();

            foreach (var tag in tags)
            {
                var check_GLAcchart = (from m in db.GlAcchartDbSet
                                       where m.COMPID == companyID && m.HEADCD == psGid && m.ACCOUNTCD == tag.PSID
                                       select new { m.ACCOUNTNM }).ToList();
                foreach (var a in check_GLAcchart)
                {
                    list.Add(a.ACCOUNTNM.ToString());
                }
            }
            var rt = list.Where(t => t.StartsWith(changedText)).ToList();

            int lenChangedtxt = changedText.Length;

            Int64 cont = rt.Count();
            Int64 k = 0;
            string status = "";
            if (changedText != "" && cont != 0)
            {
                while (status != "no")
                {
                    k = 0;
                    foreach (var n in rt)
                    {
                        string ss = Convert.ToString(n);
                        string subss = "";
                        if (ss.Length >= lenChangedtxt)
                        {
                            subss = ss.Substring(0, lenChangedtxt);
                            subss = subss.ToUpper();
                        }
                        else
                        {
                            subss = "";
                        }


                        if (subss == changedText.ToUpper())
                        {
                            k++;
                        }
                        if (k == cont)
                        {
                            status = "yes";
                            lenChangedtxt++;
                            if (ss.Length > lenChangedtxt - 1)
                            {
                                changedText = changedText + ss[lenChangedtxt - 1];
                            }

                        }
                        else
                        {
                            status = "no";
                            //lenChangedtxt--;
                        }

                    }

                }
                if (lenChangedtxt == 1)
                {
                    itemId = changedText.Substring(0, lenChangedtxt);
                }

                else
                {
                    itemId = changedText.Substring(0, lenChangedtxt - 1);
                }
            }
            else
            {
                itemId = changedText;
            }

            String accountCD = "", address = "", teleNo = "", mobNo = "", email = "", webid = "", cpname = "", cpcno = "", remarks = "";

            var findAccountName_AccountCd = (from m in db.GlAcchartDbSet
                                             where m.COMPID == companyID && m.ACCOUNTNM == changedText && m.HEADCD == psGid
                                             select m).ToList();
            if (findAccountName_AccountCd.Count != 0)
            {
                foreach (var glAcchart in findAccountName_AccountCd)
                {
                    accountCD = Convert.ToString(glAcchart.ACCOUNTCD);
                }
            }

            Int64 psid = 0;
            if (accountCD != "")
            {
                psid = Convert.ToInt64(accountCD);
            }

            Int64 insertUserId = 0, id = 0;
            string inserttime = "", insertIpno = "", insltude = "";
            var get_Stk_PK = (from m in db.PsDbSet where m.COMPID == companyID && m.PSGRID == psGid && m.PSID == psid select m).ToList();
            foreach (var stkPs in get_Stk_PK)
            {
                id = stkPs.ID;
                address = stkPs.ADDRESS;
                teleNo = stkPs.TELNO;
                mobNo = stkPs.MOBNO;
                email = stkPs.EMAIL;
                webid = stkPs.WEBID;
                cpname = stkPs.CPNM;
                cpcno = stkPs.CPCNO;
                remarks = stkPs.REMARKS;
                insertUserId = stkPs.INSUSERID;
                inserttime = Convert.ToString(stkPs.INSTIME);
                insertIpno = Convert.ToString(stkPs.INSIPNO);
                insltude = Convert.ToString(stkPs.INSLTUDE);
            }

            var result = new
            {
                ID = id,
                ACCOUNTNM = itemId,
                ACCOUNTCD = accountCD,
                ADDRESS = address,
                TELNO = teleNo,
                MOBNO = mobNo,
                EMAIL = email,
                WEBID = webid,
                CPNM = cpname,
                CPCNO = cpcno,
                REMARKS = remarks,
                INSUSERID = insertUserId,
                INSTIME = inserttime,
                INSIPNO = insertIpno,
                INSLTUDE = insltude
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }









        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
