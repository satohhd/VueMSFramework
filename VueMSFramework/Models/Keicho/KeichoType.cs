using System.ComponentModel;
using VueMSFramework.Models.Common;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;

namespace VueMSFramework.Models.Keicho
{
    //【ExamResult】
    public class KeichoType :BaseModel
    {

        [Key]
        [DisplayName("慶弔種類")]
        [Required]
        [StringLength(10)]
        [HtmlTag("input", "text")]
        public string KeichoTypeId { get; set; }

        [DisplayName("種類")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string KeichoTypeName { get; set; }

        [DisplayName("表示順")]
        [Range(0,1000)]
        [HtmlTag("input", "number")]
        public int OrderBy { get; set; }

        [DisplayName("非表示フラグ")]
        [HtmlTag("input", "radio")]
        public bool IsHidden { get; set; } = false;


    }
}
