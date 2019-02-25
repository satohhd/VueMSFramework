using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Option
{

    public class EditPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }

        [DisplayName("更新")]
        [HtmlTag("button")]
        [Event("edit/update","edit", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnUpdate { get; set; }


    }
    [DisplayName("編集")]
    [Description("編集します")]
    public class Edit : ViewModel
    {


        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public EditPanel Panel { get; set; }

        //システム項目
        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        public string OptionId { get; set; }

        //ユーザ項目
        [DisplayName("項目")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string Field { get; set; }

        [DisplayName("表示名称")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Text { get; set; }

        [DisplayName("値")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Value { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        [Range(0, 8000)]
        [Help("0～8000の範囲で入力してください。")]
        public int OrderBy { get; set; } = 0;

        [DisplayName("表示色")]
        [HtmlTag("input", "color")]
        [StringLength(10)]
        public string Color { get; set; } = "#80ffff";

        [DisplayName("アイコン")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string IconUrl { get; set; }

    }
}
