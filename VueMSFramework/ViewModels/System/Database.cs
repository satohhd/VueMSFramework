using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.System
{
    public class  Database : ViewModel
    {

        [DisplayName("データベースのマイグレーション")]
        [HtmlTag("h","2")]
        public string Group1 { get; set; }


        [DisplayName("マイグレーション実行")]
        [HtmlTag("button")]
        [Event("migrate","database")]
        public string BtnMigrate { get; set; }


        [DisplayName("データベース初期化")]
        [HtmlTag("h", "2")]
        public string Group2 { get; set; }

        [DisplayName("テーブル全削除")]
        [HtmlTag("button")]
        [Event("tabledrop","database")]
        public string BtnAllErase { get; set; }

        [DisplayName("テーブル作成")]
        [HtmlTag("button")]
        [Event("tablecreate","database")]
        public string BtnCreate { get; set; }



    }



}

