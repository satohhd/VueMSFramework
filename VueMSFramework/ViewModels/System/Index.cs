using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.System
{
    public class Index : ViewModel
    {

        [DisplayName("設定")]
        [HtmlTag("section")]
        public Setting Setting { get; set; }

        [DisplayName("データベース")]
        [HtmlTag("section")]
        public Database Database { get; set; }

    }



}

