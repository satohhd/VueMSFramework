using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Keicho
{
    public class RemovePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove", isVerify:true)]
        [Redirect("search/load","search")]
        public string BtnRemove { get; set; }


    }
    public class Remove : ViewModel
    {

        //パネル
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public RemovePanel Panel { get; set; }

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
        [ReadOnly(true)]
        public string KeichoClass { get; set; }

        [DisplayName("慶弔種類")]
        [Required]
        [StringLength(10)]
        [HtmlTag("input", "toggle")]
        [Options("KeichoTypeId")]
        [ReadOnly(true)]
        public string KeichoTypeId { get; set; }

        [DisplayName("件名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Title { get; set; }

        [DisplayName("あげた人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string GiveUserName { get; set; }

        [DisplayName("もらった人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string TakeUserName { get; set; }

        [DisplayName("慶弔日")]
        [HtmlTag("input", "date")]
        [ReadOnly(true)]
        public string KeichoDate { get; set; }

        [DisplayName("金額")]
        [Range(0, 100000)]
        [HtmlTag("input", "number")]
        [ReadOnly(true)]
        public decimal Money { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [ReadOnly(true)]
        public string Notes { get; set; }


    }

}

