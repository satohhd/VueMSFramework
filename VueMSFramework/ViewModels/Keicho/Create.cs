using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Keicho
{

    public class CreatePanel
    {


        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","create")]
        public string BtnBack { get; set; }

        [DisplayName("登録")]
        [HtmlTag("button")]
        [Event("create/insert","create", isVerify:true)]
        [Redirect("search/load","search")]
        public string BtnStore { get; set; }


    }
    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public CreatePanel Panel { get; set; } 


        //システム項目
        [DisplayName("id")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string KeichoId { get; set; }

        //ユーザ項目
        [DisplayName("慶弔区分")]
        [Required]
        [StringLength(10)]
        [Autofocus]
        [HtmlTag("input", "toggle")]
        [Options("KeichoClass")]
        [InvalidFeedback("慶弔区分を選んでください。")]
        public string KeichoClass { get; set; }


        [DisplayName("慶弔種類")]
        [Required]
        [StringLength(10)]
        [HtmlTag("input", "toggle")]
        [Options("KeichoTypeId")]
        [InvalidFeedback("慶弔種類を選んでください。")]
        public string KeichoTypeId { get; set; }

        [DisplayName("件名")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Title { get; set; }

        [DisplayName("あげた人")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string GiveUserName { get; set; }

        [DisplayName("もらった人")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string TakeUserName { get; set; }

        [DisplayName("慶弔日")]
        [Required]
        [HtmlTag("input", "date")]
        public string KeichoDate { get; set; }

        [DisplayName("金額")]
        [Range(0, 100000)]
        [HtmlTag("input", "number")]
        public decimal Money { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        public string Notes { get; set; }


    }

}

