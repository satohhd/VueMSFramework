using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models.Kintai
{
    //【勤怠 Kintai】
    public class Kintai :BaseModel
    {

        [Key]
        public string KintaiId { get; set; }

        //ユーザ項目
        [DisplayName("社員番号")]
        [Required]
        public string ShainId { get; set; }


        [DisplayName("日付")]
        [Required]
        public DateTime Hdk { get; set; }

        [DisplayName("勤務区分")]
        [StringLength(100)]
        public string Knmkbn { get; set; }

        //[DisplayName("勤務区分(実績)")]
        //[Required]
        //[StringLength(100)]
        //public string KnmkbnJsk {

        //    get{

        //        if (SykjkDkk != null)
        //        {
        //            return "出勤";

        //        }
        //        else
        //        {
        //            return "休日";

        //        }
        //    }
        //}

        [DisplayName("出勤時刻")]
        [StringLength(5)]
        public string Sykjk { get; set; }

        [DisplayName("退勤時刻")]
        [StringLength(5)]
        public string Tkjk { get; set; }

        [DisplayName("出勤時刻(打刻)")]
        [StringLength(5)]
        public string SykjkDkk { get; set; }

        [DisplayName("退勤時刻(打刻)")]
        [StringLength(5)]
        public string TkjkDkk { get; set; }


        [DisplayName("休憩時間")]
        [StringLength(5)]
        public string KykJkn { get; set; }


        [DisplayName("残業時間")]
        [StringLength(5)]
        public string Zgyjkn { get; set; }


        [DisplayName("残業時間:36")]
        [StringLength(5)]
        public string Zgyjkn36 { get; set; }


        [DisplayName("備考")]
        [StringLength(1000)]
        public string Biko { get; set; }


        [DisplayName("申請承認")]
        [StringLength(2)]
        public string Snsisynn { get; set; }

    }
}
