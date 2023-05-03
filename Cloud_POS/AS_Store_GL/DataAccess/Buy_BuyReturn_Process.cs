using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using AS_Store_GL.Models;

namespace AS_Store_GL.DataAccess
{
    public static class Buy_BuyReturn_Process
    {
        public static string process(PageModel model, Int64 compid)
        {

            //Datetime formet
            IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime td;


            Store_GL_DbContext db = new Store_GL_DbContext();
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName;
            IPHostEntry ipHostInfo;
            IPAddress ipAddress;

            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            string date = Convert.ToString(model.AGlMaster.TRANSDT);
            DateTime MyDateTime = DateTime.Parse(date);
            string converttoString = MyDateTime.ToString("dd-MMM-yyyy");
            string getYear = converttoString.Substring(9, 2);
            string getMonth = converttoString.Substring(3, 3);
            string Month_Year = getMonth + "-" + getYear;

            //GL Process for STK_TRANS
            var checkDate_STK_TRANS = (from n in db.StkTransmstDbSet where n.TRANSDT == model.AGlMaster.TRANSDT && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();

            if (checkDate_STK_TRANS.Count != 0)
            {
             
                Int64 Buy_SL = 70000, BuyReturn_SL = 80000;
                foreach (var stk_Trans in checkDate_STK_TRANS)
                {
                    //Buy
                    if (stk_Trans.TRANSTP == "BUY")
                    {

                        if (stk_Trans.PSID == Convert.ToInt64(compid + "2030001"))
                        {
                            Buy_SL = Buy_SL + 1;
                            model.AGlMaster.TRANSSL = Buy_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "MPAY";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.tra;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD  = stk_Trans.PSID;
                            model.AGlMaster.CREDITCD = Convert.ToInt64(compid + "1010001");
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase-" + stk_Trans.REMARKS);
                            model.AGlMaster.TRANSDRCR = "DEBIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.DEBITAMT = stk_Trans.TOTNET;
                            model.AGlMaster.CREDITAMT = 0;

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();



                            Buy_SL = Buy_SL + 1;
                            model.AGlMaster.TRANSSL = Buy_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "MPAY";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = Convert.ToInt64(compid + "1010001");
                            model.AGlMaster.CREDITCD = stk_Trans.PSID; 
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase-" + stk_Trans.REMARKS);

                            model.AGlMaster.TRANSDRCR = "CREDIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.DEBITAMT = 0;
                            model.AGlMaster.CREDITAMT = stk_Trans.TOTNET;

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();
                        }
                        else
                        {
                            Buy_SL = Buy_SL + 1;
                            model.AGlMaster.TRANSSL = Buy_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "JOUR";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = Convert.ToInt64(compid + "4010001");
                            model.AGlMaster.CREDITCD = stk_Trans.PSID; 
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase-" + stk_Trans.REMARKS);

                            model.AGlMaster.DEBITAMT = stk_Trans.TOTNET;
                            model.AGlMaster.CREDITAMT = 0;

                            model.AGlMaster.TRANSDRCR = "DEBIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();



                            Buy_SL = Buy_SL + 1;
                            model.AGlMaster.TRANSSL = Buy_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "JOUR";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = stk_Trans.PSID;
                            model.AGlMaster.CREDITCD = Convert.ToInt64(compid + "4010001");
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase-" + stk_Trans.REMARKS);

                            model.AGlMaster.DEBITAMT = 0;
                            model.AGlMaster.CREDITAMT = stk_Trans.TOTNET;

                            model.AGlMaster.TRANSDRCR = "CREDIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();

                            if (stk_Trans.CASHAMT != 0)
                            {
                                Buy_SL = Buy_SL + 1;
                                model.AGlMaster.TRANSSL = Buy_SL;
                                model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                                model.AGlMaster.COMPID = stk_Trans.COMPID;
                                model.AGlMaster.TRANSTP = "MPAY";
                                model.AGlMaster.TRANSMY = Month_Year;
                                model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                                //model.AGlMaster.TRANSFOR = stk_Trans.tra;
                                //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                                //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                                model.AGlMaster.DEBITCD = stk_Trans.PSID;
                                model.AGlMaster.CREDITCD = Convert.ToInt64(compid + "1010001");
                                //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                                //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                                model.AGlMaster.REMARKS = Convert.ToString("Purchase-" + stk_Trans.REMARKS);
                                model.AGlMaster.TRANSDRCR = "DEBIT";
                                model.AGlMaster.TABLEID = "STK_TRANS";

                                model.AGlMaster.DEBITAMT = stk_Trans.CASHAMT;
                                model.AGlMaster.CREDITAMT = 0;

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                                model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                db.SaveChanges();



                                Buy_SL = Buy_SL + 1;
                                model.AGlMaster.TRANSSL = Buy_SL;
                                model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                                model.AGlMaster.COMPID = stk_Trans.COMPID;
                                model.AGlMaster.TRANSTP = "MPAY";
                                model.AGlMaster.TRANSMY = Month_Year;
                                model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                                //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                                //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                                //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                                model.AGlMaster.DEBITCD = Convert.ToInt64(compid + "1010001");
                                model.AGlMaster.CREDITCD = stk_Trans.PSID; 
                                //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                                //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                                model.AGlMaster.REMARKS = Convert.ToString("Purchase-" + stk_Trans.REMARKS);

                                model.AGlMaster.TRANSDRCR = "CREDIT";
                                model.AGlMaster.TABLEID = "STK_TRANS";

                                model.AGlMaster.DEBITAMT = 0;
                                model.AGlMaster.CREDITAMT = stk_Trans.CASHAMT;

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                                model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                db.SaveChanges();
                            }
                        }           
                    }
                    //Buy Return
                    else if (stk_Trans.TRANSTP == "IRTB")
                    {

                        if (stk_Trans.PSID == Convert.ToInt64(compid + "2030001"))
                        {
                            BuyReturn_SL = BuyReturn_SL + 1;
                            model.AGlMaster.TRANSSL = BuyReturn_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "MPAY";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.tra;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = stk_Trans.PSID;
                            model.AGlMaster.CREDITCD = Convert.ToInt64(compid + "1010001");
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase Return-" + stk_Trans.REMARKS);

                            model.AGlMaster.TRANSDRCR = "DEBIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.DEBITAMT = stk_Trans.TOTNET;
                            model.AGlMaster.CREDITAMT = 0;

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();



                            BuyReturn_SL = BuyReturn_SL + 1;
                            model.AGlMaster.TRANSSL = BuyReturn_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "MPAY";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = Convert.ToInt64(compid + "1010001");
                            model.AGlMaster.CREDITCD = stk_Trans.PSID;
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase Return-" + stk_Trans.REMARKS);

                            model.AGlMaster.TRANSDRCR = "CREDIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.DEBITAMT = 0;
                            model.AGlMaster.CREDITAMT = stk_Trans.TOTNET;

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();
                        }
                        else
                        {
                            BuyReturn_SL = BuyReturn_SL + 1;
                            model.AGlMaster.TRANSSL = BuyReturn_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "JOUR";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = stk_Trans.PSID;
                            model.AGlMaster.CREDITCD = Convert.ToInt64(compid + "4010001");
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase Return-" + stk_Trans.REMARKS);

                            model.AGlMaster.DEBITAMT = stk_Trans.TOTNET;
                            model.AGlMaster.CREDITAMT = 0;

                            model.AGlMaster.TRANSDRCR = "DEBIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();



                            BuyReturn_SL = BuyReturn_SL + 1;
                            model.AGlMaster.TRANSSL = BuyReturn_SL;
                            model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                            model.AGlMaster.COMPID = stk_Trans.COMPID;
                            model.AGlMaster.TRANSTP = "JOUR";
                            model.AGlMaster.TRANSMY = Month_Year;
                            model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                            //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                            //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                            //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                            model.AGlMaster.DEBITCD = Convert.ToInt64(compid + "4010001");
                            model.AGlMaster.CREDITCD = stk_Trans.PSID; 
                            //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                            //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                            model.AGlMaster.REMARKS = Convert.ToString("Purchase Return-" + stk_Trans.REMARKS);

                            model.AGlMaster.DEBITAMT = 0;
                            model.AGlMaster.CREDITAMT = stk_Trans.TOTNET;

                            model.AGlMaster.TRANSDRCR = "CREDIT";
                            model.AGlMaster.TABLEID = "STK_TRANS";

                            model.AGlMaster.USERPC = strHostName;
                            model.AGlMaster.INSIPNO = ipAddress.ToString();
                            model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                            model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                            db.GlMasterDbSet.Add(model.AGlMaster);
                            db.SaveChanges();

                            if (stk_Trans.CASHAMT != 0)
                            {
                                BuyReturn_SL = BuyReturn_SL + 1;
                                model.AGlMaster.TRANSSL = BuyReturn_SL;
                                model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                                model.AGlMaster.COMPID = stk_Trans.COMPID;
                                model.AGlMaster.TRANSTP = "MPAY";
                                model.AGlMaster.TRANSMY = Month_Year;
                                model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                                //model.AGlMaster.TRANSFOR = stk_Trans.tra;
                                //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                                //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                                model.AGlMaster.DEBITCD = stk_Trans.PSID;
                                model.AGlMaster.CREDITCD = Convert.ToInt64(compid + "1010001");
                                //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                                //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                                model.AGlMaster.REMARKS = Convert.ToString("Purchase Return-" + stk_Trans.REMARKS);

                                model.AGlMaster.TRANSDRCR = "DEBIT";
                                model.AGlMaster.TABLEID = "STK_TRANS";

                                model.AGlMaster.DEBITAMT = stk_Trans.CASHAMT;
                                model.AGlMaster.CREDITAMT = 0;

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                                model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                db.SaveChanges();



                                BuyReturn_SL = BuyReturn_SL + 1;
                                model.AGlMaster.TRANSSL = BuyReturn_SL;
                                model.AGlMaster.TRANSDT = stk_Trans.TRANSDT;
                                model.AGlMaster.COMPID = stk_Trans.COMPID;
                                model.AGlMaster.TRANSTP = "MPAY";
                                model.AGlMaster.TRANSMY = Month_Year;
                                model.AGlMaster.TRANSNO = stk_Trans.TRANSNO;
                                //model.AGlMaster.TRANSFOR = stk_Trans.TRANSFOR;
                                //model.AGlMaster.TRANSMODE = stk_Trans.TRANSMODE;
                                //model.AGlMaster.COSTPID = stk_Trans.COSTPID;
                                model.AGlMaster.DEBITCD = Convert.ToInt64(compid + "1010001");
                                model.AGlMaster.CREDITCD = stk_Trans.PSID;
                                //model.AGlMaster.CHEQUENO = stk_Trans.CHEQUENO;
                                //model.AGlMaster.CHEQUEDT = stk_Trans.CHEQUEDT;
                                model.AGlMaster.REMARKS = Convert.ToString("Purchase Return-" + stk_Trans.REMARKS);

                                model.AGlMaster.TRANSDRCR = "CREDIT";
                                model.AGlMaster.TABLEID = "STK_TRANS";

                                model.AGlMaster.DEBITAMT = 0;
                                model.AGlMaster.CREDITAMT = stk_Trans.CASHAMT;

                                model.AGlMaster.USERPC = strHostName;
                                model.AGlMaster.INSIPNO = ipAddress.ToString();
                                model.AGlMaster.INSTIME = Convert.ToDateTime(td);
                                model.AGlMaster.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                db.GlMasterDbSet.Add(model.AGlMaster);
                                db.SaveChanges();
                            }
                        }
                        
                    }
                }
                return "True";
            }
            else
            {
                return "False";
            }
        }
    }
}