using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Exam
{

    [DisplayName("編集")]
    [Description("説明")]
    public class EditPanel
    {
        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }


        [DisplayName("更新")]
        [HtmlTag("button")]
        [Event("edit/update","edit",isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnUpdate { get; set; }

    }
    public class Edit : ViewModel
    {


        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public EditPanel Panel { get; set; } = new EditPanel();


        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ExamId { get; set; }

        //ユーザ項目
        [DisplayName("試験名")]
        [HtmlTag("input", "text")]
        [Required]
        [Grid(8)]
        public string ExamName { get; set; }

        [DisplayName("試験日")]
        [HtmlTag("input", "date")]
        [Required]
        [Grid(4)]
        public string ExamDate { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [Required]
        [Grid(5)]
        public string ExamineeId { get; set; }

        [DisplayName("カテゴリ")]
        [HtmlTag("input", "checkboxes")]
        [Options("Categories", "")]
        [Grid(5)]
        public string[] CategoryIds { get; set; }

        [DisplayName("ｶﾃｺﾞﾘ追加")]
        [HtmlTag("button")]
        [Event("addCategory","todo")]
        [Grid(2)]
        public string BtnAddCategory { get; set; }



        [DisplayName("国語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Autofocus]
        [Grid(3)]
        public decimal? KokugoScore { get; set; }

        [DisplayName("国語(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? KokugoAveScore { get; set; }

        [DisplayName("数学")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? SugakuScore { get; set; }

        [DisplayName("数学(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? SugakuAveScore { get; set; }

        [DisplayName("理科")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? RikaScore { get; set; }

        [DisplayName("理科(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? RikaAveScore { get; set; }


        [DisplayName("社会")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? ShakaiScore { get; set; }

        [DisplayName("社会(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? ShakaiAveScore { get; set; }

        [DisplayName("英語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? EigoScore { get; set; }

        [DisplayName("英語(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? EigoAveScore { get; set; }


        [DisplayName("メモ")]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        public string Memo { get; set; }
    }
}
