using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Home
{

    [DisplayName("検索")]
    [Description("検索します")]
    public class  Body : ViewModel
    {
   

        [DisplayName("絞込条件：名称の部分一致")]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(4)]
        public string Keywords { get; set; }


        [DisplayName("結果リスト")]
        [HtmlTag("table")]
        public List<ActivityList> ActivityList { get; set; }
    }
    public class ActivityList : ViewModel
    {

        [DisplayName("編集")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit", isVerify: true,paramItems:"activityId")]
        public string BtnEdit { get; set; }

        //システム項目
        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string ActivityId { get; set; }

        //ユーザ項目
        [DisplayName("表題")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(8)]
        [Autofocus]
        public string Title { get; set; }

        [DisplayName("内容")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [Grid(12)]
        public string Content { get; set; }


        [DisplayName("カテゴリ")]
        [StringLength(10)]
        [HtmlTag("input", "radio")]
        [Options("Contents", "Category")]
        [Grid(6)]
        public string Category { get; set; }


        [DisplayName("削除")]
        [Sortable(false)]
        [Event("remove/load", "remove", paramItems: "activityId")]
        public string BtnRemove { get; set; }



    }


}

