using Microsoft.Extensions.Localization;
using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

//[assembly: ResourceLocation("ViewModels.Auth.Index")]
//[assembly: RootNamespace("App Root Namespace")]
namespace VueMSFramework.ViewModels.Auth
{
    public class Refer : ViewModel
    {

        [DisplayName("Account information in use")]
        [HtmlTag("h","2")]
        [Grid(6, 3, 3)]
        public string Header1 { get; set; }

        [DisplayName("アカウントID")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string AccountId { get; set; }

        [DisplayName("メールアドレス")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("端末アドレス")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string TermAddr { get; set; }

        [DisplayName("リモートアドレス")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string RemoteAddr { get; set; }

        //ユーザ項目
        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string UserName { get; set; }

        [DisplayName("パスワード")]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        public string Password { get; set; }

        [DisplayName("編集")]
        [HtmlTag("button")]
        [Event("edit/load","edit", isVerify: true, paramItems: "accountId")]
        [Grid(6, 3, 3)]
        public string BtnEdit { get; set; }


        //[DisplayName("新規登録")]
        //[HtmlTag("button")]
        //[Trigger("create", true)]
        //[Grid(6, 3, 3)]
        //public string BtnCreate { get; set; }

    }

}

