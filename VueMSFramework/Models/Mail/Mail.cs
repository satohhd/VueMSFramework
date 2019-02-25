using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{

    public class Mail :BaseModel
    {

        [Key]
        public string MailId { get; set; }

        [DisplayName("送信者")]
        [StringLength(30)]
        [Required]
        public string Sender { get; set; }


        [DisplayName("送信者アドレス")]
        [StringLength(100)]
        [Required]
        public string FromEmail { get; set; }

        [DisplayName("件名")]
        [StringLength(100)]
        [Required]
        public string Subject { get; set; }

        [DisplayName("本文")]
        [StringLength(2000)]
        [Required]
        public string Body { get; set; }

        [DisplayName("署名")]
        [StringLength(150)]
        public string Signature { get; set; }

        [DisplayName("添付ファイル１")]
        public String File1 { get; set; }

        [DisplayName("添付ファイル２")]
        public String File2 { get; set; }

        [DisplayName("添付ファイル３")]
        public String File3 { get; set; }

        [DisplayName("送信済み")]
        public bool IsSent { get; set; } = false;

        [DisplayName("送信日時")]
        public DateTime? SentDate { get; set; }

        [DisplayName("下書き")]
        public bool IsDraft { get; set; } = true;

        [DisplayName("送信済み件数")]
        public int SentCount { get; set; } = 0;

        [DisplayName("エラー件数")]
        public int ErrorCount { get; set; } = 0;

        [DisplayName("既読数")]
        public int ReadCount { get; set; } = 0;

        [DisplayName("グループID")]
        public int? GroupId { get; set; }



    }
}
