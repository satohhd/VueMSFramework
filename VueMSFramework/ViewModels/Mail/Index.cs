using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Mail
{
    public class Index : ViewModel
    {

        [DisplayName("確認")]
        [HtmlTag("dialog")]
        public Confirm Confirm { get; set; }

        [DisplayName("検索")]
        [HtmlTag("section")]
        public Search Search { get; set; }

        [DisplayName("メール作成")]
        [HtmlTag("section")]
        public Create Create { get; set; }

        [DisplayName("メール作成(下書きから)")]
        [HtmlTag("section")]
        public Edit Edit { get; set; }

        [DisplayName("削除")]
        [HtmlTag("section")]
        public Remove Remove { get; set; }

        [DisplayName("照会")]
        [HtmlTag("section")]
        public Refer Refer { get; set; }


    }
}
