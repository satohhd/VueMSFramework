using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.MSection
{
    public class Index : ViewModel
    {

        public class Panel
        {

            [DisplayName("①")]
            [HtmlTag("button")]
            [Event("section1/load","section1")]
            public string BtnSec1 { get; set; }

            [DisplayName("②")]
            [HtmlTag("button")]
            [Event("section2/load","section2")]
            public string BtnSec2 { get; set; }

            [DisplayName("③")]
            [HtmlTag("button")]
            [Event("section3/load","section3")]
            public string BtnSec3 { get; set; }

            [DisplayName("④")]
            [HtmlTag("button")]
            [Event("section4/load","section4")]
            public string BtnSec4 { get; set; }

        }
        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public Panel Panel2 { get; set; }

        [Grid(6)]
        [DisplayName("セクション１")]
        [HtmlTag("window")]
        public Section1 Section1 { get; set; }

        [Grid(6)]
        [DisplayName("セクション２")]
        [HtmlTag("window")]
        public Section1 Section2 { get; set; }

        [Grid(6)]
        [DisplayName("セクション３")]
        [HtmlTag("window")]
        public Section1 Section3 { get; set; }

        [Grid(6)]
        [DisplayName("セクション４")]
        [HtmlTag("window")]
        public Section1 Section4 { get; set; }
    }



}

