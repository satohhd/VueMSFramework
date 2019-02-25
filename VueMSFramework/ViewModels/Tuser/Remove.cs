using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Tuser
{


    public class RemovePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }


        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnRemove { get; set; }

    }

    [DisplayName("削除")]
    [Description("削除します")]
    public class Remove : ViewModel
    {

        [DisplayName("ボタンパネル")]
        [HtmlTag("panel")]
        public RemovePanel Panel { get; set; }


        //システム項目
        [DisplayName("ユーザID")]
        [HtmlTag("input", "hidden")]
        public string TuserId { get; set; }


        //ユーザ項目

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string TuserName { get; set; }

        [DisplayName("よみがな")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string TuserKana { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        [ReadOnly(true)]
        public int OrderBy { get; set; }

        [DisplayName("送信メールアドレス")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        public string ToEmail { get; set; }

        [DisplayName("HTMLメール")]
        [HtmlTag("input", "checkbox")]
        [ReadOnly(true)]
        public bool IsHtmlMail { get; set; }

        [DisplayName("事務局")]
        [HtmlTag("input", "checkbox")]
        [ReadOnly(true)]
        public bool IsSecretariat { get; set; }

        [DisplayName("サポーター")]
        [HtmlTag("input", "checkbox")]
        [ReadOnly(true)]
        public bool IsSupporter { get; set; } 


        [DisplayName("所属団体")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Affiliation { get; set; }

        [DisplayName("大会役員")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Officer { get; set; }

        [DisplayName("大会実行委員会")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Department { get; set; }

        [DisplayName("肩書き")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Degree { get; set; }


        [DisplayName("電話")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Tel { get; set; }

        [DisplayName("郵便番号")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string PostCode { get; set; }

        [DisplayName("住所")]
        [HtmlTag("input", "textarea")]
        [ReadOnly(true)]
        public string Address { get; set; }


    }

}

