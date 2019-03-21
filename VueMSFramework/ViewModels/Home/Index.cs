using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Home
{
    [DisplayName("ホーム")]
    public class Index : ViewModel
    {
        [HtmlTag("section")]
        public Header Header { get; set; }

        [HtmlTag("section")]
        public Body Body { get; set; }

        [HtmlTag("section")]
        public Footer Footer { get; set; }


    }



}

