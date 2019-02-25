using System.Collections.Generic;
using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Exam
{

    [DisplayName("選択")]
    [Description("説明")]
    public class  Select : ViewModel
    {
        [DisplayName("新規登録")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        public string BtnCreate { get; set; }


        [DisplayName("受験者リスト")]
        [HtmlTag("table")]
        [Grid(3)]
        public List<ExamineeList> ExamineeList { get; set; }

    }
    public class ExamineeList : ViewModel
    {

        [DisplayName("受験者")]
        [HtmlTag("link")]
        [Event("search/load","search",paramItems:"examineeId")]
        public string ExamineeId { get; set; }

    }

}

