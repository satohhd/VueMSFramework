using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Activity
{

    public class CreatePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","create")]
        public string BtnBack { get; set; }

        [DisplayName("登録")]
        [HtmlTag("button")]
        [Event("create/insert","create", isVerify: true, isConfirm: true)]
        [Redirect("search/load","search")]
        public string BtnInsert { get; set; }

    }

    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public CreatePanel Panel { get; set; }

        //システム項目
        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ActivityId { get; set; }

        //ユーザ項目
        [DisplayName("表題")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(8)]
        [Autofocus]
        public string Title { get; set; }

        [DisplayName("内容")]
        [Required]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        public string Content { get; set; }


        [DisplayName("カテゴリ")]
        [StringLength(10)]
        [HtmlTag("input", "radio")]
        [Options("Contents", "Category")]
        [Grid(6)]
        public string Category { get; set; }

        [DisplayName("ファイル")]
        [HtmlTag("input", "file")]
        public File File1 { get; set; }


    }

}

