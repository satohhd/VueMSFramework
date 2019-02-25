using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Table
{

    public class ControlPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","refer")]
        public string BtnTbList { get; set; }

        [DisplayName("データエクスポート")]
        [HtmlTag("button")]
        [Event("export","refere")]
        public string BtnExport { get; set; }

        [DisplayName("データインポート")]
        [HtmlTag("button")]
        [Event("import/load","import")]
        public string BtnImport { get; set; }

    }

    public class  Refere : ViewModel
    {
        [DisplayName("panel")]
        [HtmlTag("panel")]
        public ControlPanel Panel { get; set; }


        //システム項目
        [DisplayName("id")]
        [HtmlTag("input", "hidden")]
        public string TableId { get; set; }

        [DisplayName("テーブル")]
        [HtmlTag("text-block")]
        public string TableName { get; set; }

        //ユーザ項目
        [DisplayName("タブ")]
        [HtmlTag("tabs")] 
        public TableTabs Tabs { get; set; } = new TableTabs();

    }

    public class TableTabs
    {
        public string ActiveTabName { get; set; } = "tab1";

        [DisplayName("データ")]
        [HtmlTag("tab")]
        public RowDataTab Tab1 { get; set; } = new RowDataTab();

        [DisplayName("テーブル定義")]
        [HtmlTag("tab")]
        public DefinitionTab Tab2 { get; set; } = new DefinitionTab();

    }
    public class DefinitionTab
    {

        [DisplayName("テーブル定義")]
        [HtmlTag("table")]
        [Grid(6)]
        public List<ColumnList> ColumnList { get; set; }


    }
    public class RowDataTab
    {

        [DisplayName("データ")]
        [HtmlTag("table")]
        public List<object> RowDataList { get; set; }

        [DisplayName("ページャー")]
        [HtmlTag("pagination")]
        [Event("search/load","search")]
        public Pagination RowDataPager { get; set; } = new Pagination();
    }
    public class ColumnList : ViewModel
    {

        //システム項目
        public string ColumnId { get; set; }

        //ユーザ項目
        [DisplayName("項目")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string ColumnName { get; set; }

        [DisplayName("属性")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string DataType { get; set; }


    }
}

