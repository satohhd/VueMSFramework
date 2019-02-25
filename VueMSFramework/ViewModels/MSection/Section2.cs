using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.MSection
{

 
    public class Section2 : ViewModel
    {

        [DisplayName("項目2")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string Item1 { get; set; }

 

    }

}

