using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{
    public class Group : BaseModel
    {
        //システム項目
        [Key]
        [DisplayName("グループID")]
        public string GroupId { get; set; }


        //ユーザ項目
        [DisplayName("グループ名")]
        [Required]
        [StringLength(100)]
        public string GroupName { get; set; }

        [DisplayName("表示順")]
        [Range(0,99999)]
        public int OrderBy { get; set; }

        [DisplayName("表示色")]
        [StringLength(10)]
        public string Color { get; set; }

        [DisplayName("アイコン")]
        [StringLength(255)]
        public string IconUrl { get; set; }


    }

}
