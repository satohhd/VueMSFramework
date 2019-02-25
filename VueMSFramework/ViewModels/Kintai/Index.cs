using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Kintai
{
    public class Index : ViewModel
    {

        [DisplayName("選択")]
        [HtmlTag("section")]
        public Select Select { get; set; }

        [DisplayName("検索")]
        [HtmlTag("section")]
        public Search Search { get; set; }

        //[DisplayName("作成")]
        //[HtmlTag("section")]
        //public Create Create { get; set; }

        [DisplayName("編集")]
        [HtmlTag("section")]
        public Edit Edit { get; set; }

        [DisplayName("削除")]
        [HtmlTag("section")]
        public Remove Remove { get; set; }

    }
}

