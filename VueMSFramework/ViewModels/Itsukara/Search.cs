using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Itsukara
{
    [DisplayName("検索")]
    [Description("説明")]
    public class  Search : ViewModel
    {

        //パネル
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public SearchPanel Panel { get; set; }


        [DisplayName("絞込条件：名称の部分一致")]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(4)]
        public string Keywords { get; set; }


        [DisplayName("いつからリスト")]
        [HtmlTag("table", "purchaseDate", true)]
        public List<ItsukaraList> ItsukaraList { get; set; }
    }
    public class SearchPanel
    {

        [DisplayName("新規登録")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search",isVerify: true)]
        public string BtnSearch { get; set; }

    }
    [DisplayName("いつからリスト")]
    [Description("説明")]
    public class ItsukaraList : ViewModel
    {

        [DisplayName("編集")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit",isVerify: true,paramItems: "itsukaraId")]
        public string BtnEdit { get; set; }

        //システム項目
        [DisplayName("Id")]
        [Required]
        [HtmlTag("input", "hidden")]
        public string ItsukaraId { get; set; }

        //ユーザ項目
        [DisplayName("商品")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Goods { get; set; }

        [DisplayName("規格")]
        [StringLength(200)]
        [HtmlTag("input", "hidden")]
        public string Std { get; set; }

        [DisplayName("使用開始日")]
        [HtmlTag("input", "date")]
        public string PurchaseDate { get; set; }

        [DisplayName("使用日数")]
        [HtmlTag("input", "text")]
        public string UseDays
        {
            get
            {
                if (DateTime.TryParse(PurchaseDate, out DateTime dt1))
                {
                    TimeSpan span = DateTime.Now.Date - dt1;

                    if (span.Days >= 0)
                    {
                        return span.Days.ToString();
                    }
                    else
                    {
                        return "未使用";

                    }
                }
                else
                {
                    return null;
                }
            }
        }


        [DisplayName("期限日")]
        [HtmlTag("input", "hidden")]
        public string ExpirationDate { get; set; }

        //[DisplayName("残り日数")]
        //[HtmlTag("input", "text")]
        //public string DaysLeft
        //{
        //    get
        //    {
        //        if (DateTime.TryParse(ExpirationDate, out DateTime dt1))
        //        {
        //            TimeSpan span = dt1 - DateTime.Now.Date;

        //            if (span.Days >= 0)
        //            {
        //                return span.Days.ToString();
        //            }
        //            else
        //            {
        //                return "期限切れ";

        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "hidden")]
        public string Notes { get; set; }

        [DisplayName("削除")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("remove/load","remove", paramItems: "itsukaraId")]
        public string BtnRemove { get; set; }

    }

}

