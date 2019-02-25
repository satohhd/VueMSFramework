using System.ComponentModel;
using VueMSFramework.Models.Common;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;

namespace VueMSFramework.Models
{
    public class ExamCategory : BaseModel
    {

        [Key]
        public string ExamCategoryId { get; set; }


        [DisplayName("試験ID")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string ExamId { get; set; }


        [DisplayName("カテゴリ")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string CategoryId { get; set; }


    }
}
