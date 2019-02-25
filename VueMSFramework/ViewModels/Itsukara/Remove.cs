using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Itsukara
{

    [DisplayName("削除")]
    [Description("削除します")]
    public class Remove : ViewModel
    {
        //パネル
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public ErasePanel Panel { get; set; }

        //システム項目
        [DisplayName("id")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ItsukaraId { get; set; }

        [DisplayName("商品")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(8)]
        [Autofocus]
        [ReadOnly(true)]
        public string Goods { get; set; }

        [DisplayName("規格")]
        [StringLength(200)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        [ReadOnly(true)]
        public string Std { get; set; }

        [DisplayName("使用開始日")]
        [HtmlTag("input", "date")]
        [Grid(4)]
        [ReadOnly(true)]
        public string PurchaseDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        [DisplayName("期限日")]
        [HtmlTag("input", "date")]
        [Grid(4)]
        [ReadOnly(true)]
        public string ExpirationDate { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        [ReadOnly(true)]
        public string Notes { get; set; }

    }
    public class ErasePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnRemove { get; set; }


    }

}

