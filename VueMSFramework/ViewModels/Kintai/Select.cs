using System.Collections.Generic;
using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Kintai
{
    public class  Select : ViewModel
    {


        [DisplayName("社員リスト")]
        [HtmlTag("table")]
        [Grid(6)]
        public List<ShainList> ShainList { get; set; }

    }

    public class ShainList : ViewModel
    {

        [DisplayName("社員ID")]
        [HtmlTag("input", "hidden")]
        public string ShainId { get; set; }

        [DisplayName("よみ順")]
        [HtmlTag("input", "text")]
        public string YmgnIchimoji { get; set; }

        [DisplayName("社員番号")]
        [HtmlTag("input", "text")]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        public string Shmi { get; set; }

        [DisplayName("選択")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("search/load","search",paramItems:"shainId")]
        public string BtnRefer { get; set; }


    }

}

