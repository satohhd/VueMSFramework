using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Option
{

    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {
        public class Panel
        {

            [DisplayName("戻る")]
            [HtmlTag("button")]
            [Event("back","create")]
            public string BtnBack { get; set; }

            [DisplayName("登録")]
            [HtmlTag("button")]
            [Event("create/insert","create", isVerify: true)]
            [Redirect("search/load","search")]
            public string BtnInsert { get; set; }


        }

        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public virtual Panel ControlPanel { get; set; }


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

