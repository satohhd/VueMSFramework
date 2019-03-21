using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    public class Index : ViewModel
    {
    
        [DisplayName("SignIn")]
        [HtmlTag("section")]
        public SignIn SignIn { get; set; }

        [DisplayName("AccountInfo")]
        [HtmlTag("section")]
        public Refer Refer { get; set; }

        [DisplayName("EditProfile")]
        [HtmlTag("section")]
        public Edit Edit { get; set; }

        [DisplayName("RegistAccount")]
        [HtmlTag("section")]
        public SignUp SignUp { get; set; }

    }



}

