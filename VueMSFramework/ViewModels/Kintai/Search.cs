using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Kintai
{

    public class SearchPanel
    {
        [DisplayName("戻る")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("back","search")]
        public string BtnSelect { get; set; }

    }
    public class YmPanel
    {

        [DisplayName("前")]
        [HtmlTag("button")]
        [Event("search/prev","search", isVerify: true)]
        public string BtnPrev { get; set; }

        [DisplayName("年月")]
        [HtmlTag("input", "text")]
        [RegularExpression(@"^20[0-9]{2}-[0-1]{0,1}[0-9]{1}$")]
        [StringLength(10)]
        [Required]
        public string YearMonth { get; set; }

        [DisplayName("次")]
        [HtmlTag("button")]
        [Event("search/next","search", isVerify: true)]
        public string BtnNext { get; set; }

        [DisplayName("EXCEL出力")]
        [HtmlTag("button")]
        [Event("search/output","search")]
        public string BtnOutput { get; set; }

        [DisplayName("EXCEL登録")]
        [HtmlTag("button")]
        [Event("import/load","import")]
        public string BtnImport { get; set; }

        [DisplayName("予定登録")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        public string BtnCreate { get; set; }

        [DisplayName("PDF出力")]
        [HtmlTag("button")]
        [Event("search/pdf","search", isVerify: true)]
        [Grid(1)]
        public string BtnPdf { get; set; }

        [DisplayName("一括申請")]
        [HtmlTag("button")]
        [Event("search/approval","search", isVerify: true, paramItems: "shainId")]
        [Grid(2)]
        public string BtnApproval { get; set; }

    }

    [DisplayName("勤怠")]
    [Description("説明")]
    public class  Search : ViewModel
    {

        [DisplayName("社員情報")]
        [HtmlTag("table")]
        public List<ShainInfo> ShainInfo { get; set; } = new List<ShainInfo>();

        //パネル
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public SearchPanel Panel { get; set; } = new SearchPanel();

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public YmPanel YmPanel { get; set; } = new YmPanel();

        [DisplayName("社員ID")]
        [HtmlTag("input", "hidden")]
        public string ShainId { get; set; }

        //ユーザ項目
        [DisplayName("タブ")]
        [HtmlTag("tabs")]
        public ResultTabs Tabs { get; set; } = new ResultTabs();

    }
    public class ShainInfo : ViewModel
    {
        [DisplayName("社員ID")]
        [HtmlTag("input", "hidden")]
        public string ShainId { get; set; }

        //ユーザ項目
        [DisplayName("社員番号")]
        [HtmlTag("input", "text")]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        public string Shmi { get; set; }

        [DisplayName("雇用形態")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Kykti { get; set; }

        [DisplayName("部門名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Bmnmi { get; set; }

        [DisplayName("拠点名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Kytnmi { get; set; }


        [DisplayName("編集")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit",pageName:"shain", paramItems: "shainId")]
        public string BtnRefer { get; set; }

    }
    public class ResultTabs
    {

        public string ActiveTabName { get; set; } = "tab1";

        [DisplayName("勤怠リスト")]
        [HtmlTag("tab")]
        public KintaiTab Tab1 { get; set; } = new KintaiTab();
    }
  
    public class KintaiTab
    {
        [DisplayName("データ")]
        [HtmlTag("table")]
        public List<KintaiList> KintaiList { get; set; }

        [DisplayName("ページャー")]
        [HtmlTag("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }

    public class KintaiList : ViewModel
    {

        [DisplayName("編集<br>&nbsp;")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit", paramItems: "shainId,shinbng,shmi,kintaiId,hdk",isVerify: true)]
        public string BtnEdit { get; set; }

        [DisplayName("勤怠ID")]
        [HtmlTag("input", "hidden")]
        public string KintaiId { get; set; }

        [DisplayName("社員ID")]
        [HtmlTag("input", "hidden")]
        public string ShainId { get; set; }

        [DisplayName("社員番号")]
        [HtmlTag("input", "hidden")]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [HtmlTag("input", "hidden")]
        public string Shmi { get; set; }

        //ユーザ項目
        public string Hdk { get; set; }

        [DisplayName("曜日")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "hidden")]
        public string Dow { get; set; }

        [DisplayName("日付<br>&nbsp;")]
        [Required]
        [HtmlTag("input", "date")]
        [SortableAttribute(false)]
        public string Hdk_Yb { get; set; }

        [DisplayName("勤務区分<br>&nbsp;")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string Knmkbn_KnmkbnJsk { get; set; }


        [DisplayName("出勤時刻<br>&nbsp;&nbsp;(打刻)")]
        [Required]
        [StringLength(5)]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string Sykjk_SykjkDkk { get; set; }
        public string Sykjk { get; set; }
        public string SykjkDkk { get; set; }

        [DisplayName("退勤時刻<br>&nbsp;&nbsp;(打刻)")]
        [Required]
        [StringLength(5)]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string Tkjk_TkjkDkk { get; set; }
        public string Tkjk { get; set; }
        public string TkjkDkk { get; set; }

        [DisplayName("休憩時間<br>&nbsp;")]
        [Required]
        [StringLength(5)]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string KykJkn { get; set; }


        [DisplayName("残業時間<br>&nbsp;")]
        [Required]
        [StringLength(5)]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string Zgyjkn { get; set; }


        [DisplayName("残業時間:36<br>&nbsp;")]
        [Required]
        [StringLength(5)]
        [HtmlTag("input", "text")]
        [SortableAttribute(false)]
        public string Zgyjkn36 { get; set; }


        [DisplayName("勤怠グラフ<br><table class='label-kntichart'><tr><td>5</td><td>6</td><td>7</td><td>8</td><td>9</td><td>10</td><td>11</td><td>12</td><td>13</td><td>14</td><td>15</td><td>16</td><td>17</td><td>18</td><td>19</td><td>20</td><td>21</td><td>22</td><td>23</td><td>24</td><td>1</td><td>2</td><td>3</td><td>4</td></tr></table>")]
        [HtmlTag("input", "bar")]
        [SortableAttribute(false)]
        public string KntiChart { get; set; }


        [DisplayName("備考<br>&nbsp;")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [SortableAttribute(false)]
        public string Biko { get; set; }


        [DisplayName("申請承認<br>&nbsp;")]
        [StringLength(2)]
        [HtmlTag("button")]
        [SortableAttribute(false)]
        [Event("search/apply","search", isVerify: true, paramItems: "shainId")]
        public string BtnApply { get; set; }
        
    }

}

