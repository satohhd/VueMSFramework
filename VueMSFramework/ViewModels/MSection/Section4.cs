using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.MSection
{

 
    public class Section4 : ViewModel
    {

        [DisplayName("項目4")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string Item1 { get; set; }

 

    }

}

