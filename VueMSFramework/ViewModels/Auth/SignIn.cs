using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    public class SignIn : ViewModel
    {
        [DisplayName("認証後のリダイレクト先")]
        [HtmlTag("input", "hidden")]
        public string Redirect { get; set; }

        [DisplayName("認証")]
        [HtmlTag("input", "hidden")]
        public bool Authorized { get; set; }

        [DisplayName("申請中")]
        [HtmlTag("input", "hidden")]
        public bool Applying { get; set; }


        [DisplayName("アカウントID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string AccountId { get; set; }

        //ユーザ項目
        [DisplayName("EmailAddress")]
        [Required]
        [StringLength(256)]
        [Autofocus]
        [HtmlTag("input", "email")]
        [Placeholder("メールアドレスを入力してください")]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("端末アドレス")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string TermAddr { get; set; }

        [DisplayName("リモートアドレス")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string RemoteAddr { get; set; }

        //ユーザ項目
        [DisplayName("User Name")]
        [StringLength(100)]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        public string UserName { get; set; }


        [DisplayName("password")]
        [StringLength(100)]
        [HtmlTag("input", "password")]
        [Required]
        [Grid(6, 3, 3)]
        public string Password { get; set; }


        [DisplayName("接続端末情報")]
        [HtmlTag("input", "hidden")]
        public string ClientInfo { get; set; }


        [DisplayName("チケット")]
        [HtmlTag("input", "hidden")]
        public string Ticket { get; set; }

        [DisplayName("有効期間")]
        [HtmlTag("input", "hidden")]
        public DateTime? Expiration { get; set; }


        [DisplayName("サインイン")]
        [HtmlTag("button")]
        [Event("signIn/execute","signIn")]
        [Redirect("index", "home","home")]
        [Grid(6, 3, 3)]
        public string BtnSignIn { get; set; }


        [DisplayName("初めて利用される方はこちら")]
        [HtmlTag("link")]
        [Event("signUp","signUp",pageName:"auth")]
        [Grid(6, 3, 3)]
        public string LinkSignUp { get; set; }



    }

}

