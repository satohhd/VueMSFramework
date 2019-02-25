using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Exam
{
    [DisplayName("削除")]
    [Description("説明")]
    public class RemovePanel
    {
        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove")]
        [Redirect("search/load","search")]
        public string BtnRemove { get; set; }


    }
    public class Remove : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public RemovePanel Panel { get; set; } = new RemovePanel();

        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ExamId { get; set; }


        [DisplayName("試験名")]
        [HtmlTag("input", "text")]
        [Required]
        [Grid(8)]
        [ReadOnly(true)]
        public string ExamName { get; set; }

        [DisplayName("試験日")]
        [HtmlTag("input", "date")]
        [Grid(4)]
        [ReadOnly(true)]
        public string ExamDate { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [Required]
        [ReadOnly(true)]
        [Grid(6)]
        public string ExamineeId { get; set; }

        [DisplayName("カテゴリ")]
        [HtmlTag("input", "checkboxes")]
        [Options("Categories", "")]
        [ReadOnly(true)]
        [Grid(6)]
        public string[] CategoryIds { get; set; }

        [DisplayName("国語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? KokugoScore { get; set; }

        [DisplayName("国語(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? KokugoAveScore { get; set; }


        [DisplayName("数学")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? SugakuScore { get; set; }

        [DisplayName("数学(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? SugakuAveScore { get; set; }


        [DisplayName("理科")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? RikaScore { get; set; }

        [DisplayName("理科(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? RikaAveScore { get; set; }

        [DisplayName("社会")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? ShakaiScore { get; set; }

        [DisplayName("社会(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public decimal? ShakaiAveScore { get; set; }


        [DisplayName("英語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Grid(3)]
        public int? EigoScore { get; set; }

        [DisplayName("英語(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Grid(3)]
        [ReadOnly(true)]
        public float? EigoAveScore { get; set; }


        [DisplayName("メモ")]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        [ReadOnly(true)]
        public string Memo { get; set; }


    }

}

