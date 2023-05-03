using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.Models;
using AS_Store_GL.Models.DTO;

namespace AS_Store_GL.Controllers.GL
{
    public class ChequePaymentController : AppController
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
        Int64 loggedCompId;

        public ChequePaymentController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            //td = DateTime.Now;
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            ViewData["HighLight_Menu_AccountForm"] = "Heigh Light Menu";

            try
            {
                loggedCompId = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            }
            catch (Exception)
            {
                RedirectToAction("Index", "Logout");
            }
        }



        public ActionResult Index()
        {
            return View();
        }


        
        public ActionResult ChequePrint(string dt,string d, string tp)
        {
            Gl_StransDTO model = new Gl_StransDTO();
            model.Id = Convert.ToInt64(d);
            model.COMPID = loggedCompId;
            model.TRANSDT = Convert.ToDateTime(dt);
            model.TRANSTP = tp;
            TempData["chequeModel"] = model;
            return RedirectToAction("GetChequePrint");
        }

        public ActionResult GetChequePrint()
        {
            var model = (Gl_StransDTO)TempData["chequeModel"];
            return View(model);
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
