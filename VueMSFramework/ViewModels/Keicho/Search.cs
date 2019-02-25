using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Keicho
{
    public class SearchPanel
    {


        [DisplayName("新規登録")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search",isVerify:true)]
        public string BtnSearch { get; set; }


    }
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
        public string Keywords { get; set; }

        //ユーザ項目
        [DisplayName("タブ")]
        [HtmlTag("tabs")]
        public ResultTabs Tabs { get; set; } = new ResultTabs();

    }

    public class ResultTabs
    {

        public string ActiveTabName { get; set; } = "tab1";

        [DisplayName("もらいました")]
        [HtmlTag("tab")]
        public TakeTab Tab1 { get; set; } = new TakeTab();


        [DisplayName("あげました")]
        [HtmlTag("tab")]
        public GiveTab Tab2 { get; set; } = new GiveTab();

    }
    public class GiveTab
    {

        [DisplayName("テーブル定義")]
        [HtmlTag("table")]
        public List<GiveList> GiveList { get; set; }

        [DisplayName("ページャー")]
        [HtmlTag("pagination", "search")]
        public Pagination GIvePager { get; set; } = new Pagination();

    }
    public class TakeTab
    {
        [DisplayName("データ")]
        [HtmlTag("table")]
        public List<TakeList> TakeList { get; set; }

        [DisplayName("ページャー")]
        [HtmlTag("pagination", "search")]
        public Pagination TakePager { get; set; } = new Pagination();
    }
    public class GiveList : ViewModel
    {


        [DisplayName("編集")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit", paramItems: "keichoId")]
        public string BtnEdit { get; set; }


        //システム項目
        [DisplayName("id")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string KeichoId { get; set; }


        //ユーザ項目
        [DisplayName("慶弔種類")]
        [HtmlTag("input", "text")]
        public string KeichoTypeName { get; set; }

        [DisplayName("件名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Title { get; set; }

        [DisplayName("あげた人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string GiveUserName { get; set; }

        [DisplayName("もらった人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string TakeUserName { get; set; }

        [DisplayName("慶弔日")]
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


        [DisplayName("削除")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("remove","remove", paramItems: "keichoId")]
        public string BtnRemove { get; set; }

    }
    public class TakeList : ViewModel
    {

        [DisplayName("編集")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("edit/update","edit",paramItems: "keichoId")]
        public string BtnEdit { get; set; }


        //システム項目
        [DisplayName("id")]
        [HtmlTag("input", "hidden")]
        public string KeichoId { get; set; }

        //ユーザ項目

        [DisplayName("慶弔種類")]
        [HtmlTag("input", "text")]
        public string KeichoTypeName { get; set; }

        [DisplayName("件名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Title { get; set; }

        [DisplayName("あげた人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string GiveUserName { get; set; }

        [DisplayName("もらった人")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string TakeUserName { get; set; }

        [DisplayName("慶弔日")]
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

        [DisplayName("削除")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("remove/load","remove", paramItems: "keichoId")]
        public string BtnRemove { get; set; }
        
    }
}

