using System.ComponentModel;
using VueMSFramework.Models.Common;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;

namespace VueMSFramework.Models
{
    public class Category : BaseModel
    {

        [Key]
        public string CategoryId { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        public string ExamineeId { get; set; }

        [DisplayName("カテゴリ")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string CategoryName { get; set; }

        [DisplayName("表示順")]
        [Range(0,1000)]
        [HtmlTag("input", "number")]
        public int? OrderBy { get; set; }

    }
}
