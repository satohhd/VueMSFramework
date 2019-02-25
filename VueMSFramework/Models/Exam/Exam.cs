using System;
using System.ComponentModel;
using VueMSFramework.Models.Common;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;

namespace VueMSFramework.Models
{
    public class Exam : BaseModel
    {

        [Key]
        public string ExamId { get; set; }

        //ユーザ項目

        [DisplayName("試験名")]
        [HtmlTag("input", "text")]
        [Required]
        public string ExamName { get; set; }


        [DisplayName("試験日")]
        [HtmlTag("input", "date")]
        public DateTime? ExamDate { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [Required]
        public string ExamineeId { get; set; }

        [DisplayName("国語")]
        [HtmlTag("input", "number")]
        [Range(0,100)]
        public decimal? KokugoScore { get; set; }
        [DisplayName("数学")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? SugakuScore { get; set; }
        [DisplayName("理科")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? RikaScore { get; set; }
        [DisplayName("社会")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? ShakaiScore { get; set; }
        [DisplayName("英語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? EigoScore { get; set; }


        [DisplayName("国語(平均)")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? KokugoAveScore { get; set; }
        [DisplayName("数学(平均)")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? SugakuAveScore { get; set; }
        [DisplayName("理科(平均)")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? RikaAveScore { get; set; }
        [DisplayName("社会(平均)")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? ShakaiAveScore { get; set; }
        [DisplayName("英語(平均)")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        public decimal? EigoAveScore { get; set; }

        [DisplayName("メモ")]
        public string Memo { get; set; }



    }
}
