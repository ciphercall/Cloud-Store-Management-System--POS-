using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.Models;

namespace AS_Store_GL.Controllers
{
    public class StoreController : AppController
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

        public StoreController()
        {
            //if (System.Web.HttpContext.Current.Request.Cookies["UI"] != null)
            //{

            //}
            //else
            //{
            //    Index();
            //}
            //HttpCookie decodedCookie1 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["CI"]);
            //HttpCookie decodedCookie2 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["UI"]);
            //HttpCookie decodedCookie3 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["UT"]);
            //HttpCookie decodedCookie4 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["UN"]);
            //HttpCookie decodedCookie5 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["US"]);
            //HttpCookie decodedCookie6 = CookieSecurityProvider.Decrypt(System.Web.HttpContext.Current.Request.Cookies["CS"]);

            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            //td = DateTime.Now;
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            ViewData["HighLight_Menu_BillingForm"] = "Heigh Light Menu";
        }






        public ASL_LOG aslLog = new ASL_LOG();
        public void Insert_Stk_Store_LogData(STK_STORE stkStore)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkStore.COMPID && n.USERID == stkStore.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkStore.COMPID);
            aslLog.USERID = stkStore.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkStore.INSIPNO;
            aslLog.LOGLTUDE = stkStore.INSLTUDE;
            aslLog.TABLEID = "STK_STORE";
            aslLog.LOGDATA = Convert.ToString("Store Nmae: " + stkStore.STORENM + ",\nStores ID: " + stkStore.STORESID + ",\nRemarks: " + stkStore.REMARKS + ".");
            aslLog.USERPC = stkStore.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }



        public void Update_Stk_Store_LogData(ASL_LOG stkStore)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkStore.COMPID && n.USERID == stkStore.USERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkStore.COMPID);
            aslLog.USERID = stkStore.USERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkStore.LOGIPNO;
            aslLog.LOGLTUDE = stkStore.LOGLTUDE;
            aslLog.TABLEID = "STK_STORE";
            aslLog.LOGDATA = stkStore.LOGDATA;
            aslLog.USERPC = stkStore.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        public void Delete_STK_STORE_LogData(STK_STORE stkStore)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkStore.COMPID && n.USERID == stkStore.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkStore.COMPID);
            //aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
            aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkStore.UPDIPNO;
            aslLog.LOGLTUDE = stkStore.UPDLTUDE;
            aslLog.TABLEID = "STK_STORE";
            aslLog.LOGDATA = Convert.ToString("Store Nmae: " + stkStore.STORENM + ",\nStores ID: " + stkStore.STORESID + ",\nRemarks: " + stkStore.REMARKS + ".");
            aslLog.USERPC = stkStore.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }







        public ASL_DELETE AslDelete = new ASL_DELETE();
        public void Delete_ASL_DELETE_Stk_Store(STK_STORE stkStore)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == stkStore.COMPID && n.USERID == stkStore.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(stkStore.COMPID);
            //AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = stkStore.UPDIPNO;
            AslDelete.DELLTUDE = stkStore.UPDLTUDE;
            AslDelete.TABLEID = "STK_STORE";
            AslDelete.DELDATA = Convert.ToString("Store Nmae: " + stkStore.STORENM + ",\nStores ID: " + stkStore.STORESID + ",\nRemarks: " + stkStore.REMARKS + ".");
            AslDelete.USERPC = stkStore.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }





        public ActionResult Index()
        {
            var dt = (STK_STORE)TempData["model"];
            return View(dt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(STK_STORE stkStore, string command)
        {
            if (ModelState.IsValid)
            {
                if (command == "Add")
                {
                    //.........................................................Create Permission Check
                    //var LoggedCompId = System.Web.HttpContext.Current.Request.Cookies["CI"].Value;
                    //var loggedUserID = System.Web.HttpContext.Current.Request.Cookies["UI"].Value;

                    var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"];
                    var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"];

                    var createStatus = "";

                    System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
                    string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='Store' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable ds = new DataTable();
                    da.Fill(ds);

                    foreach (DataRow row in ds.Rows)
                    {
                        createStatus = row["INSERTR"].ToString();
                    }

                    conn.Close();

                    if (createStatus == 'I'.ToString())
                    {
                        TempData["ShowUpdateButton"] = null;
                        TempData["message"] = "Permission not granted!";
                        return RedirectToAction("Index");
                    }
                    //...............................................................................................

                    stkStore.USERPC = strHostName;
                    stkStore.INSIPNO = ipAddress.ToString();
                    stkStore.INSTIME = td;
                    //storeItemModel.StkItem.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                    stkStore.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    Int64 maxData = Convert.ToInt64((from n in db.StkStoreDbSet where n.COMPID == stkStore.COMPID select n.STOREID).Max());
                    Int64 R = Convert.ToInt64(stkStore.COMPID + "99");

                    if (maxData == 0)
                    {
                        stkStore.STOREID = Convert.ToInt64(stkStore.COMPID + "01");
                        TempData["message"] = "Store name successfully saved!";
                        Insert_Stk_Store_LogData(stkStore);
                        db.StkStoreDbSet.Add(stkStore);
                        db.SaveChanges();
                    }
                    else if (maxData <= R)
                    {
                        stkStore.STOREID = maxData + 1;
                        TempData["message"] = "Store name successfully saved!";
                        Insert_Stk_Store_LogData(stkStore);
                        db.StkStoreDbSet.Add(stkStore);
                        db.SaveChanges();
                    }
                    else
                    {
                        TempData["message"] = "Entry not possible";
                    }
                    return RedirectToAction("Index");
                }

                else if (command == "Update")
                {
                    var query = from a in db.StkStoreDbSet where (a.STOREID == stkStore.STOREID && a.COMPID == stkStore.COMPID) select a;
                    foreach (STK_STORE a in query)
                    {
                        // Insert any additional changes to column values.
                        a.STORENM = stkStore.STORENM;
                        a.STORESID = stkStore.STORESID;
                        a.REMARKS = stkStore.REMARKS;
                        a.UPDIPNO = ipAddress.ToString();
                        a.UPDTIME = td;
                        a.USERPC = strHostName;
                        //a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                        a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        a.UPDLTUDE = stkStore.INSLTUDE;
                        TempData["Stk_Store_LogData"] = Convert.ToString("Store Nmae: " + a.STORENM + ",\nStores ID: " + a.STORESID + ",\nRemarks: " + a.REMARKS + ".");
                    }

                    ASL_LOG aslLogref = new ASL_LOG();
                    aslLogref.COMPID = stkStore.COMPID;
                    aslLogref.LOGLTUDE = stkStore.INSLTUDE;
                    aslLogref.LOGIPNO = ipAddress.ToString();
                    aslLogref.USERPC = strHostName;
                    //aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                    aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    aslLogref.LOGDATA = TempData["Stk_Store_LogData"].ToString();

                    Update_Stk_Store_LogData(aslLogref);
                    db.SaveChanges();
                }

                TempData["latitute_Delete"] = stkStore.INSLTUDE;
                TempData["message"] = "Store name successfully updated!";
                TempData["ShowUpdateButton"] = null;
                return RedirectToAction("Index");
            }
            return View("Index");
        }






        public ActionResult Update(Int64 ID, Int64 cid, Int64 storId, STK_STORE model)
        {
            //.........................................................Update Permission Check
            //var LoggedCompId = System.Web.HttpContext.Current.Request.Cookies["CI"].Value;
            //var loggedUserID = System.Web.HttpContext.Current.Request.Cookies["UI"].Value;
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"];
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"];

            var updateStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
            string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='Store' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query1, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                updateStatus = row["UPDATER"].ToString();
            }
            conn.Close();
            if (updateStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");
            }
            //...............................................................................................

            model = db.StkStoreDbSet.Find(ID);
            TempData["model"] = model;
            TempData["ShowUpdateButton"] = "Show update Button";
            return RedirectToAction("Index");
        }





        public ActionResult Delete(Int64 ID, Int64 cid, Int64 storId, STK_STORE model)
        {
            //.........................................................Delete Permission Check
            //var LoggedCompId = System.Web.HttpContext.Current.Request.Cookies["CI"].Value;
            //var loggedUserID = System.Web.HttpContext.Current.Request.Cookies["UI"].Value;
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"];
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"];

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='Store' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                deleteStatus = row["DELETER"].ToString();
            }
            conn.Close();
            if (deleteStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");

            }
            //...............................................................................................

            model = db.StkStoreDbSet.Find(ID);
            model.USERPC = strHostName;
            model.UPDIPNO = ipAddress.ToString();
            model.UPDTIME = Convert.ToDateTime(td);
            model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            if (TempData["latitute_Delete"] != null)
            {
                //Get current LOGLTUDE data 
                model.UPDLTUDE = TempData["latitute_Delete"].ToString();
            }

            Delete_STK_STORE_LogData(model);
            Delete_ASL_DELETE_Stk_Store(model);
            db.StkStoreDbSet.Remove(model);
            db.SaveChanges();
            TempData["ShowUpdateButton"] = null;
            TempData["message"] = "Store name successfully Deleted!";
            return RedirectToAction("Index");
        }





        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
