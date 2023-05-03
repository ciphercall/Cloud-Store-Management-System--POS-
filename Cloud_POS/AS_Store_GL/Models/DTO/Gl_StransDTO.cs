using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AS_Store_GL.Models.DTO
{
    public class Gl_StransDTO
    {

        public Int64 Id { get; set; }
        public Int64? COMPID { get; set; }
        public string TRANSTP { get; set; }
        
        public DateTime? TRANSDT { get; set; }
        
        public string TRANSMY { get; set; }
        
        public Int64? TRANSNO { get; set; }
        public string TRANSFOR { get; set; }
        public string TRANSMODE { get; set; }
        public Int64? COSTPID { get; set; }
        public Int64? CREDITCD { get; set; }

        public Int64? DEBITCD { get; set; }
        public string PaymentParticulars { get; set; }

        public string CHEQUENO { get; set; }
        public string CHEQUEDT { get; set; }
        public string REMARKS { get; set; }
        public decimal? AMOUNT { get; set; }
        public string CHQPAYTO { get; set; }










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


        public string TransDate { get; set; }
        public string Update { get; set; }

        public Int64 count { get; set; }
        public bool ValidationError { get; set; }
    }
}