using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Home
{

    [DisplayName("Footer")]
    public class Footer : ViewModel
    {
        [DisplayName("Copyright")]
        [HtmlTag("p")]
        public string Copyright { get; set; } = "Copyright (C) 2019 ae Brain. All Rights Reserved.";

    }

}

