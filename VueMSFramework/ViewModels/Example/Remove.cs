using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Example
{
    public class RemovePanel
    {


        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove", isVerify: true, isConfirm: true)]
        public string BtnRemove { get; set; }


    }
    public class Remove : ViewModel
    {
        //パネル
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public CreatePanel Panel { get; set; }

        //ユーザ項目
        [Key]
        [DisplayName("ID")]
        [Required]
        [HtmlTag("input", "hidden")]
        [StringLength(128)]
        [Grid(4)]
        public string ExampleId { get; set; }

        [DisplayName("メールアドレス")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(256)]
        [Grid(4)]
        public string Email { get; set; }

        [DisplayName("端末アドレス")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(40)]
        [Grid(4)]
        public string TermAddr { get; set; }

        [DisplayName("リモートアドレス")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(40)]
        [Grid(4)]
        public string RemoteAddr { get; set; }

        [DisplayName("ユーザ名")]
        [StringLength(256)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string UserName { get; set; }


        [DisplayName("チケット")]
        [StringLength(3000)]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        public string Ticket { get; set; }

        [DisplayName("本人確認")]
        [HtmlTag("input", "radio")]
        [Grid(4)]
        public bool EmailConfirmed { get; set; } = false;


        [DisplayName("管理者確認")]
        [HtmlTag("input", "radio")]
        [Grid(4)]
        public bool ManagerConfirmed { get; set; } = false;


        [DisplayName("ロック")]
        [HtmlTag("input", "radio")]
        [Grid(4)]
        public bool LockoutEnabled { get; set; } = false;

        [DisplayName("アクセスカウント")]
        [HtmlTag("input", "number")]
        [Grid(4)]
        public decimal AccessCount { get; set; } = 0;

    }

}

