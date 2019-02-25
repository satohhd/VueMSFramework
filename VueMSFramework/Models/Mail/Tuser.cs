using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{

    //【TUSERS ユーザマスタ】
    //ID
    //USER_CD
    //USER_NAME
    public class Tuser : BaseModel
    {
        [Key]
        [DisplayName("ユーザID")]
        [Required]
        public string TuserId { get; set; }

        //ユーザ項目


        [DisplayName("ユーザ名")]
        [Required]
        [StringLength(100)]
        public string TuserName { get; set; }

        [DisplayName("よみがな")]
        [StringLength(100)]
        public string TuserKana { get; set; }

        [DisplayName("パスワード")]
        [Required]
        [StringLength(100)]
        public string Passsowrd { get; set; }

        [DisplayName("表示順")]
        [Required]
        [Range(0,99999)]
        public int OrderBy { get; set; }

        [DisplayName("送信メールアドレス1")]
        [Required]
        [StringLength(100)]
        public string ToEmail { get; set; }

        [DisplayName("送信メールアドレス2")]
        [StringLength(100)]
        public string ToEmail2 { get; set; }

        [DisplayName("送信メールアドレス3")]
        [StringLength(100)]
        public string ToEmail3 { get; set; }

        [DisplayName("HTMLメール")]
        public bool? IsHtmlMail { get; set; }

        [DisplayName("事務局")]
        public bool? IsSecretariat { get; set; } = false;

        [DisplayName("サポーター")]
        public bool? IsSupporter { get; set; } = false;

        [DisplayName("所属団体")]
        [StringLength(100)]
        public string Affiliation { get; set; }

        [DisplayName("大会役員")]
        [StringLength(100)]
        public string Officer { get; set; }

        [DisplayName("大会実行委員会")]
        [StringLength(100)]
        public string Department { get; set; }

        [DisplayName("肩書き")]
        [StringLength(100)]
        public string Degree { get; set; }

        [DisplayName("電話")]
        [StringLength(100)]
        public string Tel { get; set; }

        [DisplayName("郵便番号")]
        [StringLength(8)]
        public string PostCode { get; set; }

        [DisplayName("住所")]
        [StringLength(100)]
        public string Address { get; set; }


        //public virtual ICollection<GroupTuser> GroupTusers { get; } = new List<GroupTuser>();

    }
}
