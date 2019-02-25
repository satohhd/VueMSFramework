using System;
using System.ComponentModel;
using VueMSFramework.Models.Common;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;

namespace VueMSFramework.Models.Keicho
{
    //【Keicho 慶弔】
    public class Keicho :BaseModel
    {

        [Key]
        public string KeichoId { get; set; }

        //ユーザ項目
        [DisplayName("慶弔区分")]
        [Required]
        [StringLength(10)]
        [HtmlTag("input", "text")]
        public string KeichoClass { get; set; }


        [DisplayName("慶弔種類")]
        [Required]
        [StringLength(10)]
        [HtmlTag("input", "text")]
        public string KeichoTypeId { get; set; }

        [DisplayName("タイトル")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Title { get; set; }

        [DisplayName("あげた人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string GiveUserName { get; set; }

        [DisplayName("もらった人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string TakeUserName { get; set; }


        [DisplayName("慶弔日")]
        [HtmlTag("input", "date")]
        public DateTime? KeichoDate { get; set; }

        [DisplayName("金額")]
        [Range(0,100000)]
        [HtmlTag("input", "number")]
        public decimal Money { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        public string Notes { get; set; }



    }
}
