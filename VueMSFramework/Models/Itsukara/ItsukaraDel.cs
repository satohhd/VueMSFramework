using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models.Itsukara
{
    public class ItsukaraDel : DeleteBaseModel
    {

        [Required]
        [StringLength(100)]
        public string ItsukaraId { get; set; }

        //ユーザ項目
        [DisplayName("商品")]
        [Required]
        [StringLength(100)]
        public string Goods { get; set; }

        [DisplayName("規格")]
        [StringLength(200)]
        public string Std { get; set; }

        [DisplayName("購入日")]
        public DateTime? PurchaseDate { get; set; }

        [DisplayName("期限日")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        public string Notes { get; set; }
    }
}
