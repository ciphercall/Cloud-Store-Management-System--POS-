using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AS_Store_GL.Models;

namespace AS_Store_GL.Controllers
{
    public class ReportController : AppController
    {
        private Store_GL_DbContext db = new Store_GL_DbContext();


        public ReportController()
        {
            ViewData["HighLight_Menu_BillingReport"] = "Heigh Light Menu";
        }




        //Stock Report (Item Lists)
        public ActionResult GetItemList()
        {
            return View();
        }








        //Stock Report (Item Ledger)
        public ActionResult ItemLedger()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ItemLedger(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("GetItemLedger");
        }

        public ActionResult GetItemLedger()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }








        //Stock Report (Closing Stock Details)
        public ActionResult ClosingStock_Details()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClosingStock_Details(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_ClosingStock_Details");
        }

        public ActionResult Get_ClosingStock_Details()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }










        //Stock Report (Closing Stock with Value)
        public ActionResult ClosingStock_withValue()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClosingStock_withValue(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_ClosingStock_withValue");
        }

        public ActionResult Get_ClosingStock_withValue()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }












        //Stock Report (Daily Sales Statement)
        public ActionResult DailySalesStatement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DailySalesStatement(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_DailySalesStatement");
        }

        public ActionResult Get_DailySalesStatement()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }










        //Stock Report (Transaction Details)
        public ActionResult TransactionDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TransactionDetails(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_TransactionDetails");
        }

        public ActionResult Get_TransactionDetails()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }









        //Stock Report (Stock order level Statement)
        public ActionResult StockOrder_levelStatement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StockOrder_levelStatement(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_StockOrder_levelStatement");
        }

        public ActionResult Get_StockOrder_levelStatement()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }







        //Stock Report (Sale Purchase Details)
        public ActionResult Sale_Purchase_DETAILS()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sale_Purchase_DETAILS(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_Sale_Purchase_DETAILS");
        }

        public ActionResult Get_Sale_Purchase_DETAILS()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }









        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Trans_TypeChanged(string txtType)
        {
            List<SelectListItem> getHeadName = new List<SelectListItem>();
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            var findPartyID = (from n in db.GlAcchartDbSet where n.COMPID == compid select n).ToList();
            Int64 headCD = 0;
            foreach (var glAcchart in findPartyID)
            {
                headCD = Convert.ToInt64(glAcchart.HEADCD.ToString().Substring(3, 3));
                if (txtType == "BUY")
                {
                    if (headCD == 203 || headCD==107)
                    {
                        getHeadName.Add(new SelectListItem { Text = glAcchart.ACCOUNTNM, Value = glAcchart.ACCOUNTCD.ToString() });
                    }
                }
                else if (txtType == "SALE")
                {
                    if (headCD == 103)
                    {
                        getHeadName.Add(new SelectListItem { Text = glAcchart.ACCOUNTNM, Value = glAcchart.ACCOUNTCD.ToString() });
                    }
                }               
            }

            return Json(getHeadName, JsonRequestBehavior.AllowGet);
        }













        //Stock Report (Sale/Purchase Summary)
        public ActionResult Sale_Purchase_SUMMARY()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sale_Purchase_SUMMARY(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_Sale_Purchase_SUMMARY");
        }

        public ActionResult Get_Sale_Purchase_SUMMARY()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }









        //Stock Report (Sale Purchase Summary-All Head)
        public ActionResult Sale_Purchase_SUMMARY_All_Head()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sale_Purchase_SUMMARY_All_Head(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_Sale_Purchase_SUMMARY_All_Head");
        }

        public ActionResult Get_Sale_Purchase_SUMMARY_All_Head()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }








        //Stock Report (Sale Purchase Summary-All Item)
        public ActionResult Sale_Purchase_SUMMARY_All_Item()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sale_Purchase_SUMMARY_All_Item(PageModel model)
        {
            TempData["model"] = model;
            return RedirectToAction("Get_Sale_Purchase_SUMMARY_All_Item");
        }

        public ActionResult Get_Sale_Purchase_SUMMARY_All_Item()
        {
            var model = (PageModel)TempData["model"];
            return View(model);
        }




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
