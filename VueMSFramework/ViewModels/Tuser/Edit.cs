using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Tuser
{

    public class EditPanel
    {
        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }

        [DisplayName("更新")]
        [HtmlTag("button")]
        [Event("edit/update","edit", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnUpdate { get; set; }


    }
    public class Edit : ViewModel
    {
        [DisplayName("ボタンパネル")]
        [HtmlTag("panel")]
        public EditPanel Panel { get; set; }



        //システム項目
        [DisplayName("ユーザID")]
        [HtmlTag("input", "hidden")]
        public string TuserId { get; set; }

        //ユーザ項目
        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        [Required]
        [StringLength(100)]
        [Autofocus]
        public string TuserName { get; set; }

        [DisplayName("よみがな")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string TuserKana { get; set; }


        [DisplayName("パスワード")]
        [HtmlTag("input", "password")]
        [Required]
        [StringLength(100)]
        public string Passsowrd { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "text")]
        [Help("100文字以内の英数字の文字列を入力してください。リスト表示順となります。")]
        [Required]
        [Range(0, 100)]
        public int OrderBy { get; set; }

        [DisplayName("送信メールアドレス")]
        [HtmlTag("input", "email")]
        [Required]
        [StringLength(100)]
        public string ToEmail { get; set; }

        [DisplayName("送信メールアドレス2")]
        [HtmlTag("input", "email")]
        [StringLength(100)]
        public string ToEmail2 { get; set; }

        [DisplayName("送信メールアドレス3")]
        [HtmlTag("input", "email")]
        [StringLength(100)]
        public string ToEmail3 { get; set; }

        [DisplayName("HTMLメール")]
        [HtmlTag("input", "checkbox")]
        public bool IsHtmlMail { get; set; } = false;

        [DisplayName("事務局")]
        [HtmlTag("input", "checkbox")]
        public bool IsSecretariat { get; set; }

        [DisplayName("サポーター")]
        [HtmlTag("input", "checkbox")]
        public bool IsSupporter { get; set; }


        [DisplayName("所属団体")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Affiliation { get; set; }

        [DisplayName("大会役員")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Officer { get; set; }

        [DisplayName("大会実行委員会")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Department { get; set; }

        [DisplayName("肩書き")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Degree { get; set; }


        [DisplayName("電話")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Tel { get; set; }

        [DisplayName("郵便番号")]
        [HtmlTag("input", "text")]
        [StringLength(8)]
        public string PostCode { get; set; }

        [DisplayName("住所")]
        [HtmlTag("input", "textarea")]
        [StringLength(500)]
        public string Address { get; set; }


    }



}
