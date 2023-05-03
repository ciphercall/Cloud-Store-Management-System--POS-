using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.Models;

namespace AS_Store_GL.Controllers.GL
{
    public class ProcessMissMatchController : AppController
    {
        public ProcessMissMatchController()
        {
            ViewData["HighLight_Menu_AccountForm"] = "highlight menu";
        }



        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Index(PageModel model, string command)
        {
            if (command == "Miss Match Report")
            {
                TempData["ProcessMissMatch"] = model;
                return RedirectToAction("ProcessMissMatchReport");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult ProcessMissMatchReport()
        {
            PageModel model = (PageModel)TempData["ProcessMissMatch"];
            return View(model);
        }

    }
}
