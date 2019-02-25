using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Group
{


    [DisplayName("検索")]
    [Description("説明")]
    public class Search : ViewModel
    {

        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public SearchPanel Panel { get; set; }
 
        [DisplayName("リスト")]
        [HtmlTag("table")]
        public List<GroupList> GroupList { get; set; }
    }



    public class SearchPanel
    {

        [DisplayName("メール管理へ")]
        [HtmlTag("button")]
        [Event("search/load","search",pageName: "mail")]
        public string BtnMailMng { get; set; }

        [DisplayName("グループ登録")]
        [HtmlTag("button")]
        [Event("create","search")]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search",isVerify:true)]
        public string BtnSearch { get; set; }

    }
    public class GroupList : ViewModel
    {

        [DisplayName("編集")]
        [Sortable(false)]
        [Event("edit/load","edit",paramItems: "groupId")]
        public string BtnEdit { get; set; }

        //システム項目
        [DisplayName("グループID")]
        [HtmlTag("input", "hidden")]
        public string GroupId { get; set; }

        //ユーザ項目
        [DisplayName("グループ名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string GroupName { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        public int OrderBy { get; set; }

        [DisplayName("表示色")]
        [StringLength(10)]
        [HtmlTag("input", "text")]
        public string Color { get; set; }

        [DisplayName("アイコン")]
        [HtmlTag("input", "text")]
        public string IconUrl { get; set; }

        [DisplayName("削除")]
        [Sortable(false)]
        [Event("remove/load","remove", paramItems: "groupId")]
        public string BtnRemove { get; set; }

    }

   
}

