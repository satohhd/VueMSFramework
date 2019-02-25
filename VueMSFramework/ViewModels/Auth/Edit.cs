using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    public class Edit : ViewModel
    {

        [DisplayName("Account information in use")]
        [HtmlTag("h","2")]
        [Grid(6, 3, 3)]
        public string Header1 { get; set; }

        [DisplayName("アカウントID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string AccountId { get; set; }

        [DisplayName("Email")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        [Placeholder("メールアドレスを入力してください")]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("端末アドレス")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string TermAddr { get; set; }

        [DisplayName("リモートアドレス")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string RemoteAddr { get; set; }
        //ユーザ項目
        [DisplayName("氏名")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(6, 3, 3)]
        [Autofocus]
        public string UserName { get; set; }

        [DisplayName("パスワード")]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        public string Password { get; set; }

        [DisplayName("チケット")]
        [HtmlTag("input", "hidden")]
        public string Ticket { get; set; }


        [DisplayName("保存")]
        [HtmlTag("button")]
        [Event("edit/update","edit", isVerify: true)]
        [Redirect("refer/load","refer", paramItems: "accountId")]
        [Grid(6, 3, 3)]
        public string BtnUpdate { get; set; }






    }

}

