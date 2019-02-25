using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    [DisplayName("サインアップ")]
    [Description("新規作成します")]
    public class SignUp : ViewModel
    {

        [DisplayName("新規アカウント")]
        [HtmlTag("h","2")]
        [Grid(6, 3, 3)]
        public string Header1 { get; set; }


        [DisplayName("メールアドレス")]
        [HtmlTag("input", "email")]
        [Placeholder("メールアドレスを入力してください")]
        [Required]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("端末アドレス")]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        [Required]
        [ReadOnly(true)]
        [StringLength(40)]
        public string TermAddr { get; set; }

        [DisplayName("リモートアドレス")]
        [HtmlTag("input", "hidden")]
        [Required]
        [Grid(6, 3, 3)]
        [ReadOnly(true)]
        [StringLength(40)]
        public string RemoteAddr { get; set; }

        //ユーザ項目
        [DisplayName("氏名")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(6, 3, 3)]
        public string UserName { get; set; }

        [DisplayName("パスワード")]
        [HtmlTag("input", "password")]
        [Grid(6, 3, 3)]
        public string Password { get; set; }


        [DisplayName("チケット")]
        [HtmlTag("input", "hidden")]
        public string Ticket { get; set; }


        [DisplayName("登録申請")]
        [HtmlTag("button")]
        [Event("signUp/request","signUp", isVerify: true)]
        [Redirect("signIn", "signIn")]
        [Grid(6, 3, 3)]
        public string BtnRequest { get; set; }

        [DisplayName("登録済みの方はこちら")]
        [HtmlTag("link")]
        [Event("signIn","signIn", pageName: "auth")]
        [Grid(6, 3, 3)]
        public string LinkSignUp { get; set; }



    }

}

