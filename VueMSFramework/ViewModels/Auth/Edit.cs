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

        [DisplayName("AccountId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string AccountId { get; set; }

        [DisplayName("E-Mail")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        [Placeholder("Enter your e-mail address")]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("Terminal address")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string TermAddr { get; set; }

        [DisplayName("Remote address")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string RemoteAddr { get; set; }
        //ユーザ項目
        [DisplayName("Name")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(6, 3, 3)]
        [Autofocus]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        public string Password { get; set; }

        [DisplayName("Ticket")]
        [HtmlTag("input", "hidden")]
        public string Ticket { get; set; }


        [DisplayName("Store")]
        [HtmlTag("button")]
        [Event("edit/update","edit", isVerify: true)]
        [Redirect("refer/load","refer", paramItems: "accountId")]
        [Grid(6, 3, 3)]
        public string BtnUpdate { get; set; }






    }

}

