using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Table
{
    [DisplayName("テーブル")]
    [Description("説明")]
    public class Index : ViewModel
    {
        [DisplayName("テーブル一覧")]
        [HtmlTag("section")]
        public Search Search { get; set; }

        [DisplayName("照会")]
        [HtmlTag("section")]
        public Refere Refere { get; set; }

        [DisplayName("取り込み")]
        [HtmlTag("dialog")]
        public Import Import { get; set; }


    
    }

}

