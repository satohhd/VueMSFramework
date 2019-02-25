using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Itsukara
{
    [DisplayName("編集")]
    [Description("編集します")]
    public class Edit : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public EditPanel Panel { get; set; }

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
        public string Goods { get; set; }

        [DisplayName("規格")]
        [StringLength(200)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Std { get; set; }

        [DisplayName("使用開始日")]
        [HtmlTag("input", "date")]
        [Grid(4)]
        public string PurchaseDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        [DisplayName("期限日")]
        [HtmlTag("input", "date")]
        [Grid(4)]
        public string ExpirationDate { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        public string Notes { get; set; }


    }
    public class EditPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }

        [DisplayName("更新")]
        [HtmlTag("button")]
        [Event("edit/update","edit", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnUpdate { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/load","remove", paramItems: "itsukaraId")]
        public string BtnRemove { get; set; }


    }

}
