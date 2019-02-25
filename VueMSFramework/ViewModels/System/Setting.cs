using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.System
{
    public class  Setting : ViewModel
    {

        [DisplayName("ローカルストレージ削除")]
        [HtmlTag("h", "2")]
        public string Group4 { get; set; }

        [DisplayName("実行")]
        [HtmlTag("button")]
        [Event("localClear","setting")]
        public string BtnClear { get; set; }

        [DisplayName("データベース")]
        [HtmlTag("h", "2")]
        public string Group5 { get; set; }

        [DisplayName("実行")]
        [HtmlTag("button")]
        [Event("database/load","database")]
        public string BtnDatabase { get; set; }


    }



}

