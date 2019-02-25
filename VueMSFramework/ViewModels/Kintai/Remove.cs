using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Kintai
{

    public class RemovePanel
    {
        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove", isVerify: true,isConfirm:true)]
        [Grid(6, 0, 4)]
        public string BtnRemove { get; set; }

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        [Grid(2)]
        public string BtnBack { get; set; }

    }
    public class Remove : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        [Grid(8)]
        public RemovePanel Panel { get; set; }

        [DisplayName("勤怠ID")]
        [HtmlTag("input", "hidden")]
        public string KintaiId { get; set; }

        //社員情報
        [DisplayName("社員ID")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(4)]
        public string ShainId { get; set; }

        [DisplayName("社員番号")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(2)]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(2)]
        public string Shmi { get; set; }

        //ユーザ項目
        [DisplayName("日付")]
        [Required]
        [ReadOnly(true)]
        [HtmlTag("input", "date")]
        [Grid(2)]
        public string Hdk { get; set; }

        [DisplayName("勤務区分")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string Knmkbn { get; set; }

        [DisplayName("出勤時刻")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string Sykjk { get; set; }

        [DisplayName("退勤時刻")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string Tkjk { get; set; }


        [DisplayName("休憩時間")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string KykJkn { get; set; }


        [DisplayName("残業時間")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        public string Zgyjkn { get; set; }


        [DisplayName("残業時間:36")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2, 0,2)]
        public string Zgyjkn36 { get; set; }


        [DisplayName("出勤時刻(打刻)")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string SykjkDkk { get; set; }

        [DisplayName("退勤時刻(打刻)")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2, 0, 8)]
        public string TkjkDkk { get; set; }

        [DisplayName("備考")]
        [StringLength(1000)]
        [HtmlTag("input", "textarea")]
        [Grid(10)]
        public string Biko { get; set; }


    }

}

