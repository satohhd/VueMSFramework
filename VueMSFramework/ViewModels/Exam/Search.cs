using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Exam
{
    public class SearchPanel
    {
        [DisplayName("新規登録")]
        [HtmlTag("button")]
        [Event("create","create")]
        public string BtnCreate { get; set; }

    }
    [DisplayName("検索")]
    [Description("説明")]
    public class  Search : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public SearchPanel Panel { get; set; } = new SearchPanel();

        [DisplayName("条件")]
        [HtmlTag("input", "hidden")]
        public string Condition { get; set; }

        [DisplayName("現在ページ")]
        [HtmlTag("input", "hidden")]
        public int CurrentPageNumber { set; get; } = 1;

        [DisplayName("受験者")]
        [HtmlTag("text-block")]
        [Grid(2)]
        public string ExamineeId { get; set; }


        [DisplayName("カテゴリ")]
        [HtmlTag("input", "checkboxes")]
        //[Options("Categories", "")]
        [Grid(8)]
        [Autofocus]
        public string[] CategoryIds { get; set; }

        [DisplayName("ｶﾃｺﾞﾘ追加")]
        [HtmlTag("button")]
        [Event("category/load", "category", paramItems: "examineeId")]
        [Grid(2)]
        public string BtnAddCategory { get; set; }


        [DisplayName("絞込条件：名称の部分一致")]
        [HtmlTag("input", "hidden")]
        public string Keywords { get; set; }


        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search", isVerify: true)]
        [Grid(3)]
        public string BtnSearch { get; set; }

        //ユーザ項目
        [DisplayName("タブ")]
        [HtmlTag("tabs")]
        public ResultTabs Tabs { get; set; } = new ResultTabs();

    }

    public class ResultTabs
    {

        public string ActiveTabName { get; set; } = "tab1";

        [DisplayName("一覧")]
        [HtmlTag("tab")]
        public ExamTab Tab1 { get; set; } = new ExamTab();

        [DisplayName("総合計")]
        [HtmlTag("tab")]
        public ExamChrtTab3 Tab2 { get; set; } = new ExamChrtTab3();

        [DisplayName("国語")]
        [HtmlTag("tab")]
        public ExamChrtTab3 Tab3 { get; set; } = new ExamChrtTab3();

        [DisplayName("理科")]
        [HtmlTag("tab")]
        public ExamChrtTab3 Tab4 { get; set; } = new ExamChrtTab3();

        [DisplayName("社会")]
        [HtmlTag("tab")]
        public ExamChrtTab3 Tab5 { get; set; } = new ExamChrtTab3();

        [DisplayName("英語")]
        [HtmlTag("tab")]
        public ExamChrtTab3 Tab6 { get; set; } = new ExamChrtTab3();

        [DisplayName("数学")]
        [HtmlTag("tab")]
        public ExamChrtTab3 Tab7 { get; set; } = new ExamChrtTab3();

    }

    public class ExamTab
    {
        [DisplayName("データ")]
        [HtmlTag("table")]
        public List<ExamList> DataList { get; set; }


        [DisplayName("受験者")]
        [HtmlTag("input", "hidden")]
        public string ExamineeId { get; set; }

        [DisplayName("絞込条件：名称の部分一致")]
        [HtmlTag("input", "hidden")]
        [Autofocus]
        public string Keywords { get; set; }


        [DisplayName("ページャー")]
        [HtmlTag("pagination")]
        [Event("pagination","search")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class ExamChrtTab
    {
        [DisplayName("グラフ")]
        [HtmlTag("chart", "area")]
        public string ExamList { get; set; }

    }
    public class ExamChrtTab2
    {
        [DisplayName("グラフ")]
        [HtmlTag("chart", "area2")]
        public string ExamList2 { get; set; }

    }
    public class ExamChrtTab3
    {
        [DisplayName("グラフ3")]
        [HtmlTag("chart", "highcharts")]
        public HigthChartOption Option { get; set; } = new HigthChartOption();

    }
    public class HigthChartOption
    {
        public Title Title { get; set; } = new Title { Text= "", X=0};
        public Title Subtitle { get; set; } = new Title { Text = "", X = -0 };
        public XAxis XAxis { get; set; } = new XAxis();
        public YAxis YAxis { get; set; } = new YAxis();
        public Credits Credits { get; set; } = new Credits();
        public Tooltip Tooltip { get; set; } = new Tooltip { ValueSuffix="点"};
        public Legend Legend { get; set; } = new Legend { Layout= "vertical",Align= "right",VerticalAlign= "middle",BorderWidth=0 };
        public IEnumerable<Sery> Series { get; set; } = new Sery[] { new Sery { Type = "column", Name = "Cul", Data = new int[] { } } ,
                                                                    new Sery { Type = "spline", Name = "Abe", Data = new int[] { } }
          };
    }
    public class XAxis
    {
        public IEnumerable<string> Categories { get; set; } = new string[] {};
    }
    public class YAxis
    {
        public Title Title { get; set; } = new Title { Text= "採点", X=0};
        public int Max { get; set; } = 100;
        public IEnumerable<PlotLine> PlotLines { get; set; } = new PlotLine[0];

    }
    public class PlotLine
    {
        public int Value { get; set; } = 0;
        public int Width { get; set; } = 1;
        public string Color { get; set; } = "#808080";
    }

    public class Title
    {
        public string Text { get; set; }
        public int X { get; set; } = 0;
    }
    public class Sery
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Data { get; set; }

    }
    public class Legend
    {
        public string Layout { get; set; }
        public string Align { get; set; }
        public string VerticalAlign { get; set; }
        public int BorderWidth { get; set; }

    }
    public class Credits
    {
        public bool Enabled { get; set; } = true;
    }
    public class Tooltip
    {
        public string ValueSuffix { get; set; }
    }
    public class ExamList : ViewModel
    {

        [DisplayName("編集")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit",paramItems: "examId")]
        public string BtnEdit { get; set; }

        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ExamId { get; set; }

        //ユーザ項目

        [DisplayName("試験名")]
        [HtmlTag("input", "text")]
        [Required]
        public string ExamName { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [Required]
        public string ExamineeId { get; set; }

        [DisplayName("試験日")]
        [HtmlTag("input", "date")]
        public string ExamDate { get; set; }

        [DisplayName("国語")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
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


        [DisplayName("総合計")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        public decimal TotalScore
        {
            get
            {
                return (decimal)(KokugoScore ?? 0)
                    + (decimal)(SugakuScore ?? 0)
                    + (decimal)(RikaScore ?? 0)
                    + (decimal)(ShakaiScore ?? 0)
                    + (decimal)(EigoScore ?? 0);
            }
        }

        public decimal? KokugoAveScore { get; set; }
        public decimal? SugakuAveScore { get; set; }
        public decimal? RikaAveScore { get; set; }
        public decimal? ShakaiAveScore { get; set; }
        public decimal? EigoAveScore { get; set; }

        [DisplayName("総合計(平均)")]
        [HtmlTag("input", "number")]
        [DecimalPoint("0.1")]
        public decimal TotalAveScore
        {
            get
            {
                return (decimal)(KokugoAveScore ?? 0)
                    + (decimal)(SugakuAveScore ?? 0)
                    + (decimal)(RikaAveScore ?? 0)
                    + (decimal)(ShakaiAveScore ?? 0)
                    + (decimal)(EigoAveScore ?? 0);
            }
        }



    }
}

