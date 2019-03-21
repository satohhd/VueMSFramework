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

        [DisplayName("AccountId")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string AccountId { get; set; }

        [DisplayName("E-Mail")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("Terminal address")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string TermAddr { get; set; }

        [DisplayName("Remote address")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string RemoteAddr { get; set; }

        [DisplayName("User Name")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        public string Password { get; set; }

        [DisplayName("Edit")]
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

