using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{

    public class Activity : BaseModel
    {

        [Key]
        public string ActivityId { get; set; }

        //ユーザ項目
        [DisplayName("表題")]
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [DisplayName("内容")]
        [StringLength(1000)]
        [Required]
        public string Content { get; set; }


        [DisplayName("カテゴリ")]
        [StringLength(10)]
        public string Category { get; set; }


        [DisplayName("ファイル")]
        public string File1 { get; set; }


    }
}
