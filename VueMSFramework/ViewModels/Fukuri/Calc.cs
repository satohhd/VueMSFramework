using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Fukuri
{

    [DisplayName("計算")]
    [Description("計算する")]
    public class  Calc : ViewModel
    {

        [DisplayName("元金")]
        [HtmlTag("input", "number")]
        [Range(0,1000000)]
        [Grid(6,3,3)]
        [Required]
        [Event("calc","calc", isVerify:true)]
        public decimal Gnkn { get; set; }

        [DisplayName("金利")]
        [HtmlTag("input", "number")]
        [Range(0, 100)]
        [Grid(6, 3, 3)]
        [Required]
        [Event("calc","calc", isVerify:true)]
        public decimal Knr { get; set; }


        [DisplayName("表示")]
        [HtmlTag("input","textarea")]
        [ReadOnly(true)]
        [Grid(6, 3, 3)]
        public string Display { get; set; }


        //[DisplayName("クリア")]
        //[HtmlTag("button")]
        //[Trigger("clear")]
        //[Grid(6, 3, 3)]
        //public string BtnCrear { get; set; }

        //[DisplayName("保存")]
        //[HtmlTag("button")]
        //[Trigger("save")]
        //[Grid(6, 3, 3)]
        //public string BtnSave { get; set; }

        [DisplayName("計算結果")]
        [HtmlTag("table")]
        [Grid(6, 3, 3)]
        public List<ResultList> ResultList { get; set; }


    }
    [DisplayName("結果")]
    [Description("結果を表示する")]
    public class ResultList : ViewModel
    {

        //ユーザ項目

        [DisplayName("N年目")]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string Nnm { get; set; }

        [DisplayName("金額")]
        [HtmlTag("input", "number")]
        [SortableAttribute(false)]
        public decimal Kgk { get; set; }


    }



}

