using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AS_Store_GL.Models.Store
{
    [Table("STK_PS")]
    public class STK_PS
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ID { get; set; }

        [Key, Column(Order = 1)]
        public Int64 COMPID { get; set; }
        public Int64 PSGRID { get; set; } //--101 103 / 101 203 (GL_ACCHARTMST)

        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "Please, input valid name!")]
        public Int64 PSID { get; set; } //--101 1030001/101 2030001 (GL_ACCHART)

        [StringLength(100, MinimumLength = 0)]
        public string ADDRESS { get; set; }

        [StringLength(20, MinimumLength = 0)]
        public string TELNO { get; set; }

        [RegularExpression(@"^(8{2})([0-9]{11})", ErrorMessage = "Insert a valid phone number like 8801711001100")]
        public string MOBNO { get; set; }

        [StringLength(80, MinimumLength = 0)]
        public string EMAIL { get; set; }
        [StringLength(80, MinimumLength = 0)]
        public string WEBID { get; set; }
        [StringLength(80, MinimumLength = 0)]
        public string CPNM { get; set; }
        [StringLength(11, MinimumLength = 0)]
        public string CPCNO { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string REMARKS { get; set; }







        public string USERPC { get; set; }
        public Int64 INSUSERID { get; set; }

        //[Display(Name = "Insert Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? INSTIME { get; set; }
        public string INSIPNO { get; set; }
        public string INSLTUDE { get; set; }
        public Int64 UPDUSERID { get; set; }

        //[Display(Name = "Update Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UPDTIME { get; set; }
        public string UPDIPNO { get; set; }
        public string UPDLTUDE { get; set; }
    }
}