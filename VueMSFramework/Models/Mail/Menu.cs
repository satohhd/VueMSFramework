using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{

    public class Menu : BaseModel
    {

        [Key]
        public string MenuId { get; set; }

        //ユーザ項目
        [DisplayName("タイトル")]
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [DisplayName("説明")]
        [StringLength(1000)]
        public string Description { get; set; }

        [DisplayName("タイプ")]
        [StringLength(10)]
        public string Type { get; set; }

        [DisplayName("画像")]
        [StringLength(256)]
        public string Image { get; set; }

        [DisplayName("アイコン")]
        [StringLength(256)]
        public string Icon { get; set; }

        [DisplayName("表示順")]
        [Range(0,99999)]
        public int OrderBy { get; set; } = 99999;

        [DisplayName("権限")]
        public bool IsAuth { get; set; } = true;


    }
}
