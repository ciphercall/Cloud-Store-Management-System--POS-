using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace AS_Store_GL.Models
{
    public class PageModel
    {

        public PageModel()
        {
            this.aslMenumst = new ASL_MENUMST();
            this.aslMenu = new ASL_MENU();
            this.aslUserco = new AslUserco();
            this.aslCompany = new AslCompany();
            this.aslLog = new ASL_LOG();
            this.AGL_accharmst = new GL_ACCHARMST();
            this.AGL_acchart = new GL_ACCHART();


            this.AGlCostPMST = new GL_COSTPMST();
            this.AGlCostPool = new GL_COSTPOOL();
            this.AGlStrans = new GL_STRANS();
            this.AGlMaster=new GL_MASTER();

            this.StkItemmst = new STK_ITEMMST();
            this.StkItem = new STK_ITEM();
            this.StkStore = new STK_STORE();
            this.StkTransmst = new STK_TRANSMST();
            this.StkTrans = new STK_TRANS();
        }

        public ASL_MENUMST aslMenumst { get; set; }
        public ASL_MENU aslMenu { get; set; }
        public AslUserco aslUserco { get; set; }
        public AslCompany aslCompany { get; set; }
        public ASL_LOG aslLog { get; set; }


        public GL_ACCHARMST AGL_accharmst { get; set; }
        public GL_ACCHART AGL_acchart { get; set; }
        public GL_COSTPMST AGlCostPMST { get; set; }
        public GL_COSTPOOL AGlCostPool { get; set; }
        public GL_STRANS AGlStrans { get; set; }
        public GL_MASTER AGlMaster { get; set; }



        public STK_ITEMMST StkItemmst { get; set; }
        public STK_ITEM StkItem { get; set; }
        public STK_STORE StkStore { get; set; }
        public STK_TRANSMST StkTransmst { get; set; }
        public STK_TRANS StkTrans { get; set; }




        [Display(Name = "HeadType")]
        public string HeadType { get; set; }
        
        [Required(ErrorMessage = "From date field can not be empty!")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessage = "To Date field can not be empty!")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }





        //SaleController
        public string Empty { get; set; } //It used for readonly value(HtmlTextBoxfor) hold.




        //ReportController
        [Required(ErrorMessage = "From date field can not be empty!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Report_FromDate { get; set; }

        [Required(ErrorMessage = "To Date field can not be empty!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Report_ToDate { get; set; }

        [Required(ErrorMessage = "Item Name field can not be empty!")]
        public Int64 ITEMID { get; set; }

        [Required(ErrorMessage = "Store Name field can not be empty!")]
        public Int64 STOREID { get; set; }

        [Required(ErrorMessage = "Transaction Type field can not be empty!")]
        public string TRANSTP { get; set; }



        //Schedular Calendar
        public Int64? Userid { get; set; }
    }
}