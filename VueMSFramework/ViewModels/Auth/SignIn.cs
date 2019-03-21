using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    public class SignIn : ViewModel
    {
        [DisplayName("Redirect")]
        [HtmlTag("input", "hidden")]
        public string Redirect { get; set; }

        [DisplayName("Authorized")]
        [HtmlTag("input", "hidden")]
        public bool Authorized { get; set; }

        [DisplayName("Applying")]
        [HtmlTag("input", "hidden")]
        public bool Applying { get; set; }


        [DisplayName("AccountId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string AccountId { get; set; }

        [DisplayName("E-Mail")]
        [Required]
        [StringLength(256)]
        [Autofocus]
        [HtmlTag("input", "email")]
        [Placeholder("Enter your e-mail address")]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("Terminal address")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string TermAddr { get; set; }

        [DisplayName("Remote address")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        [StringLength(40)]
        public string RemoteAddr { get; set; }

        [DisplayName("User Name")]
        [StringLength(100)]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        public string UserName { get; set; }


        [DisplayName("Password")]
        [StringLength(100)]
        [HtmlTag("input", "password")]
        [Required]
        [Grid(6, 3, 3)]
        public string Password { get; set; }


        [DisplayName("Connected terminal information")]
        [HtmlTag("input", "hidden")]
        public string ClientInfo { get; set; }


        [DisplayName("Ticket")]
        [HtmlTag("input", "hidden")]
        public string Ticket { get; set; }

        [DisplayName("Expiration")]
        [HtmlTag("input", "hidden")]
        public DateTime? Expiration { get; set; }


        [DisplayName("SignIn")]
        [HtmlTag("button")]
        [Event("signIn/execute","signIn")]
        [Redirect("index", "home","home")]
        [Grid(6, 3, 3)]
        public string BtnSignIn { get; set; }


        [DisplayName("Click here for the first time")]
        [HtmlTag("link")]
        [Event("signUp","signUp",pageName:"auth")]
        [Grid(6, 3, 3)]
        public string LinkSignUp { get; set; }



    }

}

