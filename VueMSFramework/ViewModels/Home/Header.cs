using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Home
{

    [DisplayName("Header")]
    public class Header : ViewModel
    {
        [DisplayName("menu")]
        [HtmlTag("menu")]
        public string Menu { get; set; }

        [DisplayName("breadcrumb")]
        [HtmlTag("breadcrumb")]
        public string Breadcrumb { get; set; }


    }

}

