using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{
    public class Option : BaseModel
    {
        //システム項目
        [Key]
        [DisplayName("グループID")]
        public string OptionId { get; set; }

        //ユーザ項目
        [DisplayName("項目")]
        [Required]
        [StringLength(100)]
        public string Field { get; set; }

        [DisplayName("名称")]
        [Required]
        [StringLength(100)]
        public string Text { get; set; }

        [DisplayName("値")]
        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        [DisplayName("アクション")]
        [StringLength(100)]
        public string Action { get; set; }

        [DisplayName("表示順")]
        [StringLength(5)]
        public int OrderBy { get; set; } = 99999;

        [DisplayName("表示色")]
        [StringLength(10)]
        public string Color { get; set; }

        [DisplayName("アイコン")]
        [StringLength(255)]
        public string IconUrl { get; set; }

    }

}
