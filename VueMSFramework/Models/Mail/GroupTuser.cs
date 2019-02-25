using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{
    public class GroupTuser : BaseModel
    {
 
        [Key]
        public string GroupId { get; set; }
        [Key]
        public string TuserId { get; set; }


        [DisplayName("送信日時")]
        public DateTime? SentDate { get; set; }

        [DisplayName("送信済み")]
        public bool IsSent { get; set; } = false;

        [DisplayName("既読")]
        public bool IsRead { get; set; } = false;

        [DisplayName("送信エラー")]
        public bool IsError { get; set; } = false;

        [DisplayName("エラーメッセージ")]
        [StringLength(3000)]
        public string ErrorMessage { get; set; }



    }

}
