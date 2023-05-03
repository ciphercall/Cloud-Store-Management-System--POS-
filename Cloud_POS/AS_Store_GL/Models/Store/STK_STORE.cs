using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AS_Store_GL.Models
{
    public class STK_STORE
    {
        [Key]
        public Int64 STK_STORE_ID { get; set; }

        [Display(Name = "Company ID")]
        public Int64? COMPID { get; set; }

        //[Required(ErrorMessage = "Select a valid category!")]
        [Display(Name = "Store ID")]
        public Int64? STOREID { get; set; }

        [Required(ErrorMessage = "Store name can not be empty!")]
        [Display(Name = "Store Name")]
        public string STORENM { get; set; }

        [Display(Name = "Stores ID")]
        public string STORESID { get; set; }
        
        [Display(Name = "Remarks")]
        public string REMARKS { get; set; }








        [Display(Name = "User PC")]
        public string USERPC { get; set; }
        public Int64? INSUSERID { get; set; }

        [Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }

        [Display(Name = "Inesrt IP ADDRESS")]
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64? UPDUSERID { get; set; }

        [Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }

        [Display(Name = "Update IP ADDRESS")]
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}