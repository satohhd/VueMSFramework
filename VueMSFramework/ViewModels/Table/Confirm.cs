using System.Collections.Generic;
using System.ComponentModel;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;
using VueMSFramework.Core.Utils.DataAnnotations;

namespace VueMSFramework.ViewModels.Table
{

    public class Confirm : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public ConfirmPanel Panel { get; set; } = new ConfirmPanel();

        [DisplayName("インポートファイル")]
        [HtmlTag("input", "file")]
        [Help("10Mbyte以下のファイルをしてください。")]
        public File File1 { get; set; }

        [DisplayName("データ")]
        [HtmlTag("table")]
        public List<RowDataList> RowDataList { get; set; }

        [DisplayName("ページャー")]
        [HtmlTag("pager")]
        [Event("search/load","search")]
        public Pagination RowDataPager { get; set; } = new Pagination();

    }
    public class ConfirmPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","confirm")]
        public bool BtnBack { get; set; }

        [DisplayName("取込実行")]
        [HtmlTag("button")]
        [Event("confirm/execute","confirm", isVerify:true, isConfirm:false)]
        public bool BtnExecute { get; set; }


    }

    public class RowDataList : ViewModel
    {

        //システム項目
        public string RowDataId { get; set; }

        //ユーザ項目
        //動的プロパティ

    }


}
