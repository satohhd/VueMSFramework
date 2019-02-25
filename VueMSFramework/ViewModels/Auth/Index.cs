using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Auth
{
    public class Index : ViewModel
    {
    
        [DisplayName("サインイン")]
        [HtmlTag("section")]
        public SignIn SignIn { get; set; }

        [DisplayName("アカウント情報")]
        [HtmlTag("section")]
        public Refer Refer { get; set; }

        [DisplayName("プロファイル編集")]
        [HtmlTag("section")]
        public Edit Edit { get; set; }

        [DisplayName("アカウント登録")]
        [HtmlTag("section")]
        public SignUp SignUp { get; set; }

    }



}

