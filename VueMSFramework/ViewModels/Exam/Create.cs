using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Exam
{
    [DisplayName("新規")]
    [Description("説明")]
    public class CreatePanel
    {
        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","create")]
        public string BtnBack { get; set; }

        [DisplayName("登録")]
        [HtmlTag("button")]
        [Event("create/insert","create",isVerify:true)]
        [Redirect("search/load","search")]
        public string BtnStore { get; set; }

    }
    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {
        public Create()
        {
            ExamDate = DateTime.Now.ToString("yyyy-MM-dd");
        }
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public CreatePanel Panel { get; set; } = new CreatePanel();

        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ExamId { get; set; }

        //ユーザ項目
        [DisplayName("試験名")]
        [HtmlTag("input", "text")]
        [Required]
        [Autofocus]
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
        [Grid(6)]
        public string ExamineeId { get; set; }

        [DisplayName("カテゴリ")]
        [HtmlTag("input", "checkboxes")]
        // [Options("Categories", "")]
        [Grid(5)]
        public string[] CategoryIds { get; set; }

        [DisplayName("ｶﾃｺﾞﾘ追加")]
        [HtmlTag("button")]
        [Event("category/addCategory","category")]
        [Grid(1)]
        public string BtnAddCategory { get; set; }


        [DisplayName("国語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? KokugoScore { get; set; }

        [DisplayName("国語(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        [Range(0, 100)]
        [Required]
        [Grid(3,0,6)]
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
        [Grid(3,0,6)]
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
        [Grid(3,0,6)]
        public decimal? RikaAveScore { get; set; }

        [DisplayName("社会")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? ShakaiScore { get; set; }

        [DisplayName("社会(平均)")]
        [DecimalPoint("0.1")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3,0,6)]
        public decimal? ShakaiAveScore { get; set; }

        [DisplayName("英語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3)]
        public decimal? EigoScore { get; set; }

        [DisplayName("英語(平均)")]
        [DecimalPoint("0.1")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Required]
        [Grid(3,0,6)]
        public decimal? EigoAveScore { get; set; }


        [DisplayName("メモ")]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        public string Memo { get; set; }

    }

}

