using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Itsukara
{
    public class CreatePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","create")]
        public string BtnBack { get; set; }

        [DisplayName("登録")]
        [HtmlTag("button")]
        [Event("create/insert","create", isVerify: true)]
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
        public string ItsukaraId { get; set; }

        //ユーザ項目
        [DisplayName("商品")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Help("100文字以内で入力してください")]
        [InvalidFeedback("必須")]
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

}

