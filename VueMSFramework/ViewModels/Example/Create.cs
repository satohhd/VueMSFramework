using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Example
{
    public class CreatePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","create")]
        public string BtnBack { get; set; }

        [DisplayName("登録")]
        [HtmlTag("button")]
        [Event("create/insert","create", isVerify:true, isConfirm:true)]
        public string BtnInsert { get; set; }


    }
    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {
        //パネル
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public CreatePanel Panel { get; set; }

        //ユーザ項目
        [Key]
        [DisplayName("ID")]
        [Required]
        [HtmlTag("input", "hidden")]
        [StringLength(128)]
        [Grid(4)]
        public string ExampleId { get; set; }

        [DisplayName("メールアドレス")]
        [Required]
        [HtmlTag("input", "email")]
        [StringLength(256)]
        [Grid(4)]
        public string Email { get; set; }

        [DisplayName("端末アドレス")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(40)]
        [Grid(4)]
        public string TermAddr { get; set; }

        [DisplayName("リモートアドレス")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(40)]
        [Grid(4)]
        public string RemoteAddr { get; set; }

        [DisplayName("ユーザ名")]
        [StringLength(256)]
        [HtmlTag("input", "textarea")]
        [Grid(4)]
        public string UserName { get; set; }


        [DisplayName("リンクラベル")]
        [StringLength(256)]
        [HtmlTag("label")]
        public string DetailInput { get; set; }


    }

}

