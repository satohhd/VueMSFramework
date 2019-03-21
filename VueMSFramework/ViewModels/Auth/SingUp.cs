using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    [DisplayName("SingUp")]
    [Description("Create new")]
    public class SignUp : ViewModel
    {

        [DisplayName("NewAccount")]
        [HtmlTag("h","2")]
        [Grid(6, 3, 3)]
        public string Header1 { get; set; }


        [DisplayName("E-Mail")]
        [HtmlTag("input", "email")]
        [Placeholder("Enter your e-mail address")]
        [Required]
        [Grid(6, 3, 3)]
        public string Email { get; set; }

        [DisplayName("Terminal address")]
        [HtmlTag("input", "hidden")]
        [Grid(6, 3, 3)]
        [Required]
        [ReadOnly(true)]
        [StringLength(40)]
        public string TermAddr { get; set; }

        [DisplayName("Remote address")]
        [HtmlTag("input", "hidden")]
        [Required]
        [Grid(6, 3, 3)]
        [ReadOnly(true)]
        [StringLength(40)]
        public string RemoteAddr { get; set; }

        [DisplayName("User Name")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(6, 3, 3)]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [HtmlTag("input", "password")]
        [Grid(6, 3, 3)]
        public string Password { get; set; }


        [DisplayName("Ticket")]
        [HtmlTag("input", "hidden")]
        public string Ticket { get; set; }


        [DisplayName("Registration application")]
        [HtmlTag("button")]
        [Event("signUp/request","signUp", isVerify: true)]
        [Redirect("signIn", "signIn")]
        [Grid(6, 3, 3)]
        public string BtnRequest { get; set; }

        [DisplayName("Click here for registered users")]
        [HtmlTag("link")]
        [Event("signIn","signIn", pageName: "auth")]
        [Grid(6, 3, 3)]
        public string LinkSignUp { get; set; }



    }

}

