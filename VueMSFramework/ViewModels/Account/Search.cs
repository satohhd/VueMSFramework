using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Account
{

    public class SearchPanel
    {
        [DisplayName("新規登録")]
        [HtmlTag("button")]
        [Event("create/load","create" )]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search",isVerify:true)]
        public string BtnSearch { get; set; }


    }

    public class  Search : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public SearchPanel Panel { get; set; }


        [DisplayName("絞込条件：名称の部分一致")]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(4)]
        public string Keywords { get; set; }


        [DisplayName("結果リスト")]
        [HtmlTag("table")]
        public List<SearchList> SearchList { get; set; }
    }

    public class SearchList : ViewModel
    {

        [DisplayName("編集")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit", isVerify: true,paramItems: "accountId")]
        public string BtnEdit { get; set; }


        //ユーザ項目
        [DisplayName("アカウントID")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(128)]
        [Grid(4)]
        public string AccountId { get; set; }

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

