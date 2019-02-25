using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Table
{

    public class Import : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public ImportPanel Panel { get; set; }

        [DisplayName("テーブル")]
        [HtmlTag("input", "text")]
        [Required]
        public string TableId { get; set; }

        [DisplayName("インポートファイル")]
        [HtmlTag("input", "file")]
        [Help("10Mbyte以下のファイルを指定ください。")]
        [Required]
        public File File1 { get; set; }

        [DisplayName("ファイル名")]
        [HtmlTag("input", "hidden")]
        public string FileName { get; set; }

        [DisplayName("データ")]
        [HtmlTag("table")]
        public List<object> RowDataList { get; set; }


    }
    public class ImportPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","import")]
        public string BtnBack { get; set; }

        [DisplayName("取込実行")]
        [HtmlTag("button")]
        [Event("import/execute","import", isVerify: true,isConfirm:true)]
        public string BtnExecute { get; set; }

    }


}
