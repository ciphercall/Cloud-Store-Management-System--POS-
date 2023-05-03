using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.DataAccess;
using AS_Store_GL.Models;

namespace AS_Store_GL.Controllers
{
    public class StockItemController : AppController
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

        public StockItemController()
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




        // Create ASL_LOG object and it used to this Insert_StkItemMst_LogData, Update_Stk_ItemMst_LogData, Delete_Stk_ItemMst_LogData (STK_ITEMMST posItemmst).
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from CategoryItemModel TO Asl_lOG Database Table.
        public void Insert_StkItemMst_LogData(PageModel storeitem)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == storeitem.StkItemmst.COMPID && n.USERID == storeitem.StkItemmst.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(storeitem.StkItemmst.COMPID);
            aslLog.USERID = storeitem.StkItemmst.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = storeitem.StkItemmst.INSIPNO;
            aslLog.LOGLTUDE = storeitem.StkItemmst.INSLTUDE;
            aslLog.TABLEID = "STK_ITEMMST";
            aslLog.LOGDATA = Convert.ToString("Category Name: " + storeitem.StkItemmst.CATNM + ",\nRemarks: " + storeitem.StkItemmst.REMARKS + ".");
            aslLog.USERPC = storeitem.StkItemmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // Edit ALL INFORMATION from STK_ITEMMST TO Asl_lOG Database Table.
        public void Update_Stk_ItemMst_LogData(STK_ITEMMST stkItemmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkItemmst.COMPID && n.USERID == stkItemmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkItemmst.COMPID);
            aslLog.USERID = stkItemmst.UPDUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkItemmst.UPDIPNO;
            aslLog.LOGLTUDE = stkItemmst.UPDLTUDE;
            aslLog.TABLEID = "STK_ITEMMST";
            aslLog.LOGDATA = Convert.ToString("Category Name: " + stkItemmst.CATNM + ",\nRemarks: " + stkItemmst.REMARKS + ".");
            aslLog.USERPC = stkItemmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }





        // Delete ALL INFORMATION from STK_ITEMMST TO Asl_lOG Database Table.
        public void Delete_Stk_ItemMst_LogData(STK_ITEMMST stkItemmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkItemmst.COMPID && n.USERID == stkItemmst.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkItemmst.COMPID);
            aslLog.USERID = stkItemmst.UPDUSERID;
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkItemmst.UPDIPNO;
            aslLog.LOGLTUDE = stkItemmst.UPDLTUDE;
            aslLog.TABLEID = "STK_ITEMMST";
            aslLog.LOGDATA = Convert.ToString("Category Name: " + stkItemmst.CATNM + ",\nRemarks: " + stkItemmst.REMARKS + ".");
            aslLog.USERPC = stkItemmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }






        // SAVE ALL INFORMATION from STK_ITEM TO Asl_lOG Database Table.
        public void Insert_StkItem_LogData(STK_ITEM stkItem)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkItem.COMPID && n.USERID == stkItem.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkItem.COMPID);
            aslLog.USERID = stkItem.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkItem.INSIPNO;
            aslLog.LOGLTUDE = stkItem.INSLTUDE;
            aslLog.TABLEID = "STK_ITEM";
            aslLog.LOGDATA = Convert.ToString("Item Name: " + stkItem.ITEMNM + ",\nBrand: " + stkItem.BRAND + ",\nUnit: " + stkItem.UNIT + ",\nBuy Rate: " + stkItem.BUYRT + ",\nSale Rate: " + stkItem.SALRT + ",\nStoke Minimum: " + stkItem.STKMIN + ",\nDisscount: " + stkItem.DISCRT + ",\nCPQTY: " + stkItem.CPQTY + ",\nRemarks: " + stkItem.REMARKS + ".");
            aslLog.USERPC = stkItem.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }






        // Edit ALL INFORMATION from STK_ITEM TO Asl_lOG Database Table.
        public void Update_StkItem_LogData(ASL_LOG aslLOGRef)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == aslLOGRef.COMPID && n.USERID == aslLOGRef.USERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aslLOGRef.COMPID);
            aslLog.USERID = aslLOGRef.USERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aslLOGRef.LOGIPNO;
            aslLog.LOGLTUDE = aslLOGRef.LOGLTUDE;
            aslLog.TABLEID = "STK_ITEM";
            aslLog.LOGDATA = aslLOGRef.LOGDATA;
            aslLog.USERPC = strHostName;
            db.AslLogDbSet.Add(aslLog);
        }




        // Delete ALL INFORMATION from STK_ITEM TO Asl_lOG Database Table.
        public void Delete_STK_ITEM_LogData(STK_ITEM stkItem)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == stkItem.COMPID && n.USERID == stkItem.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(stkItem.COMPID);
            //aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
            aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = stkItem.UPDIPNO;
            aslLog.LOGLTUDE = stkItem.UPDLTUDE;
            aslLog.TABLEID = "STK_ITEM";
            aslLog.LOGDATA = Convert.ToString("Item Name: " + stkItem.ITEMNM + ",\nBrand: " + stkItem.BRAND + ",\nUnit: " + stkItem.UNIT + ",\nBuy Rate: " + stkItem.BUYRT + ",\nSale Rate: " + stkItem.SALRT + ",\nStoke Minimum: " + stkItem.STKMIN + ",\nDisscount: " + stkItem.DISCRT + ",\nCPQTY: " + stkItem.CPQTY + ",\nRemarks: " + stkItem.REMARKS + ".");
            aslLog.USERPC = stkItem.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }




        // Create ASL_DELETE object and it used to this Delete_ASL_DELETE (AslUserco aslUsercos).
        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from STK_ITEMMST TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE_Stk_ItemMst(STK_ITEMMST stkItemmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == stkItemmst.COMPID && n.USERID == stkItemmst.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(stkItemmst.COMPID);
            //AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = stkItemmst.UPDIPNO;
            AslDelete.DELLTUDE = stkItemmst.UPDLTUDE;
            AslDelete.TABLEID = "STK_ITEMMST";
            AslDelete.DELDATA = Convert.ToString("Category Name: " + stkItemmst.CATNM + ",\nRemarks: " + stkItemmst.REMARKS + ".");
            AslDelete.USERPC = stkItemmst.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }





        // Delete ALL INFORMATION from STK_ITEM TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE_STK_ITEM(STK_ITEM stkItem)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == stkItem.COMPID && n.USERID == stkItem.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(stkItem.COMPID);
            //AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = stkItem.UPDIPNO;
            AslDelete.DELLTUDE = stkItem.UPDLTUDE;
            AslDelete.TABLEID = "STK_ITEM";
            AslDelete.DELDATA = Convert.ToString("Item Name: " + stkItem.ITEMNM + ",\nBrand: " + stkItem.BRAND + ",\nUnit: " + stkItem.UNIT + ",\nBuy Rate: " + stkItem.BUYRT + ",\nSale Rate: " + stkItem.SALRT + ",\nStoke Minimum: " + stkItem.STKMIN + ",\nDisscount: " + stkItem.DISCRT + ",\nCPQTY: " + stkItem.CPQTY + ",\nRemarks: " + stkItem.REMARKS + ".");
            AslDelete.USERPC = stkItem.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }





        // GET: /CategoryItem/
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            var dt = (PageModel)TempData["category"];
            return View(dt);
        }



        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(PageModel storeItemModel, string command)
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
                string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='StockItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["category"] = storeItemModel;
                    TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Index");
                }
                //...............................................................................................

                storeItemModel.StkItem.COMPID = storeItemModel.StkItemmst.COMPID;
                storeItemModel.StkItem.CATID = storeItemModel.StkItemmst.CATID;
                if (storeItemModel.StkItem.CATID == null)
                {
                    TempData["message"] = "Enter Category First";
                    return View("Index");
                }
                if (storeItemModel.StkItem.ITEMNM == null)
                {
                    TempData["ShowAddButton"] = "Show Add Button";
                    TempData["Null_Item_Name"] = "Item Name required!";
                    TempData["category"] = storeItemModel;
                    TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                    return View("Index");
                }
                storeItemModel.StkItem.USERPC = strHostName;
                storeItemModel.StkItem.INSIPNO = ipAddress.ToString();
                storeItemModel.StkItem.INSTIME = td;
                //storeItemModel.StkItem.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                storeItemModel.StkItem.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                try
                {
                   
                    STK_ITEMMST stk_itemmst_CompID = db.StkItemmstDbSet.FirstOrDefault(r => (r.COMPID == storeItemModel.StkItemmst.COMPID));
                    Int64 catagoryID = Convert.ToInt64(storeItemModel.StkItemmst.CATID);
                    STK_ITEMMST stk_ItemMst_CATID = db.StkItemmstDbSet.FirstOrDefault(r => (r.CATID == catagoryID));

                    if (stk_itemmst_CompID == null && stk_ItemMst_CATID == null)
                    {
                        TempData["ShowAddButton"] = "Show Add Button";
                        TempData["message"] = "Catagory ID not found ";
                        return View("Index");
                    }
                    else
                    {
                        Int64 maxData = Convert.ToInt64((from n in db.StkItemDbSet where n.COMPID == storeItemModel.StkItemmst.COMPID && n.CATID == storeItemModel.StkItemmst.CATID select n.ITEMID).Max());

                        Int64 R = Convert.ToInt64(catagoryID + "9999");

                        if (maxData == 0)
                        {
                            storeItemModel.StkItem.ITEMID = Convert.ToInt64(catagoryID + "0001");
                            Insert_StkItem_LogData(storeItemModel.StkItem);

                            db.StkItemDbSet.Add(storeItemModel.StkItem);
                            if (db.SaveChanges() > 0)
                            {
                                TempData["message"] = "Item Successfully Saved";
                                storeItemModel.StkItem.ITEMNM = "";
                                storeItemModel.StkItem.BRAND = "";
                                storeItemModel.StkItem.UNIT = "";
                                storeItemModel.StkItem.BUYRT = 0;
                                storeItemModel.StkItem.SALRT = 0;
                                storeItemModel.StkItem.STKMIN = 0;
                                storeItemModel.StkItem.DISCRT = 0;
                                storeItemModel.StkItem.CPQTY = 0;
                                storeItemModel.StkItem.REMARKS = "";



                            }

                        }
                        else if (maxData <= R)
                        {

                            storeItemModel.StkItem.ITEMID = maxData + 1;
                            Insert_StkItem_LogData(storeItemModel.StkItem);

                            db.StkItemDbSet.Add(storeItemModel.StkItem);
                            db.SaveChanges();
                            TempData["message"] = "Item Successfully Saved";
                            storeItemModel.StkItem.ITEMNM = "";
                            storeItemModel.StkItem.BRAND = "";
                            storeItemModel.StkItem.UNIT = "";
                            storeItemModel.StkItem.BUYRT = 0;
                            storeItemModel.StkItem.SALRT = 0;
                            storeItemModel.StkItem.STKMIN = 0;
                            storeItemModel.StkItem.DISCRT = 0;
                            storeItemModel.StkItem.CPQTY = 0;
                            storeItemModel.StkItem.REMARKS = "";


                        }
                        else
                        {
                            TempData["message"] = "Item entry not possible";
                            TempData["ShowAddButton"] = "Show Add Button";

                        }
                    }

                }
                catch (Exception ex)
                {

                }
                TempData["ShowAddButton"] = "Show Add Button";
                TempData["category"] = storeItemModel;
                TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                return RedirectToAction("Index");
            }


            if (command == "Submit")
            {

                if (storeItemModel.StkItemmst.CATNM != null)
                {
                    //Get Ip ADDRESS,Time & user PC Name
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];


                    storeItemModel.StkItemmst.USERPC = strHostName;
                    storeItemModel.StkItemmst.INSIPNO = ipAddress.ToString();
                    storeItemModel.StkItemmst.INSTIME = Convert.ToDateTime(td);
                    //Insert User ID save STK_ITEMMST table attribute (INSUSERID) field
                    //storeItemModel.StkItemmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                    storeItemModel.StkItemmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    Int64 companyID = Convert.ToInt64(storeItemModel.StkItemmst.COMPID);


                    Int64 minCategoryId = Convert.ToInt64((from n in db.StkItemmstDbSet where n.COMPID == companyID select n.CATID).Min());
                    //if (aCategoryItemModel.StkItemmst.CATID == null)
                    //{
                    //    aCategoryItemModel.StkItemmst.CATID = minCategoryId;
                    //}


                    var result = db.StkItemmstDbSet.Count(d => d.CATNM == storeItemModel.StkItemmst.CATNM
                                                              && d.COMPID == companyID);
                    if (result == 0)
                    {

                        //.........................................................Create Permission Check
                        //var LoggedCompId = System.Web.HttpContext.Current.Request.Cookies["CI"].Value;
                        //var loggedUserID = System.Web.HttpContext.Current.Request.Cookies["UI"].Value;
                        var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"];
                        var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"];

                        var createStatus = "";

                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
                        string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='StockItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
                            TempData["category"] = storeItemModel;
                            TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                            TempData["ShowAddButton"] = "Show Add Button";
                            TempData["message"] = "Permission not granted!";
                            return RedirectToAction("Index");
                        }
                        //...............................................................................................


                        AslUserco aslUserco = db.AslUsercoDbSet.FirstOrDefault(r => (r.COMPID == companyID));
                        if (aslUserco == null)
                        {
                            TempData["message"] = " User ID not found ";
                            TempData["ShowAddButton"] = "Show Add Button";
                        }
                        else
                        {
                            Int64 maxData = Convert.ToInt64((from n in db.StkItemmstDbSet where n.COMPID == storeItemModel.StkItemmst.COMPID select n.CATID).Max());

                            Int64 R = Convert.ToInt64(storeItemModel.StkItemmst.COMPID + "99");

                            if (maxData == 0)
                            {
                                storeItemModel.StkItemmst.CATID = Convert.ToInt64(storeItemModel.StkItemmst.COMPID + "01");

                                Insert_StkItemMst_LogData(storeItemModel);

                                db.StkItemmstDbSet.Add(storeItemModel.StkItemmst);
                                db.SaveChanges();
                                
                                TempData["message"] = "Category Name: '" + storeItemModel.StkItemmst.CATNM + "' successfully saved.\n Please Create the item List.";
                                TempData["ShowAddButton"] = "Show Add Button";
                                TempData["category"] = storeItemModel;
                                TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                                storeItemModel.StkItem.ITEMNM = "";
                                storeItemModel.StkItem.BRAND = "";
                                storeItemModel.StkItem.UNIT = "";
                                storeItemModel.StkItem.BUYRT = 0;
                                storeItemModel.StkItem.SALRT = 0;
                                storeItemModel.StkItem.STKMIN = 0;
                                storeItemModel.StkItem.DISCRT = 0;
                                storeItemModel.StkItem.CPQTY = 0;
                                storeItemModel.StkItem.REMARKS = "";
                                return RedirectToAction("Index");
                            }
                            else if (maxData <= R)
                            {
                                storeItemModel.StkItemmst.CATID = maxData + 1;

                                Insert_StkItemMst_LogData(storeItemModel);

                                db.StkItemmstDbSet.Add(storeItemModel.StkItemmst);
                                db.SaveChanges();

                                TempData["message"] = "Category Name: '" + storeItemModel.StkItemmst.CATNM + "' successfully saved.\n Please Create the item List. ";
                                TempData["ShowAddButton"] = "Show Add Button";
                                TempData["category"] = storeItemModel;
                                TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                                storeItemModel.StkItem.ITEMNM = "";
                                storeItemModel.StkItem.BRAND = "";
                                storeItemModel.StkItem.UNIT = "";
                                storeItemModel.StkItem.BUYRT = 0;
                                storeItemModel.StkItem.SALRT = 0;
                                storeItemModel.StkItem.STKMIN = 0;
                                storeItemModel.StkItem.DISCRT = 0;
                                storeItemModel.StkItem.CPQTY = 0;
                                storeItemModel.StkItem.REMARKS = "";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["ShowAddButton"] = "Show Add Button";
                                TempData["message"] = "Not possible entry ";
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    else if (result > 0)
                    {
                        var findCategoryID = (from m in db.StkItemmstDbSet
                            where m.COMPID == companyID && m.CATNM == storeItemModel.StkItemmst.CATNM
                            select new {m.CATID}).Distinct().ToList();
                        foreach (var l in findCategoryID)
                        {
                            storeItemModel.StkItemmst.CATID = l.CATID;
                        }

                        //TempData["message"] = "Get the Item List";
                        TempData["ShowAddButton"] = "Show Add Button";
                        TempData["category"] = storeItemModel;
                        TempData["categoryId"] = storeItemModel.StkItemmst.CATID;
                        TempData["latitute_CategoryList"] = storeItemModel.StkItemmst.INSLTUDE;
                        storeItemModel.StkItem.ITEMNM = "";
                        storeItemModel.StkItem.BRAND = "";
                        storeItemModel.StkItem.UNIT = "";
                        storeItemModel.StkItem.BUYRT = 0;
                        storeItemModel.StkItem.SALRT = 0;
                        storeItemModel.StkItem.STKMIN = 0;
                        storeItemModel.StkItem.DISCRT = 0;
                        storeItemModel.StkItem.CPQTY = 0;
                        storeItemModel.StkItem.REMARKS = "";
                        return RedirectToAction("Index");
                    }
                }

                else if (storeItemModel.StkItemmst.CATNM == null && storeItemModel.StkItemmst.REMARKS != null)
                {
                    TempData["CategoryMsg"] = "Please Enter Category Name.";
                    return View("Index");
                }
                else if (storeItemModel.StkItemmst.CATNM == null)
                {
                    TempData["CategoryMsg"] = "Please Enter Category Name!";
                    return RedirectToAction("Index");
                }


            }

            if (command == "Update")
            {
                var query =
                    from a in db.StkItemDbSet
                    where (a.ITEMID == storeItemModel.StkItem.ITEMID && a.COMPID == storeItemModel.StkItemmst.COMPID && a.CATID == storeItemModel.StkItemmst.CATID)
                    select a;
                storeItemModel.StkItem.COMPID = storeItemModel.StkItemmst.COMPID;
                storeItemModel.StkItem.CATID = storeItemModel.StkItemmst.CATID;


                foreach (STK_ITEM a in query)
                {
                    // Insert any additional changes to column values.
                    a.ITEMNM = storeItemModel.StkItem.ITEMNM;
                    a.BRAND = storeItemModel.StkItem.BRAND;
                    a.UNIT = storeItemModel.StkItem.UNIT;
                    a.BUYRT = storeItemModel.StkItem.BUYRT;
                    a.SALRT = storeItemModel.StkItem.SALRT;
                    a.STKMIN = storeItemModel.StkItem.STKMIN;
                    a.DISCRT = storeItemModel.StkItem.DISCRT;
                    a.CPQTY = storeItemModel.StkItem.CPQTY;
                    a.REMARKS = storeItemModel.StkItem.REMARKS;
                    a.UPDIPNO = ipAddress.ToString();
                    a.UPDTIME = td;
                    //a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                    a.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    a.UPDLTUDE = storeItemModel.StkItemmst.INSLTUDE;
                    TempData["StkIemLogData"] = Convert.ToString("Item Name: " + a.ITEMNM + ",\nBrand: " + a.BRAND + ",\nUnit: " + a.UNIT + ",\nBuy Rate: " + a.BUYRT + ",\nSale Rate: " + a.SALRT + ",\nStoke Minimum: " + a.STKMIN + ",\nDisscount: " + a.DISCRT + ",\nCPQTY: " + a.CPQTY + ",\nRemarks: " + a.REMARKS + ".");

                }

                ASL_LOG aslLogref = new ASL_LOG();

                aslLogref.LOGIPNO = ipAddress.ToString();
                aslLogref.COMPID = storeItemModel.StkItem.COMPID;
                aslLogref.LOGLTUDE = storeItemModel.StkItem.INSLTUDE;

                //Update User ID save ASL_ROLE table attribute UPDUSERID
                //aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                aslLogref.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                aslLogref.LOGDATA = TempData["StkIemLogData"].ToString();
                Update_StkItem_LogData(aslLogref);

                db.SaveChanges();

                TempData["category"] = storeItemModel;
                TempData["categoryId"] = storeItemModel.StkItem.CATID;
                TempData["ShowAddButton"] = "Show Add Button";
                storeItemModel.StkItem.ITEMNM = "";
                storeItemModel.StkItem.BRAND = "";
                storeItemModel.StkItem.UNIT = "";
                storeItemModel.StkItem.BUYRT = 0;
                storeItemModel.StkItem.SALRT = 0;
                storeItemModel.StkItem.STKMIN = 0;
                storeItemModel.StkItem.DISCRT = 0;
                storeItemModel.StkItem.CPQTY = 0;
                storeItemModel.StkItem.REMARKS = "";
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }






        public ActionResult EditItemUpdate(Int64 id, Int64 cid, Int64 catid, Int64 itemId, string itemname, PageModel model)
        {
            //.........................................................Create Permission Check
            //var LoggedCompId = System.Web.HttpContext.Current.Request.Cookies["CI"].Value;
            //var loggedUserID = System.Web.HttpContext.Current.Request.Cookies["UI"].Value;
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"];
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"];

            var updateStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
            string query1 = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='StockItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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

            //add the data from database to model
            var categoryName_Remarks = from m in db.StkItemmstDbSet where m.CATID == catid && m.COMPID == cid select m;
            foreach (var categoryResult in categoryName_Remarks)
            {
                model.StkItemmst.COMPID = cid;
                model.StkItemmst.CATID = catid;
                model.StkItemmst.CATNM = categoryResult.CATNM;
                model.StkItemmst.REMARKS = categoryResult.REMARKS;
            }
            TempData["category"] = model;
            TempData["categoryId"] = catid;
            TempData["ShowAddButton"] = null;
            if (updateStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");
            }
            //...............................................................................................

            model.StkItem = db.StkItemDbSet.Find(id);

            var item = from r in db.StkItemDbSet where r.STK_ITEM_ID == id select r.ITEMNM;
            foreach (var it in item)
            {
                model.StkItem.ITEMNM = it.ToString();
            }

            return RedirectToAction("Index");

        }




        public ActionResult ItemDelete(Int64 id, Int64 cid, Int64 catid, Int64 itemId, string itemname, PageModel model)
        {
            //.........................................................Create Permission Check
            //var LoggedCompId = System.Web.HttpContext.Current.Request.Cookies["CI"].Value;
            //var loggedUserID = System.Web.HttpContext.Current.Request.Cookies["UI"].Value;
            var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"];
            var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"];

            var deleteStatus = "";

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Store_GL_DbContext"].ToString());
            string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='StockItem' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
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
            //add the data from database to model
            var categoryName_Remarks = from m in db.StkItemmstDbSet where m.CATID == catid && m.COMPID == cid select m;
            foreach (var categoryResult in categoryName_Remarks)
            {
                model.StkItemmst.COMPID = cid;
                model.StkItemmst.CATID = catid;
                model.StkItemmst.CATNM = categoryResult.CATNM;
                model.StkItemmst.REMARKS = categoryResult.REMARKS;
            }
            TempData["category"] = model;
            TempData["categoryId"] = catid;
            TempData["ShowAddButton"] = "Show Add Button";
            if (deleteStatus == 'I'.ToString())
            {
                TempData["message"] = "Permission not granted!";
                return RedirectToAction("Index");

            }
            //...............................................................................................

            STK_ITEM stkitem = db.StkItemDbSet.Find(id);
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            stkitem.USERPC = strHostName;
            stkitem.UPDIPNO = ipAddress.ToString();
            stkitem.UPDTIME = Convert.ToDateTime(td);
            //Delete User ID save STK_ITEMMST table attribute (UPDUSERID) field
            //stkitem.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
            stkitem.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            if (TempData["latitute_CategoryList"] != null)
            {
                //Get current LOGLTUDE data 
                stkitem.UPDLTUDE = TempData["latitute_CategoryList"].ToString();
            }

            Delete_STK_ITEM_LogData(stkitem);
            Delete_ASL_DELETE_STK_ITEM(stkitem);
            db.StkItemDbSet.Remove(stkitem);
            db.SaveChanges();


            model.StkItem.ITEMNM = "";
            model.StkItem.BRAND = "";
            model.StkItem.UNIT = "";
            model.StkItem.BUYRT = 0;
            model.StkItem.SALRT = 0;
            model.StkItem.STKMIN = 0;
            model.StkItem.DISCRT = 0;
            model.StkItem.CPQTY = 0;
            model.StkItem.REMARKS = "";
            return RedirectToAction("Index");
        }



        //
        // GET: /CategoryItemModel/
        public ActionResult ShowCategoryList()
        {
            ViewData["HighLight_Menu_Settings"] = "Heigh Light Menu";
            ViewData["HighLight_Menu_BillingForm"] = null;
            //Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["CI"].Value);
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var result = (from n in db.StkItemmstDbSet
                          where n.COMPID == compid
                          select n
                     );
            return View(result);
        }

        //
        // GET: /CategoryItemModel

        public ActionResult EditCategoryList(int id = 0)
        {
            ViewData["HighLight_Menu_Settings"] = "Heigh Light Menu";
            ViewData["HighLight_Menu_BillingForm"] = null;
            STK_ITEMMST stkItemmst = db.StkItemmstDbSet.Find(id);
            if (stkItemmst == null)
            {
                return HttpNotFound();
            }
            return View(stkItemmst);
        }


        // POST: /CategoryItemModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategoryList(STK_ITEMMST stkItemmst, string Command)
        {
            if (Command == "Update")
            {
                if (ModelState.IsValid)
                {
                    //Get Ip ADDRESS,Time & user PC Name
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];


                    stkItemmst.UPDIPNO = ipAddress.ToString();
                    stkItemmst.UPDTIME = Convert.ToDateTime(td);

                    //Insert User ID save ASL_MENUMST table attribute INSUSERID
                    //stkItemmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                    stkItemmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    Update_Stk_ItemMst_LogData(stkItemmst);

                    db.Entry(stkItemmst).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["UpdateCategoryInfo"] = "Category Name: '" + stkItemmst.CATNM + "' update successfully!";
                    return RedirectToAction("ShowCategoryList");
                }
                return View(stkItemmst);
            }
            else
            {
                return RedirectToAction("ShowCategoryList");
            }
        }




        //
        // GET: /CategoryItemModel

        public ActionResult DeleteCategory(int id = 0)
        {
            ViewData["HighLight_Menu_Settings"] = "Heigh Light Menu";
            ViewData["HighLight_Menu_BillingForm"] = null;
            STK_ITEMMST stkItemmst = db.StkItemmstDbSet.Find(id);
            if (stkItemmst == null)
            {
                return HttpNotFound();
            }
            return View(stkItemmst);
        }

        //
        // POST: /CategoryItemModel

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id, STK_ITEMMST Stk_Itemmst_Delete, String Command)
        {
            if (Command == "Yes")
            {
                STK_ITEMMST stkItemmst = db.StkItemmstDbSet.Find(id);
                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                stkItemmst.USERPC = strHostName;
                stkItemmst.UPDIPNO = ipAddress.ToString();
                stkItemmst.UPDTIME = Convert.ToDateTime(td);
                //Delete User ID save STK_ITEMMST table attribute (UPDUSERID) field
                //stkItemmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Request.Cookies["UI"].Value);
                stkItemmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                stkItemmst.UPDLTUDE = Stk_Itemmst_Delete.UPDLTUDE;

                //Search all information from Menu Table,when it match to the Module ID
                var menuList = (from sub in db.StkItemDbSet select sub)
                    .Where(sub => sub.CATID == stkItemmst.CATID);

                var date = Convert.ToString(td.ToString("dd-MMM-yyyy"));
                var time = Convert.ToString(td.ToString("hh:mm:ss tt"));

                Int64 maxSerialNoDelete =
                    Convert.ToInt64(
                        (from n in db.AslDeleteDbSet
                         where n.COMPID == stkItemmst.COMPID && n.USERID == stkItemmst.UPDUSERID
                         select n.DELSLNO).Max());
                if (maxSerialNoDelete == 0)
                {
                    AslDelete.DELSLNO = Convert.ToInt64("1");
                }
                else
                {
                    AslDelete.DELSLNO = maxSerialNoDelete + 1;
                }
                // Delete ALL INFORMATION from STK_ITEM TO Asl_Delete Database Table.
                AslDelete.COMPID = Convert.ToInt64(stkItemmst.COMPID);
                AslDelete.USERID = Convert.ToInt64(stkItemmst.UPDUSERID);
                AslDelete.DELSLNO = AslDelete.DELSLNO;
                AslDelete.DELDATE = Convert.ToString(date);
                AslDelete.DELTIME = Convert.ToString(time);
                AslDelete.DELIPNO = stkItemmst.UPDIPNO;
                AslDelete.DELLTUDE = stkItemmst.UPDLTUDE;
                AslDelete.TABLEID = "STK_ITEM";
                AslDelete.USERPC = stkItemmst.USERPC;
                AslDelete.DELDATA = " ";


                Int64 maxSerialNoLog =
                    Convert.ToInt64(
                        (from n in db.AslLogDbSet
                         where n.COMPID == stkItemmst.COMPID && n.USERID == stkItemmst.UPDUSERID
                         select n.LOGSLNO).Max());
                if (maxSerialNoLog == 0)
                {
                    aslLog.LOGSLNO = Convert.ToInt64("1");
                }
                else
                {
                    aslLog.LOGSLNO = maxSerialNoLog + 1;
                }
                // Delete ALL INFORMATION from STK_ITEM TO Asl_lOG Database Table.
                aslLog.COMPID = Convert.ToInt64(stkItemmst.COMPID);
                aslLog.USERID = Convert.ToInt64(stkItemmst.UPDUSERID);
                aslLog.LOGTYPE = "DELETE";
                aslLog.LOGSLNO = aslLog.LOGSLNO;
                aslLog.LOGDATE = Convert.ToDateTime(date);
                aslLog.LOGTIME = Convert.ToString(time);
                aslLog.LOGIPNO = stkItemmst.UPDIPNO;
                aslLog.LOGLTUDE = stkItemmst.UPDLTUDE;
                aslLog.TABLEID = "STK_ITEM";
                aslLog.USERPC = stkItemmst.USERPC;
                aslLog.LOGDATA = "";

                Int64 serial = 1;
                foreach (var n in menuList)
                {
                    AslDelete.DELDATA = AslDelete.DELDATA +
                                        Convert.ToString("("+serial+")"+"Item Name: " + n.ITEMNM + ",\nBrand: " + n.BRAND +
                                                         ",\nUnit: " + n.UNIT + ",\nBuy Rate: " + n.BUYRT +
                                                         ",\nSale Rate: " + n.SALRT + ",\nStoke Minimum: " + n.STKMIN +
                                                         ",\nDisscount: " + n.DISCRT + ",\nCPQTY" + n.CPQTY + ",\nRemarks: " + n.REMARKS +
                                                         " .\n..................\n");
                    aslLog.LOGDATA = aslLog.LOGDATA +
                                     Convert.ToString("(" + serial + ")" + "Item Name: " + n.ITEMNM + ",\nBrand: " + n.BRAND + ",\nUnit: " +
                                                      n.UNIT + ",\nBuy Rate: " + n.BUYRT + ",\nSale Rate: " + n.SALRT +
                                                      ",\nStoke Minimum: " + n.STKMIN + ",\nDisscount: " + n.DISCRT + ",\nCPQTY" + n.CPQTY +
                                                      ",\nRemarks: " + n.REMARKS + " .\n..................\n");

                    serial += 1;
                    db.StkItemDbSet.Remove(n);

                }
                db.AslDeleteDbSet.Add(AslDelete);
                db.AslLogDbSet.Add(aslLog);
                db.SaveChanges();

                Delete_Stk_ItemMst_LogData(stkItemmst);
                Delete_ASL_DELETE_Stk_ItemMst(stkItemmst);

                db.StkItemmstDbSet.Remove(stkItemmst);
                db.SaveChanges();

                TempData["DeleteCategoryInfo"] = "Category Name: '" + stkItemmst.CATNM + "' delete successfully!";
                return RedirectToAction("ShowCategoryList");
            }
            else
            {
                return RedirectToAction("ShowCategoryList");
            }
        }


        //// Get All Item Information
        //public ActionResult ShowItemList()
        //{
        //    Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
        //    var result = (from n in db.StkItemDbSet
        //                  where n.COMPID == compid
        //                  select n
        //             );
        //    return View(result);
        //}





        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ItemNameChanged(string changedText)
        {
            //var compid = Convert.ToInt16(System.Web.HttpContext.Current.Request.Cookies["CI"].Value);
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"]);
            String itemId = "";
            string remarks = "";
            var rt = db.StkItemmstDbSet.Where(n => n.CATNM == changedText &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             catid = n.CATID,
                                                             rmks = n.REMARKS
                                                         });
            foreach (var n in rt)
            {
                itemId = Convert.ToString(n.catid);
                remarks = Convert.ToString(n.rmks);
            }

            var result = new { catid = itemId, rmrks = remarks };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            //var compid = Convert.ToInt16(System.Web.HttpContext.Current.Request.Cookies["CI"].Value);
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"]);


            var tags = from p in db.StkItemmstDbSet
                       where p.COMPID == compid
                       select p.CATNM;

            return this.Json(tags.Where(t => t.StartsWith(term)),
                            JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
