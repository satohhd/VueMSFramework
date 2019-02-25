using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Exam
{
    [DisplayName("試験管理")]
    [Description("説明")]
    public class Index : ViewModel
    {

        [DisplayName("受験者")]
        [HtmlTag("section")]
        public Select Select { get; set; }

        [DisplayName("試験結果")]
        [HtmlTag("section")]
        public Search Search { get; set; }

        [DisplayName("作成")]
        [HtmlTag("section")]
        public Create Create { get; set; }

        [DisplayName("編集")]
        [HtmlTag("section")]
        public Edit Edit { get; set; }

        [DisplayName("削除")]
        [HtmlTag("section")]
        public Remove Remove { get; set; }

        [DisplayName("カテゴリ")]
        [HtmlTag("section")]
        public CategoryCrud Category { get; set; }

    }



}

