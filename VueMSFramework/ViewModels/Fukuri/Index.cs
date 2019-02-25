using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Fukuri
{
    [DisplayName("福利厚生")]
    [Description("説明")]
    public class Index : ViewModel
    {


        [DisplayName("計算機")]
        [HtmlTag("section")]
        public Calc Calc { get; set; }


    }



}

