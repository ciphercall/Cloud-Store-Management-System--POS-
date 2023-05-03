using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.Models;

namespace AS_Store_GL.Controllers
{
    public class SaleReturnController : AppController  
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

        public SaleReturnController()
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



        // Create ASL_LOG object and it used to this Insert_Asl_LogData (RMS_TRANSMST rmsTransmst).
        public ASL_LOG aslLog = new ASL_LOG();

        // SAVE ALL INFORMATION from RmsTransMst TO Asl_lOG Database Table.
        public void Insert_StkTransMst_LogData(STK_TRANSMST aStkTransmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == compid && n.USERID == aStkTransmst.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(compid);
            aslLog.USERID = aStkTransmst.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aStkTransmst.INSIPNO;
            aslLog.LOGLTUDE = aStkTransmst.INSLTUDE;
            aslLog.TABLEID = "STK_TRANSMST";

            string storeName = "", PartyName = "";
            var findStoreName = (from n in db.StkStoreDbSet where n.COMPID == compid && n.STOREID == aStkTransmst.STORETO select n).ToList();
            foreach (var x in findStoreName)
            {
                storeName = x.STORENM.ToString();
            }

            var findPartyID = (from n in db.GlAcchartDbSet where n.COMPID == compid && n.ACCOUNTCD == aStkTransmst.PSID select n).ToList();
            foreach (var y in findPartyID)
            {
                PartyName = y.ACCOUNTNM.ToString();
            }

            aslLog.LOGDATA = Convert.ToString("Transaction Date:" + aStkTransmst.TRANSDT + ",\nYear:" + aStkTransmst.TRANSYY + ",\nTransaction type:" + aStkTransmst.TRANSTP + ",\nMemo NO:" + aStkTransmst.TRANSNO + ",\nStore To:" + storeName + ",\nParty:" + PartyName + ",\nTotal Amount:" + aStkTransmst.TOTAMT + ",\nDiscount Rate: " + aStkTransmst.DISCRTG + ",\nDiscount Amount: " + aStkTransmst.DISCAMTG + ",\nTotal Gross: " + aStkTransmst.TOTGROSS + ",\nVat Rate: " + aStkTransmst.VATRT + ",\nVat Amount: " + aStkTransmst.VATAMT + ",\nOther Charge: " + aStkTransmst.OTCAMT + ",\nTotal Net: " + aStkTransmst.TOTNET + ",\nCash Amount: " + aStkTransmst.CASHAMT + ",\nCredit Amount: " + aStkTransmst.CREDITAMT + ",\nRemarks: " + aStkTransmst.REMARKS + ".");
            aslLog.USERPC = aStkTransmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Update_StkTransMst_LogData(STK_TRANSMST aStkTransmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == compid && n.USERID == aStkTransmst.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(compid);
            aslLog.USERID = aStkTransmst.INSUSERID;
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aStkTransmst.INSIPNO;
            aslLog.LOGLTUDE = aStkTransmst.INSLTUDE;
            aslLog.TABLEID = "STK_TRANSMST";

            string storeName = "", PartyName = "";
            var findStoreName = (from n in db.StkStoreDbSet where n.COMPID == compid && n.STOREID == aStkTransmst.STORETO select n).ToList();
            foreach (var x in findStoreName)
            {
                storeName = x.STORENM.ToString();
            }

            var findPartyID = (from n in db.GlAcchartDbSet where n.COMPID == compid && n.ACCOUNTCD == aStkTransmst.PSID select n).ToList();
            foreach (var y in findPartyID)
            {
                PartyName = y.ACCOUNTNM.ToString();
            }
            aslLog.LOGDATA = Convert.ToString("Transaction Date:" + aStkTransmst.TRANSDT + ",\nYear:" + aStkTransmst.TRANSYY + ",\nTransaction type:" + aStkTransmst.TRANSTP + ",\nMemo NO:" + aStkTransmst.TRANSNO + ",\nStore To:" + storeName + ",\nParty:" + PartyName + ",\nTotal Amount:" + aStkTransmst.TOTAMT + ",\nDiscount Rate: " + aStkTransmst.DISCRTG + ",\nDiscount Amount: " + aStkTransmst.DISCAMTG + ",\nTotal Gross: " + aStkTransmst.TOTGROSS + ",\nVat Rate: " + aStkTransmst.VATRT + ",\nVat Amount: " + aStkTransmst.VATAMT + ",\nOther Charge: " + aStkTransmst.OTCAMT + ",\nTotal Net: " + aStkTransmst.TOTNET + ",\nCash Amount: " + aStkTransmst.CASHAMT + ",\nCredit Amount: " + aStkTransmst.CREDITAMT + ",\nRemarks: " + aStkTransmst.REMARKS + ".");
            aslLog.USERPC = aStkTransmst.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }





        public void Delete_StkTransMst_LogData(STK_TRANSMST aStkTransmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 userID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"].ToString());
            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == compid && n.USERID == userID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(compid);
            aslLog.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"].ToString());
            aslLog.LOGTYPE = "DELETE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = ipAddress.ToString();
            aslLog.LOGLTUDE = aStkTransmst.INSLTUDE;
            aslLog.TABLEID = "STK_TRANSMST";

            string storeName = "", PartyName = "";
            var findStoreName = (from n in db.StkStoreDbSet where n.COMPID == compid && n.STOREID == aStkTransmst.STORETO select n).ToList();
            foreach (var x in findStoreName)
            {
                storeName = x.STORENM.ToString();
            }

            var findPartyID = (from n in db.GlAcchartDbSet where n.COMPID == compid && n.ACCOUNTCD == aStkTransmst.PSID select n).ToList();
            foreach (var y in findPartyID)
            {
                PartyName = y.ACCOUNTNM.ToString();
            }

            aslLog.LOGDATA = Convert.ToString("Delete also STK_Trans Table Data! " + "\nTransaction Date:" + aStkTransmst.TRANSDT + ",\nYear:" + aStkTransmst.TRANSYY + ",\nTransaction type:" + aStkTransmst.TRANSTP + ",\nMemo NO:" + aStkTransmst.TRANSNO + ",\nStore To:" + storeName + ",\nParty:" + PartyName + ",\nTotal Amount:" + aStkTransmst.TOTAMT + ",\nDiscount Rate: " + aStkTransmst.DISCRTG + ",\nDiscount Amount: " + aStkTransmst.DISCAMTG + ",\nTotal Gross: " + aStkTransmst.TOTGROSS + ",\nVat Rate: " + aStkTransmst.VATRT + ",\nVat Amount: " + aStkTransmst.VATAMT + ",\nOther Charge: " + aStkTransmst.OTCAMT + ",\nTotal Net: " + aStkTransmst.TOTNET + ",\nCash Amount: " + aStkTransmst.CASHAMT + ",\nCredit Amount: " + aStkTransmst.CREDITAMT + ",\nRemarks: " + aStkTransmst.REMARKS + ".");
            aslLog.USERPC = strHostName;
            db.AslLogDbSet.Add(aslLog);
        }





        public ASL_DELETE AslDelete = new ASL_DELETE();
        public void Delete_STK_TransMst_DELETE(STK_TRANSMST stkTransmst)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("HH:mm:ss tt"));

            Int64 userID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"].ToString());
            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == stkTransmst.COMPID && n.USERID == userID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(stkTransmst.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"].ToString());
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = ipAddress.ToString();
            AslDelete.DELLTUDE = stkTransmst.UPDLTUDE;
            AslDelete.TABLEID = "STK_TRANSMST";

            string storeName = "", PartyName = "";
            var findStoreName = (from n in db.StkStoreDbSet where n.COMPID == compid && n.STOREID == stkTransmst.STORETO select n).ToList();
            foreach (var x in findStoreName)
            {
                storeName = x.STORENM.ToString();
            }

            var findPartyID = (from n in db.GlAcchartDbSet where n.COMPID == compid && n.ACCOUNTCD == stkTransmst.PSID select n).ToList();
            foreach (var y in findPartyID)
            {
                PartyName = y.ACCOUNTNM.ToString();
            }

            AslDelete.DELDATA = Convert.ToString("Delete also STK_Trans Table Data! " + "\nTransaction Date:" + stkTransmst.TRANSDT + ",\nYear:" + stkTransmst.TRANSYY + ",\nTransaction type:" + stkTransmst.TRANSTP + ",\nMemo NO:" + stkTransmst.TRANSNO + ",\nStore To:" + storeName + ",\nParty:" + PartyName + ",\nTotal Amount:" + stkTransmst.TOTAMT + ",\nDiscount Rate: " + stkTransmst.DISCRTG + ",\nDiscount Amount: " + stkTransmst.DISCAMTG + ",\nTotal Gross: " + stkTransmst.TOTGROSS + ",\nVat Rate: " + stkTransmst.VATRT + ",\nVat Amount: " + stkTransmst.VATAMT + ",\nOther Charge: " + stkTransmst.OTCAMT + ",\nTotal Net: " + stkTransmst.TOTNET + ",\nCash Amount: " + stkTransmst.CASHAMT + ",\nCredit Amount: " + stkTransmst.CREDITAMT + ",\nRemarks: " + stkTransmst.REMARKS + ".");
            AslDelete.USERPC = strHostName;
            db.AslDeleteDbSet.Add(AslDelete);
        }









        // GET: /Transaction/
        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public ActionResult Index()
        {
            var dt = (PageModel)TempData["data"];
            return View(dt);
        }






        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(PageModel model, string command)
        {
            if (model.StkTrans.TRANSDT != null)
            {
                DateTime transDateTime = Convert.ToDateTime(model.StkTrans.TRANSDT);
                string converttoString = Convert.ToString(transDateTime.ToString("dd-MMM-yyyy"));
                string getYear = converttoString.Substring(7, 4);
                model.StkTrans.TRANSYY = Convert.ToInt64(getYear);
            }

            if (command == "Add")
            {
                //Permission Check
                Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                var checkPermission = from role in db.AslRoleDbSet
                                      where role.USERID == loggedUserID && role.CONTROLLERNAME == "SaleReturn" && role.ACTIONNAME == "Index"
                                      select new { role.INSERTR };
                string Insert = "";
                foreach (var VARIABLE in checkPermission)
                {
                    Insert = VARIABLE.INSERTR;
                }

                if (Insert == "I")
                {
                    ViewBag.InsertPermission = "Permission Denied !";
                    return View("Index");
                }


                if (model.StkTrans.TRANSDT == null)
                {
                    ViewBag.errorDate = "Please select date!";
                    return View("Index");
                }


                if (model.StkStore.STOREID == null && model.AGL_acchart.ACCOUNTCD == null)
                {
                    ViewBag.errorStoreName = "Please select a store name!";
                    ViewBag.errorParty = "Please select a party name!";
                    return View("Index");
                }
                else if (model.StkStore.STOREID == null)
                {
                    ViewBag.errorStoreName = "Please select a store name!";
                    return View("Index");
                }
                else if (model.AGL_acchart.ACCOUNTCD == null)
                {
                    ViewBag.errorParty = "Please select a party name!";
                    return View("Index");
                }


                //Validation Check
                if ((model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0) && model.StkTrans.QTY == null)
                {
                    //ViewBag.TableNO = "Table name required!";
                    ViewBag.errorItemid = "Please select a valid item name !";
                    ViewBag.errorQty = "Please select a valid quantity !";
                    return View("Index");
                }
                else if (model.StkTrans.QTY == null && (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0))
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    return View("Index");
                }
                else if (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0)
                {
                    //ViewBag.TableNO = "Table name required!";
                    ViewBag.errorItemid = "Please select a valid item name!";
                    return View("Index");
                }
                else if (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0)
                {
                    ViewBag.errorItemid = "Please select a valid item name!";
                    TempData["transdate"] = model.StkTrans.TRANSDT;
                    TempData["data"] = model;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    return View("Index");
                }
                else if (model.StkTrans.QTY == null)
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    TempData["transdate"] = model.StkTrans.TRANSDT;
                    TempData["data"] = model;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    return View("Index");
                }

                model.StkTrans.USERPC = strHostName;
                model.StkTrans.INSIPNO = ipAddress.ToString();
                model.StkTrans.INSTIME = td;
                model.StkTrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.StkTrans.TRANSTP = "IRTS";
                model.StkTrans.STORETO = model.StkStore.STOREID;
                model.StkTrans.PSID = model.AGL_acchart.ACCOUNTCD;


                var res = db.StkTransDbSet.Where(a => a.TRANSNO == model.StkTrans.TRANSNO && a.TRANSTP == "IRTS" && a.TRANSYY == model.StkTrans.TRANSYY && a.COMPID == compid).Count(a => a.ITEMID == model.StkTrans.ITEMID) == 0;

                if (res == true)
                {
                    var sid = db.StkTransDbSet.Where(x => x.TRANSNO == model.StkTrans.TRANSNO && x.TRANSTP == "IRTS" && x.TRANSYY == model.StkTrans.TRANSYY && x.COMPID == compid)
                                  .Max(o => o.ITEMSL);
                    var transno_Max = db.StkTransDbSet.Where(x => x.TRANSYY == model.StkTrans.TRANSYY && x.TRANSTP == "IRTS" && x.COMPID == compid)
                        .Max(s => s.TRANSNO);
                    string transno = Convert.ToString(transno_Max);
                    if (model.StkTrans.TRANSNO == null)
                    {
                        if (transno == "")
                        {
                            transno = Convert.ToString(model.StkTrans.TRANSYY + "0001");
                            model.StkTrans.TRANSNO = Convert.ToInt64(transno);
                            TempData["transno"] = transno;
                        }
                        else
                        {
                            Int64 convertTransNO = Convert.ToInt64(transno.Substring(4, 4));
                            Int64 transNO_Int = convertTransNO + 1;
                            if (transNO_Int < 10)
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + "000" + transNO_Int.ToString());
                            }
                            else if ((10 <= transNO_Int) && (transNO_Int <= 99))
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + "00" + transNO_Int.ToString());
                            }
                            else if ((100 <= transNO_Int) && (transNO_Int <= 999))
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + "0" + transNO_Int.ToString());
                            }
                            else
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + transNO_Int.ToString());
                            }

                            TempData["transno"] = model.StkTrans.TRANSNO;
                        }

                        if (sid == null)
                        {
                            model.StkTrans.ITEMSL = 1;
                        }
                        else
                        {
                            model.StkTrans.ITEMSL = sid + 1;
                        }

                        db.StkTransDbSet.Add(model.StkTrans);
                        db.SaveChanges();

                        model.StkTrans.ITEMSL = 0;
                        model.StkTrans.ITEMID = 0;
                        model.StkTrans.QTY = 0;
                        model.StkTrans.RATE = 0;
                        model.StkTrans.AMOUNT = 0;
                        model.StkTrans.DISCAMT = 0;
                        model.StkTrans.DISCRT = 0;
                        model.StkTrans.GROSSAMT = 0;

                        model.StkItem.ITEMNM = "";
                        TempData["STOREID"] = model.StkStore.STOREID;
                        TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                        TempData["transdate"] = model.StkTrans.TRANSDT;
                        TempData["transYear"] = model.StkTrans.TRANSYY;
                        TempData["data"] = model;
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        TempData["transno"] = model.StkTrans.TRANSNO;
                        model.StkTrans.ITEMSL = Convert.ToInt64(sid) + 1;

                        db.StkTransDbSet.Add(model.StkTrans);
                        db.SaveChanges();

                        model.StkTrans.ITEMSL = 0;
                        model.StkTrans.ITEMID = 0;
                        model.StkTrans.QTY = 0;
                        model.StkTrans.RATE = 0;
                        model.StkTrans.AMOUNT = 0;
                        model.StkTrans.DISCAMT = 0;
                        model.StkTrans.DISCRT = 0;
                        model.StkTrans.GROSSAMT = 0;

                        model.StkItem.ITEMNM = "";
                        TempData["STOREID"] = model.StkStore.STOREID;
                        TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                        TempData["transdate"] = model.StkTrans.TRANSDT;
                        TempData["transYear"] = model.StkTrans.TRANSYY;
                        TempData["data"] = model;
                        return RedirectToAction("Index");
                    }

                }

                else
                {
                    var result = (from n in db.StkTransDbSet
                                  where n.TRANSNO == model.StkTrans.TRANSNO &&
                                        n.COMPID == compid &&
                                        n.ITEMID == model.StkTrans.ITEMID &&
                                        n.TRANSYY == model.StkTrans.TRANSYY && n.TRANSTP == "IRTS"
                                  select new
                                  {
                                      transPID = n.STK_TRANS_ID,
                                      sl = n.ITEMSL
                                  }
                         );

                    foreach (var item in result)
                    {
                        model.StkTrans.STK_TRANS_ID = item.transPID;
                        model.StkTrans.ITEMSL = item.sl;
                    }

                    db.Entry(model.StkTrans).State = EntityState.Modified;
                    db.SaveChanges();

                    model.StkTrans.ITEMSL = 0;
                    model.StkTrans.ITEMID = 0;
                    model.StkTrans.QTY = 0;
                    model.StkTrans.RATE = 0;
                    model.StkTrans.AMOUNT = 0;
                    model.StkTrans.DISCAMT = 0;
                    model.StkTrans.DISCRT = 0;
                    model.StkTrans.GROSSAMT = 0;

                    model.StkItem.ITEMNM = "";
                    TempData["STOREID"] = model.StkStore.STOREID;
                    TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                    TempData["transdate"] = model.StkTrans.TRANSDT;
                    TempData["transYear"] = model.StkTrans.TRANSYY;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    TempData["data"] = model;

                    return RedirectToAction("Index");

                }
                //}

            }

            else if (command == "Save")
            {
                if (model.StkTransmst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }

                var findTransNO = (from n in db.StkTransmstDbSet
                                   where n.TRANSNO == model.StkTrans.TRANSNO && n.COMPID == model.StkTrans.COMPID &&
                          n.TRANSYY == model.StkTrans.TRANSYY && n.TRANSTP == "IRTS"
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {

                }
                else
                {
                    model.StkTransmst.USERPC = strHostName;
                    model.StkTransmst.INSIPNO = ipAddress.ToString();
                    model.StkTransmst.INSTIME = td;
                    model.StkTransmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.StkTransmst.TRANSDT = model.StkTrans.TRANSDT;
                    model.StkTransmst.TRANSYY = model.StkTrans.TRANSYY;
                    model.StkTransmst.COMPID = model.StkTrans.COMPID;
                    model.StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                    model.StkTransmst.TRANSTP = "IRTS";
                    model.StkTransmst.STORETO = model.StkStore.STOREID;
                    model.StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                    Insert_StkTransMst_LogData(model.StkTransmst);
                    db.StkTransmstDbSet.Add(model.StkTransmst);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            else if (command == "A4")
            {
                if (model.StkTransmst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }

                var findTransNO = (from n in db.StkTransmstDbSet
                                   where n.TRANSNO == model.StkTrans.TRANSNO && n.COMPID == model.StkTrans.COMPID &&
                          n.TRANSYY == model.StkTrans.TRANSYY && n.TRANSTP == "IRTS"
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {

                }
                else
                {
                    model.StkTransmst.USERPC = strHostName;
                    model.StkTransmst.INSIPNO = ipAddress.ToString();
                    model.StkTransmst.INSTIME = td;
                    model.StkTransmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.StkTransmst.TRANSDT = model.StkTrans.TRANSDT;
                    model.StkTransmst.TRANSYY = model.StkTrans.TRANSYY;
                    model.StkTransmst.COMPID = model.StkTrans.COMPID;
                    model.StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                    model.StkTransmst.TRANSTP = "IRTS";
                    model.StkTransmst.STORETO = model.StkStore.STOREID;
                    model.StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                    Insert_StkTransMst_LogData(model.StkTransmst);
                    db.StkTransmstDbSet.Add(model.StkTransmst);
                    db.SaveChanges();
                }

                PageModel aPageModel = new PageModel();
                aPageModel.StkTrans.TRANSNO = model.StkTrans.TRANSNO;
                aPageModel.StkTrans.TRANSDT = model.StkTrans.TRANSDT;
                aPageModel.StkTrans.COMPID = model.StkTrans.COMPID;
                aPageModel.StkTransmst.TRANSTP = "IRTS";
                aPageModel.StkTransmst.TRANSYY = model.StkTrans.TRANSYY;
                TempData["Sale_Command"] = command;
                TempData["pageModel"] = aPageModel;
                return RedirectToAction("SaleReturn_Memo", "BillingReport");
            }

            else if (command == "POS")
            {
                if (model.StkTransmst.TOTNET == 0)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("Index");
                }


                 var findTransNO = (from n in db.StkTransmstDbSet
                                   where n.TRANSNO == model.StkTrans.TRANSNO && n.COMPID == model.StkTrans.COMPID &&
                          n.TRANSYY == model.StkTrans.TRANSYY && n.TRANSTP == "IRTS"
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {

                }
                else
                {
                    model.StkTransmst.USERPC = strHostName;
                    model.StkTransmst.INSIPNO = ipAddress.ToString();
                    model.StkTransmst.INSTIME = td;
                    model.StkTransmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.StkTransmst.TRANSDT = model.StkTrans.TRANSDT;
                    model.StkTransmst.TRANSYY = model.StkTrans.TRANSYY;
                    model.StkTransmst.COMPID = model.StkTrans.COMPID;
                    model.StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                    model.StkTransmst.TRANSTP = "IRTS";
                    model.StkTransmst.STORETO = model.StkStore.STOREID;
                    model.StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                    Insert_StkTransMst_LogData(model.StkTransmst);
                    db.StkTransmstDbSet.Add(model.StkTransmst);
                    db.SaveChanges();
                }
                
                PageModel aPageModel = new PageModel();
                aPageModel.StkTrans.TRANSNO = model.StkTrans.TRANSNO;
                aPageModel.StkTrans.TRANSDT = model.StkTrans.TRANSDT;
                aPageModel.StkTrans.COMPID = model.StkTrans.COMPID;
                aPageModel.StkTransmst.TRANSTP = "IRTS";
                aPageModel.StkTransmst.TRANSYY = model.StkTrans.TRANSYY;
                TempData["Sale_Command"] = command;
                TempData["pageModel"] = aPageModel;
                return RedirectToAction("SaleReturn_Memo", "BillingReport");
            }

            else
            {
                return RedirectToAction("Index");
            }
        }



        public ActionResult OrderDelete(Int64 tid, Int64 orderNo, DateTime Date, Int64 Year, Int64 itemsl, PageModel model)
        {
            STK_TRANS stkTrans = db.StkTransDbSet.Find(tid);

            db.StkTransDbSet.Remove(stkTrans);
            db.SaveChanges();

            var result = (from t in db.StkTransDbSet
                          where t.COMPID == compid && t.TRANSNO == orderNo && t.TRANSYY == Year && t.TRANSTP == "IRTS"
                          select new { t.TRANSYY, t.STORETO, t.PSID, t.TRANSDT }
             ).Distinct().ToList();


            foreach (var n in result)
            {
                model.StkTrans.TRANSYY = n.TRANSYY;
                model.StkStore.STOREID = n.STORETO;
                model.AGL_acchart.ACCOUNTCD = n.PSID;
                TempData["STOREID"] = model.StkStore.STOREID;
                TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                TempData["transdate"] = n.TRANSDT;
                TempData["transYear"] = n.TRANSYY;
            }

            if (result.Count == 0)
            {
                TempData["transdate"] = null;
                TempData["transYear"] = null;
                model.StkTrans.TRANSYY = null;
                model.StkTrans.TRANSDT = null;
                orderNo = 0;
            }
            else
            {
                var sid = db.StkTransDbSet.Where(x => x.TRANSNO == model.StkTrans.TRANSNO && x.TRANSYY == Year && x.TRANSTP == "IRTS" && x.COMPID == compid)
                            .Max(o => o.ITEMSL);
                model.StkTrans.TRANSNO = orderNo;
                model.StkTrans.ITEMSL = sid;
                model.StkTrans.TRANSDT = Date;
            }



            TempData["data"] = model;
            TempData["transno"] = orderNo;
            return RedirectToAction("Index");
        }




        public ActionResult OrderUpdate(Int64 tid, Int64 orderNo, DateTime Date, Int64 Year, Int64 itemsl, Int64 itemId, PageModel model)
        {
            model.StkTrans = db.StkTransDbSet.Find(tid);
            model.StkStore.STOREID = model.StkTrans.STORETO;
            model.AGL_acchart.ACCOUNTCD = model.StkTrans.PSID;

            var item = from r in db.StkItemDbSet where r.ITEMID == itemId select r.ITEMNM;
            foreach (var it in item)
            {
                model.StkItem.ITEMNM = it.ToString();
            }

            model.StkTrans.TRANSDT = Date;
            model.StkTrans.TRANSYY = Year;
            TempData["STOREID"] = model.StkStore.STOREID;
            TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
            TempData["transdate"] = model.StkTrans.TRANSDT;
            TempData["transYear"] = model.StkTrans.TRANSYY;
            TempData["data"] = model;
            TempData["transno"] = model.StkTrans.TRANSNO;
            return RedirectToAction("Index");

        }




        //[AcceptVerbs("POST")]
        //public ActionResult OrderComplete(PageModel model)
        //{
        //    return RedirectToAction("Index");
        //}










        [AcceptVerbs("GET")]
        [ActionName("EditOrder")]
        public ActionResult EditOrder()
        {
            //Permission Check
            Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            var checkPermission = from role in db.AslRoleDbSet
                                  where role.USERID == loggedUserID && role.CONTROLLERNAME == "SaleReturn" && role.ACTIONNAME == "Index"
                                  select new { role.UPDATER };
            string Update = "";
            foreach (var VARIABLE in checkPermission)
            {
                Update = VARIABLE.UPDATER;
            }

            if (Update == "I")
            {
                TempData["UpdatePermission"] = "Update Permission Denied !";
                return RedirectToAction("Index");
            }
            var dt = (PageModel)TempData["data"];
            return View(dt);
        }





        [AcceptVerbs("POST")]
        [ActionName("EditOrder")]
        public ActionResult EditOrder(PageModel model, string command)
        {
            if (command == "Add")
            {
                //Validation Check
                if (model.StkTrans.TRANSDT == null && model.StkTrans.QTY == null && (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0) && model.StkTrans.TRANSNO == null)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSDT == null && model.StkTrans.QTY == null && (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0))
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.StkTrans.QTY == null && (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0) && model.StkTrans.TRANSNO == null)
                {
                    ViewBag.errorQty = "Please select a valid quantity !";
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    ViewBag.MemoNO = "Please select a Memo NO!";
                    string date = model.StkTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSDT == null)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    TempData["data"] = model;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.StkTrans.ITEMID == null || model.StkTrans.ITEMID == 0)
                {
                    ViewBag.errorItemid = "Please Select a Valid Item Name !";

                    string date = model.StkTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;

                    TempData["data"] = model;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.StkTrans.QTY == null)
                {
                    ViewBag.errorQty = "Please select a valid quantity !";

                    string date = model.StkTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.MemoNO = "Please select a Memo NO!";

                    string date = model.StkTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.MemoNO = "Please select a Memo NO!";

                    string date = model.StkTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["transdate"] = currentDate;
                    TempData["data"] = model;
                    return View("EditOrder");
                }


                var res = db.StkTransDbSet.Where(a => a.TRANSDT == model.StkTrans.TRANSDT && a.TRANSNO == model.StkTrans.TRANSNO && a.TRANSTP == "IRTS" && a.COMPID == compid).Count(a => a.ITEMID == model.StkTrans.ITEMID) == 0;
                if (res == true)
                {
                    //Permission Check
                    Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    var checkPermission = from role in db.AslRoleDbSet
                                          where role.USERID == loggedUserID && role.CONTROLLERNAME == "Sale" && role.ACTIONNAME == "Index"
                                          select new { role.INSERTR };
                    string Insert = "";
                    foreach (var VARIABLE in checkPermission)
                    {
                        Insert = VARIABLE.INSERTR;
                    }

                    if (Insert == "I")
                    {
                        ViewBag.InsertPermission = "Permission Denied !";
                        return View("Index");
                    }





                    var get_TRANSDT_TRANSYY = (from m in db.StkTransDbSet
                                               where m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSTP == "IRTS"
                                               select new { m.TRANSDT, m.TRANSYY }).Distinct().ToList();
                    foreach (var VARIABLE in get_TRANSDT_TRANSYY)
                    {
                        model.StkTrans.TRANSDT = VARIABLE.TRANSDT;
                        model.StkTrans.TRANSYY = VARIABLE.TRANSYY;
                    }

                    model.StkTrans.USERPC = strHostName;
                    model.StkTrans.INSIPNO = ipAddress.ToString();
                    model.StkTrans.INSTIME = td;
                    model.StkTrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.StkTrans.TRANSTP = "IRTS";
                    model.StkTrans.STORETO = model.StkStore.STOREID;
                    model.StkTrans.PSID = model.AGL_acchart.ACCOUNTCD;

                    var sid = db.StkTransDbSet.Where(x => x.TRANSNO == model.StkTrans.TRANSNO && x.TRANSTP == "IRTS" && x.COMPID == compid)
                               .Max(o => o.ITEMSL);
                    var transno_Max = db.StkTransDbSet.Where(x => x.TRANSYY == model.StkTrans.TRANSYY && x.TRANSTP == "IRTS" && x.COMPID == compid)
                        .Max(s => s.TRANSNO);

                    string transno = Convert.ToString(transno_Max);
                    if (model.StkTrans.TRANSNO == null)
                    {
                        if (transno == "")
                        {
                            transno = Convert.ToString(model.StkTrans.TRANSYY + "0001");
                            model.StkTrans.TRANSNO = Convert.ToInt64(transno);
                            TempData["transno"] = transno;
                        }
                        else
                        {
                            Int64 convertTransNO = Convert.ToInt64(transno.Substring(4, 4));
                            Int64 transNO_Int = convertTransNO + 1;
                            if (transNO_Int < 10)
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + "000" + transNO_Int.ToString());
                            }
                            else if ((10 <= transNO_Int) && (transNO_Int <= 99))
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + "00" + transNO_Int.ToString());
                            }
                            else if ((100 <= transNO_Int) && (transNO_Int <= 999))
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + "0" + transNO_Int.ToString());
                            }
                            else
                            {
                                model.StkTrans.TRANSNO = Convert.ToInt64(transno.Substring(0, 4) + transNO_Int.ToString());
                            }

                            TempData["transno"] = model.StkTrans.TRANSNO;
                        }

                        if (sid == null)
                        {
                            model.StkTrans.ITEMSL = 1;
                        }
                        else
                        {
                            model.StkTrans.ITEMSL = sid + 1;
                        }

                        db.StkTransDbSet.Add(model.StkTrans);
                        db.SaveChanges();

                        model.StkTrans.ITEMSL = 0;
                        model.StkTrans.ITEMID = 0;
                        model.StkTrans.QTY = 0;
                        model.StkTrans.RATE = 0;
                        model.StkTrans.AMOUNT = 0;
                        model.StkTrans.DISCAMT = 0;
                        model.StkTrans.DISCRT = 0;
                        model.StkTrans.GROSSAMT = 0;

                        model.StkItem.ITEMNM = "";
                        TempData["STOREID"] = model.StkStore.STOREID;
                        TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                        TempData["transdate"] = model.StkTrans.TRANSDT;
                        TempData["transYear"] = model.StkTrans.TRANSYY;
                        TempData["data"] = model;
                        return RedirectToAction("EditOrder");
                    }

                    else
                    {
                        TempData["transno"] = model.StkTrans.TRANSNO;
                        model.StkTrans.ITEMSL = Convert.ToInt64(sid) + 1;

                        db.StkTransDbSet.Add(model.StkTrans);
                        db.SaveChanges();

                        //Discount rate, Vat rate, Service charge pass the value.
                        var transMst = (from m in db.StkTransmstDbSet
                                        where m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSDT == model.StkTrans.TRANSDT && m.TRANSTP == "IRTS"
                                        select new { m.STK_TRANSMST_ID, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.OTCAMT, m.TOTNET ,m.REMARKS}).ToList();

                        if (transMst.Count != 0)
                        {
                            foreach (var a in transMst)
                            {
                                TempData["HidendiscountRate"] = a.DISCRTG;
                                TempData["HidenVatRate"] = a.VATRT;
                                TempData["HidenServiceCharge"] = a.OTCAMT;
                                model.StkTransmst.REMARKS = a.REMARKS;
                            }

                        }

                        model.StkTrans.ITEMSL = 0;
                        model.StkTrans.ITEMID = 0;
                        model.StkTrans.QTY = 0;
                        model.StkTrans.RATE = 0;
                        model.StkTrans.AMOUNT = 0;
                        model.StkTrans.DISCAMT = 0;
                        model.StkTrans.DISCRT = 0;
                        model.StkTrans.GROSSAMT = 0;

                        model.StkItem.ITEMNM = "";

                        string date = model.StkTrans.TRANSDT.ToString();
                        DateTime MyDateTime = DateTime.Parse(date);
                        string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                        TempData["STOREID"] = model.StkStore.STOREID;
                        TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                        TempData["transdate"] = currentDate;
                        TempData["transYear"] = model.StkTrans.TRANSYY;
                        TempData["data"] = model;
                        return RedirectToAction("EditOrder");
                    }

                }

                else
                {
                    var result = (from n in db.StkTransDbSet
                                  where n.TRANSNO == model.StkTrans.TRANSNO &&
                                        n.COMPID == compid &&
                                        n.ITEMID == model.StkTrans.ITEMID &&
                                        n.TRANSDT == model.StkTrans.TRANSDT && n.TRANSTP == "IRTS"
                                  select new
                                  {
                                      transPID = n.STK_TRANS_ID,
                                      sl = n.ITEMSL,
                                      year = n.TRANSYY,
                                      type = n.TRANSTP,
                                      InsertUserId = n.INSUSERID,
                                      InsertTime = n.INSTIME,
                                      InsertIpNo = n.INSIPNO,
                                  }
                           );

                    foreach (var item in result)
                    {
                        model.StkTrans.STK_TRANS_ID = item.transPID;
                        model.StkTrans.ITEMSL = item.sl;
                        model.StkTrans.TRANSYY = item.year;
                        model.StkTrans.TRANSTP = item.type;

                        model.StkTrans.USERPC = strHostName;
                        model.StkTrans.INSUSERID = item.InsertUserId;
                        model.StkTrans.INSTIME = item.InsertTime;
                        model.StkTrans.INSIPNO = item.InsertIpNo;
                        model.StkTrans.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        model.StkTrans.UPDLTUDE = model.StkTrans.INSLTUDE;
                        model.StkTrans.UPDIPNO = ipAddress.ToString();
                        model.StkTrans.UPDTIME = td;

                        model.StkTrans.STORETO = model.StkStore.STOREID;
                        model.StkTrans.PSID = model.AGL_acchart.ACCOUNTCD;
                    }

                    db.Entry(model.StkTrans).State = EntityState.Modified;
                    db.SaveChanges();


                    //Discount rate, Vat rate, Service charge pass the value.
                    var transMst = (from m in db.StkTransmstDbSet
                                    where m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSDT == model.StkTrans.TRANSDT && m.TRANSTP == "IRTS"
                                    select new { m.STK_TRANSMST_ID, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.OTCAMT, m.TOTNET,m.REMARKS }).ToList();

                    if (transMst.Count != 0)
                    {
                        foreach (var a in transMst)
                        {
                            TempData["HidendiscountRate"] = a.DISCRTG;
                            TempData["HidenVatRate"] = a.VATRT;
                            TempData["HidenServiceCharge"] = a.OTCAMT;
                            model.StkTransmst.REMARKS = a.REMARKS;
                        }

                    }

                    model.StkTrans.ITEMSL = 0;
                    model.StkTrans.ITEMID = 0;
                    model.StkTrans.QTY = 0;
                    model.StkTrans.RATE = 0;
                    model.StkTrans.AMOUNT = 0;
                    model.StkTrans.DISCAMT = 0;
                    model.StkTrans.DISCRT = 0;
                    model.StkTrans.GROSSAMT = 0;

                    model.StkItem.ITEMNM = "";

                    string date = model.StkTrans.TRANSDT.ToString();
                    DateTime MyDateTime = DateTime.Parse(date);
                    string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                    TempData["STOREID"] = model.StkStore.STOREID;
                    TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                    TempData["transdate"] = currentDate;
                    TempData["transYear"] = model.StkTrans.TRANSYY;
                    TempData["transno"] = model.StkTrans.TRANSNO;
                    TempData["data"] = model;

                    return RedirectToAction("EditOrder");

                }
                //}

            }


            else if (command == "Save")
            {
                //Validation Check
                if (model.StkTrans.TRANSDT == null && model.StkTrans.TRANSNO == null && model.StkTransmst.TOTNET == 0)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }

                //update StkTransMaster table
                var get_TRANSDT_TRANSYY = from m in db.StkTransDbSet
                                          where m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSDT == model.StkTrans.TRANSDT && m.TRANSTP == "IRTS"
                                          select new { m.TRANSDT, m.TRANSYY };
                foreach (var VARIABLE in get_TRANSDT_TRANSYY)
                {
                    model.StkTransmst.TRANSDT = VARIABLE.TRANSDT;
                    model.StkTransmst.TRANSYY = VARIABLE.TRANSYY;
                }

                var findTransNO = (from n in db.StkTransmstDbSet
                                   where n.TRANSNO == model.StkTrans.TRANSNO && n.COMPID == model.StkTrans.COMPID &&
                          n.TRANSYY == model.StkTransmst.TRANSYY && n.TRANSTP == "IRTS"
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (STK_TRANSMST StkTransmst in findTransNO)
                    {
                        StkTransmst.USERPC = strHostName;
                        StkTransmst.UPDLTUDE = StkTransmst.INSLTUDE;
                        StkTransmst.UPDIPNO = ipAddress.ToString();
                        StkTransmst.UPDTIME = td;
                        StkTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        StkTransmst.TRANSDT = StkTransmst.TRANSDT;
                        StkTransmst.TRANSYY = StkTransmst.TRANSYY;

                        StkTransmst.COMPID = model.StkTrans.COMPID;
                        StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                        StkTransmst.TRANSTP = "IRTS";

                        StkTransmst.STORETO = model.StkStore.STOREID;
                        StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                        StkTransmst.TOTAMT = model.StkTransmst.TOTAMT;
                        StkTransmst.DISCRTG = model.StkTransmst.DISCRTG;
                        StkTransmst.DISCAMTG = model.StkTransmst.DISCAMTG;
                        StkTransmst.TOTGROSS = model.StkTransmst.TOTGROSS;
                        StkTransmst.VATRT = model.StkTransmst.VATRT;
                        StkTransmst.VATAMT = model.StkTransmst.VATAMT;
                        StkTransmst.OTCAMT = model.StkTransmst.OTCAMT;
                        StkTransmst.TOTNET = model.StkTransmst.TOTNET;
                        StkTransmst.CASHAMT = model.StkTransmst.CASHAMT;
                        StkTransmst.CREDITAMT = model.StkTransmst.CREDITAMT;
                        StkTransmst.REMARKS = model.StkTransmst.REMARKS;

                        Update_StkTransMst_LogData(StkTransmst);
                    }

                    db.SaveChanges();
                    return RedirectToAction("EditOrder");
                }


                model.StkTransmst.USERPC = strHostName;
                model.StkTransmst.INSIPNO = ipAddress.ToString();
                model.StkTransmst.INSTIME = td;
                model.StkTransmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                model.StkTransmst.COMPID = model.StkTrans.COMPID;
                model.StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                model.StkTransmst.TRANSTP = "IRTS";
                model.StkTransmst.STORETO = model.StkStore.STOREID;
                model.StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                Insert_StkTransMst_LogData(model.StkTransmst);
                db.StkTransmstDbSet.Add(model.StkTransmst);
                db.SaveChanges();
                return RedirectToAction("EditOrder");



            }

            else if (command == "search")
            {
                model.StkTrans.ITEMSL = 0;
                model.StkTrans.ITEMID = 0;
                model.StkTrans.QTY = 0;
                model.StkTrans.RATE = 0;
                model.StkTrans.AMOUNT = 0;
                model.StkTrans.DISCAMT = 0;
                model.StkTrans.DISCRT = 0;
                model.StkTrans.GROSSAMT = 0;
                model.StkItem.ITEMNM = "";

                var transMst = (from m in db.StkTransmstDbSet
                                where m.TRANSDT == model.StkTrans.TRANSDT && m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSTP == "IRTS"
                                select new { m.STK_TRANSMST_ID, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.OTCAMT, m.TOTNET, m.CASHAMT, m.CREDITAMT, m.REMARKS, m.STORETO, m.PSID }).ToList();

                if (transMst != null)
                {
                    foreach (var a in transMst)
                    {
                        TempData["totalAmount"] = a.TOTAMT;
                        TempData["discountRate"] = a.DISCRTG;
                        TempData["discountAmount"] = a.DISCAMTG;
                        TempData["GrossAmount"] = a.TOTGROSS;
                        TempData["VatRate"] = a.VATRT;
                        TempData["VatAmount"] = a.VATAMT;
                        TempData["ServiceCharge"] = a.OTCAMT;
                        TempData["NetAmount"] = a.TOTNET;
                        TempData["CashAmount"] = a.CASHAMT;
                        TempData["CreditAmount"] = a.CREDITAMT;
                        TempData["Remarks"] = a.REMARKS;
                        model.StkTransmst.REMARKS = a.REMARKS;

                        model.StkStore.STOREID = a.STORETO;
                        model.AGL_acchart.ACCOUNTCD = a.PSID;

                    }

                }

                var stk_Trans = (from m in db.StkTransDbSet
                                 where m.TRANSDT == model.StkTrans.TRANSDT && m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSTP == "IRTS"
                                 select new { m.STORETO, m.PSID, m.TRANSYY }).ToList();
                foreach (var s in stk_Trans)
                {
                    model.StkStore.STOREID = s.STORETO;
                    model.AGL_acchart.ACCOUNTCD = s.PSID;
                    model.StkTrans.TRANSYY = s.TRANSYY;
                    break;
                }

                string date = model.StkTrans.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["STOREID"] = model.StkStore.STOREID;
                TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                TempData["transdate"] = currentDate;
                TempData["transYear"] = model.StkTrans.TRANSYY;
                TempData["transno"] = model.StkTrans.TRANSNO;
                TempData["data"] = model;
                return RedirectToAction("EditOrder");
            }

            else if (command == "A4")
            {
                //Validation Check
                if (model.StkTrans.TRANSDT == null && model.StkTrans.TRANSNO == null && model.StkTransmst.TOTNET == 0)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }


                //update RmstransMaster table
                var get_TRANSDT_TRANSYY = from m in db.StkTransDbSet
                                          where m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSDT == model.StkTrans.TRANSDT && m.TRANSTP == "IRTS"
                                          select new { m.TRANSDT, m.TRANSYY };
                foreach (var VARIABLE in get_TRANSDT_TRANSYY)
                {
                    model.StkTransmst.TRANSDT = VARIABLE.TRANSDT;
                    model.StkTransmst.TRANSYY = VARIABLE.TRANSYY;
                }

                var findTransNO = (from n in db.StkTransmstDbSet
                                   where n.TRANSNO == model.StkTrans.TRANSNO && n.COMPID == model.StkTrans.COMPID &&
                          n.TRANSYY == model.StkTransmst.TRANSYY && n.TRANSTP == "IRTS"
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (STK_TRANSMST stkTransmst in findTransNO)
                    {
                        stkTransmst.USERPC = strHostName;
                        stkTransmst.UPDLTUDE = stkTransmst.INSLTUDE;
                        stkTransmst.UPDIPNO = ipAddress.ToString();
                        stkTransmst.UPDTIME = td;
                        stkTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        stkTransmst.TRANSDT = stkTransmst.TRANSDT;
                        stkTransmst.TRANSYY = stkTransmst.TRANSYY;

                        stkTransmst.COMPID = model.StkTrans.COMPID;
                        stkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                        stkTransmst.TRANSTP = "IRTS";

                        stkTransmst.STORETO = model.StkStore.STOREID;
                        stkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                        stkTransmst.TOTAMT = model.StkTransmst.TOTAMT;
                        stkTransmst.DISCRTG = model.StkTransmst.DISCRTG;
                        stkTransmst.DISCAMTG = model.StkTransmst.DISCAMTG;
                        stkTransmst.TOTGROSS = model.StkTransmst.TOTGROSS;
                        stkTransmst.VATRT = model.StkTransmst.VATRT;
                        stkTransmst.VATAMT = model.StkTransmst.VATAMT;
                        stkTransmst.OTCAMT = model.StkTransmst.OTCAMT;
                        stkTransmst.TOTNET = model.StkTransmst.TOTNET;
                        stkTransmst.CASHAMT = model.StkTransmst.CASHAMT;
                        stkTransmst.CREDITAMT = model.StkTransmst.CREDITAMT;
                        stkTransmst.REMARKS = model.StkTransmst.REMARKS;
                        Update_StkTransMst_LogData(stkTransmst);
                    }
                    db.SaveChanges();

                }
                else
                {
                    model.StkTransmst.USERPC = strHostName;
                    model.StkTransmst.INSIPNO = ipAddress.ToString();
                    model.StkTransmst.INSTIME = td;
                    model.StkTransmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    model.StkTransmst.COMPID = model.StkTrans.COMPID;
                    model.StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                    model.StkTransmst.TRANSTP = "IRTS";

                    model.StkTransmst.STORETO = model.StkStore.STOREID;
                    model.StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                    Insert_StkTransMst_LogData(model.StkTransmst);
                    db.StkTransmstDbSet.Add(model.StkTransmst);
                    db.SaveChanges();
                }

                PageModel aPageModel = new PageModel();
                aPageModel.StkTrans.TRANSNO = model.StkTrans.TRANSNO;
                aPageModel.StkTrans.TRANSDT = model.StkTransmst.TRANSDT;
                aPageModel.StkTrans.COMPID = model.StkTrans.COMPID;
                aPageModel.StkTransmst.TRANSTP = "IRTS";
                aPageModel.StkTransmst.TRANSYY = model.StkTransmst.TRANSYY;
                TempData["Sale_Command"] = command;
                TempData["pageModel"] = aPageModel;
                return RedirectToAction("SaleReturn_Memo", "BillingReport");
            }

            else if (command == "POS")
            {
                //Validation Check
                if (model.StkTrans.TRANSDT == null && model.StkTrans.TRANSNO == null && model.StkTransmst.TOTNET == 0)
                {
                    ViewBag.TransactionDate = "Transaction date required!";
                    return View("EditOrder");
                }
                else if (model.StkTrans.TRANSNO == null)
                {
                    ViewBag.addItemList = "Please Add item list!";
                    return View("EditOrder");
                }


                //update RmstransMaster table
                var get_TRANSDT_TRANSYY = from m in db.StkTransDbSet
                                          where m.COMPID == compid && m.TRANSNO == model.StkTrans.TRANSNO && m.TRANSDT == model.StkTrans.TRANSDT && m.TRANSTP == "IRTS"
                                          select new { m.TRANSDT, m.TRANSYY };
                foreach (var VARIABLE in get_TRANSDT_TRANSYY)
                {
                    model.StkTransmst.TRANSDT = VARIABLE.TRANSDT;
                    model.StkTransmst.TRANSYY = VARIABLE.TRANSYY;
                }

                var findTransNO = (from n in db.StkTransmstDbSet
                                   where n.TRANSNO == model.StkTrans.TRANSNO && n.COMPID == model.StkTrans.COMPID &&
                          n.TRANSYY == model.StkTransmst.TRANSYY && n.TRANSTP == "IRTS"
                                   select n).ToList();
                if (findTransNO.Count != 0)
                {
                    foreach (STK_TRANSMST stkTransmst in findTransNO)
                    {
                        stkTransmst.USERPC = strHostName;
                        stkTransmst.UPDLTUDE = stkTransmst.INSLTUDE;
                        stkTransmst.UPDIPNO = ipAddress.ToString();
                        stkTransmst.UPDTIME = td;
                        stkTransmst.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                        stkTransmst.TRANSDT = stkTransmst.TRANSDT;
                        stkTransmst.TRANSYY = stkTransmst.TRANSYY;

                        stkTransmst.COMPID = model.StkTrans.COMPID;
                        stkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                        stkTransmst.TRANSTP = "IRTS";

                        stkTransmst.STORETO = model.StkStore.STOREID;
                        stkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                        stkTransmst.TOTAMT = model.StkTransmst.TOTAMT;
                        stkTransmst.DISCRTG = model.StkTransmst.DISCRTG;
                        stkTransmst.DISCAMTG = model.StkTransmst.DISCAMTG;
                        stkTransmst.TOTGROSS = model.StkTransmst.TOTGROSS;
                        stkTransmst.VATRT = model.StkTransmst.VATRT;
                        stkTransmst.VATAMT = model.StkTransmst.VATAMT;
                        stkTransmst.OTCAMT = model.StkTransmst.OTCAMT;
                        stkTransmst.TOTNET = model.StkTransmst.TOTNET;
                        stkTransmst.CASHAMT = model.StkTransmst.CASHAMT;
                        stkTransmst.CREDITAMT = model.StkTransmst.CREDITAMT;
                        stkTransmst.REMARKS = model.StkTransmst.REMARKS;

                        Update_StkTransMst_LogData(stkTransmst);
                    }
                    db.SaveChanges();

                }
                else
                {
                    model.StkTransmst.USERPC = strHostName;
                    model.StkTransmst.INSIPNO = ipAddress.ToString();
                    model.StkTransmst.INSTIME = td;
                    model.StkTransmst.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    model.StkTransmst.COMPID = model.StkTrans.COMPID;
                    model.StkTransmst.TRANSNO = Convert.ToInt64(model.StkTrans.TRANSNO);
                    model.StkTransmst.TRANSTP = "IRTS";

                    model.StkTransmst.STORETO = model.StkStore.STOREID;
                    model.StkTransmst.PSID = model.AGL_acchart.ACCOUNTCD;

                    Insert_StkTransMst_LogData(model.StkTransmst);
                    db.StkTransmstDbSet.Add(model.StkTransmst);
                    db.SaveChanges();
                }

                PageModel aPageModel = new PageModel();
                aPageModel.StkTrans.TRANSNO = model.StkTrans.TRANSNO;
                aPageModel.StkTrans.TRANSDT = model.StkTransmst.TRANSDT;
                aPageModel.StkTrans.COMPID = model.StkTrans.COMPID;
                aPageModel.StkTransmst.TRANSTP = "IRTS";
                aPageModel.StkTransmst.TRANSYY = model.StkTransmst.TRANSYY;
                TempData["Sale_Command"] = command;
                TempData["pageModel"] = aPageModel;
                return RedirectToAction("SaleReturn_Memo", "BillingReport");
            }

            else if (command == "New")
            {
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("EditOrder");
            }

        }






        public ActionResult EditOrderDelete(Int64 tid, Int64 orderNo, DateTime Date, Int64 Year, Int64 itemsl, PageModel model)
        {
            //Permission Check
            Int64 loggedUserID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            var checkPermission = from role in db.AslRoleDbSet
                                  where role.USERID == loggedUserID && role.CONTROLLERNAME == "SaleReturn" && role.ACTIONNAME == "Index"
                                  select new { role.DELETER };
            string Delete = "";
            foreach (var VARIABLE in checkPermission)
            {
                Delete = VARIABLE.DELETER;
            }
            if (Delete == "I")
            {
                TempData["DeletePermission"] = "Delete Permission Denied !";
                return RedirectToAction("EditOrder");
            }

            STK_TRANS stkTrans = db.StkTransDbSet.Find(tid);
            db.StkTransDbSet.Remove(stkTrans);
            db.SaveChanges();


            var result = (from t in db.StkTransDbSet
                          where t.COMPID == compid && t.TRANSNO == orderNo && t.TRANSDT == Date && t.TRANSYY == Year && t.TRANSTP == "IRTS"
                          select t).ToList();
            //Minimum RmsTrans Table MemoNO delete then RmsTransMst tabel data delete(key-> memoNO, compid)
            if (result.Count == 0)
            {
                var searchRmsTransMst = (from n in db.StkTransmstDbSet where n.COMPID == compid && n.TRANSNO == orderNo && n.TRANSDT == Date && n.TRANSYY == Year && n.TRANSTP == "IRTS" select n).ToList();
                STK_TRANSMST aStkTransmst = new STK_TRANSMST();
                foreach (var m in searchRmsTransMst)
                {
                    aStkTransmst = m;

                }
                if (aStkTransmst != null && searchRmsTransMst.Count != 0)
                {
                    Delete_StkTransMst_LogData(aStkTransmst);
                    Delete_STK_TransMst_DELETE(aStkTransmst);
                    db.StkTransmstDbSet.Remove(aStkTransmst);
                    db.SaveChanges();
                }

            }

            //Discount rate, Vat rate, Service charge pass the value.
            var transMst = (from m in db.StkTransmstDbSet
                            where m.COMPID == compid && m.TRANSNO == orderNo && m.TRANSDT == Date && m.TRANSYY == Year && m.TRANSTP == "IRTS"
                            select new { m.STK_TRANSMST_ID, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.OTCAMT, m.TOTNET,m.REMARKS }).ToList();
            if (transMst.Count != 0)
            {
                foreach (var a in transMst)
                {
                    TempData["HidendiscountRate"] = a.DISCRTG;
                    TempData["HidenVatRate"] = a.VATRT;
                    TempData["HidenServiceCharge"] = a.OTCAMT;
                    model.StkTransmst.REMARKS = a.REMARKS;
                }
            }


            var stk_Trans = (from m in db.StkTransDbSet
                             where m.COMPID == compid && m.TRANSNO == orderNo && m.TRANSDT == Date && m.TRANSYY == Year && m.TRANSTP == "IRTS"
                             select new { m.STORETO, m.PSID }).ToList();
            foreach (var s in stk_Trans)
            {
                model.StkStore.STOREID = s.STORETO;
                model.AGL_acchart.ACCOUNTCD = s.PSID;
                TempData["STOREID"] = model.StkStore.STOREID;
                TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
                break;
            }


            var sid = db.StkTransDbSet.Where(x => x.TRANSNO == model.StkTrans.TRANSNO && x.TRANSDT == Date && x.COMPID == compid && x.TRANSYY == Year && x.TRANSTP == "IRTS").Max(o => o.ITEMSL);
            model.StkTrans.TRANSNO = orderNo;
            model.StkTrans.ITEMSL = sid;
            model.StkTrans.TRANSDT = Date;
            model.StkTrans.TRANSYY = Year;

            if (model.StkTrans.TRANSDT != null)
            {
                string date = model.StkTrans.TRANSDT.ToString();
                DateTime MyDateTime = DateTime.Parse(date);
                string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
                TempData["transdate"] = currentDate;
                TempData["transYear"] = model.StkTrans.TRANSYY;
            }

            TempData["data"] = model;
            TempData["transno"] = orderNo;
            return RedirectToAction("EditOrder");

        }






        public ActionResult EditOrderUpdate(Int64 tid, Int64 orderNo, DateTime Date, Int64 Year, Int64 itemsl, Int64 itemId, PageModel model)
        {
            model.StkTrans = db.StkTransDbSet.Find(tid);
            var item = from r in db.StkItemDbSet where r.ITEMID == itemId select r.ITEMNM;
            foreach (var it in item)
            {
                model.StkItem.ITEMNM = it.ToString();
            }

            //Discount rate, Vat rate, Service charge pass the value.
            var transMst = (from m in db.StkTransmstDbSet
                            where m.COMPID == compid && m.TRANSNO == orderNo && m.TRANSDT == Date && m.TRANSYY == Year && m.TRANSTP == "IRTS"
                            select new { m.STK_TRANSMST_ID, m.TOTAMT, m.DISCRTG, m.DISCAMTG, m.TOTGROSS, m.VATRT, m.VATAMT, m.OTCAMT, m.TOTNET,m.REMARKS }).ToList();

            if (transMst.Count != 0)
            {
                foreach (var a in transMst)
                {
                    TempData["HidendiscountRate"] = a.DISCRTG;
                    TempData["HidenVatRate"] = a.VATRT;
                    TempData["HidenServiceCharge"] = a.OTCAMT;
                    model.StkTransmst.REMARKS = a.REMARKS;
                }

            }

            model.StkStore.STOREID = model.StkTrans.STORETO;
            model.AGL_acchart.ACCOUNTCD = model.StkTrans.PSID;

            string date = model.StkTrans.TRANSDT.ToString();
            DateTime MyDateTime = DateTime.Parse(date);
            string currentDate = MyDateTime.ToString("dd-MMM-yyyy");
            TempData["STOREID"] = model.StkStore.STOREID;
            TempData["ACCOUNTCD"] = model.AGL_acchart.ACCOUNTCD;
            TempData["transdate"] = currentDate;
            TempData["transYear"] = Year;
            TempData["data"] = model;
            TempData["transno"] = model.StkTrans.TRANSNO;

            return RedirectToAction("EditOrder");

        }














        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getYear(DateTime changedtxt)
        {
            string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(7, 4);
            Int64 year = Convert.ToInt64(getYear);
            var result = new { YEAR = year };
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        public JsonResult DateChanged(string theDate)
        {

            DateTime dt = Convert.ToDateTime(theDate);
            DateTime dd = DateTime.Parse(theDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime datetm = Convert.ToDateTime(dd);

            List<SelectListItem> trans = new List<SelectListItem>();
            var transres = (from n in db.StkTransDbSet
                            where n.TRANSDT == dd && n.COMPID == compid && n.TRANSTP == "IRTS"
                            select new
                            {
                                n.TRANSNO
                            }
                            )
                            .Distinct()
                            .ToList();
            string transNO = null;
            foreach (var f in transres)
            {
                if (transNO == null)
                {
                    transNO = Convert.ToString(f.TRANSNO);
                }
                trans.Add(new SelectListItem { Text = f.TRANSNO.ToString(), Value = f.TRANSNO.ToString() });
            }

            var FirsttransNO = new { TransNO = transNO };
            return Json(new SelectList(trans, "Value", "Text"));
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult TransNoChanged(Int64 changedDropDown)
        {
            string tableNumber = "", transType = "";
            DateTime trandate;

            Int64 transno = Convert.ToInt64(changedDropDown);

            TempData["transno"] = transno;

            var rt = db.StkTransDbSet.Where(n => n.TRANSNO == transno && n.COMPID == compid).Select(n => new
            {
                transtype = n.TRANSTP
            });

            foreach (var n in rt)
            {
                transType = n.transtype;
            }

            var result = new { TRANSTP = transType, TRANSNO = transno };
            return Json(result, JsonRequestBehavior.AllowGet);

        }







        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult ItemNameChanged(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"]);
            string itemId = "";
            string rate = "";
            string discount = "";
            decimal qty = 0;
            var rt = db.StkItemDbSet.Where(n => n.ITEMNM == changedText &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             itemid = n.ITEMID,
                                                             rate = n.SALRT,
                                                             discount = n.DISCRT
                                                         });
            foreach (var n in rt)
            {
                itemId = Convert.ToString(n.itemid);
                rate = Convert.ToString(n.rate);
                discount = Convert.ToString(n.discount);
            }

            var result = new { itemid = itemId, rate = rate, discount = discount, qty = 1 };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        //AutoComplete
        public JsonResult TagSearch(string term, string changedDropDown)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"]);
            if (changedDropDown == "")
            {
                var tags = from p in db.StkItemDbSet
                           where p.COMPID == compid
                           select p.ITEMNM;

                return this.Json(tags.Where(t => t.StartsWith(term)),
                           JsonRequestBehavior.AllowGet);
            }
            else
            {
                var catid = Convert.ToInt64(changedDropDown);

                var tags = from p in db.StkItemDbSet
                           where p.COMPID == compid && p.CATID == catid
                           select p.ITEMNM;

                return this.Json(tags.Where(t => t.StartsWith(term)),
                           JsonRequestBehavior.AllowGet);
            }
        }





        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
