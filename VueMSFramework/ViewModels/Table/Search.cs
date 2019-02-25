using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Table
{
    public class Search : ViewModel
    {
        [DisplayName("テーブルリスト")]
        [HtmlTag("table", "tableName", true)]
        [Grid(3)]
        public List<SearchList> SearchList { get; set; }

    }

    public class SearchList : ViewModel
    {

        //システム項目
        public string TableId { get; set; }

        //ユーザ項目
        [DisplayName("テーブル")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string TableName { get; set; }


        [DisplayName("選択")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("refere/load","refere", paramItems: "tableId")]
        public string BtnRefer { get; set; }

    }


}

