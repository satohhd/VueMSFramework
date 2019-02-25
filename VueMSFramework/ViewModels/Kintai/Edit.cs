using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Kintai
{

    public class EditPanel
    {
        [DisplayName("保存")]
        [HtmlTag("button")]
        [Event("edit/store","edit", isVerify: true,isConfirm:true)]
        [Redirect("search/load","search",paramItems:"shainId")]
        public string BtnStore { get; set; }

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }

    }

    [DisplayName("編集")]
    [Description("説明")]
    public class Edit : ViewModel
    {
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public EditPanel Panel { get; set; }

        [DisplayName("勤怠ID")]
        [HtmlTag("input", "hidden")]
        public string KintaiId { get; set; }

        //社員情報
        [DisplayName("社員ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        [Grid(4)]
        public string ShainId { get; set; }

        [DisplayName("社員番号")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(4)]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Grid(4)]
        public string Shmi { get; set; }


        //ユーザ項目
        [DisplayName("日付")]
        [Required]
        [ReadOnly(true)]
        [HtmlTag("input", "date")]
        [Grid(4)]
        public string Hdk { get; set; }

        [DisplayName("勤務区分")]
        [HtmlTag("input", "radio")]
        [Options("Knmkbn")]
        [Grid(4)]
        public string Knmkbn { get; set; }


        [DisplayName("申請承認")]
        [StringLength(2)]
        [HtmlTag("button")]
        [Grid(2,2)]
        [Event("edit/apply","edit", isVerify: true)]
        public string BtnApply { get; set; }


        [DisplayName("出勤時刻")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1]*[0-9]|[2][0-8]):[0-5]*[0-9]$")]
        [Placeholder("9:00")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string Sykjk { get; set; }

        [DisplayName("退勤時刻")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1]*[0-9]|[2][0-8]):[0-5]*[0-9]$")]
        [Placeholder("17:30")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string Tkjk { get; set; }

        [DisplayName("休憩時間")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1]*[0-9]|[2][0-8]):[0-5]*[0-9]$")]
        [Placeholder("1:00")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string KykJkn { get; set; }


        [DisplayName("残業時間")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1]*[0-9]|[2][0-8]):[0-5]*[0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string Zgyjkn { get; set; }


        [DisplayName("残業時間:36")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1]*[0-9]|[2][0-8]):[0-5]*[0-9]$")]
        [HtmlTag("input", "text")]
        [Grid(2, 0, 2)]
        public string Zgyjkn36 { get; set; }


        [DisplayName("出勤時刻(打刻)")]
        [StringLength(5)]
        [HtmlTag("input", "text")]
        [Grid(2)]
        public string SykjkDkk { get; set; }

        [DisplayName("退勤時刻(打刻)")]
        [StringLength(5)]
        [RegularExpression(@"^([0-1]*[0-9]|[2][0-8]):[0-5]*[0-9]$")]
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
