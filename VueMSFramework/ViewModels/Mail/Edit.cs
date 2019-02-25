using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Mail
{

    public class EditPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }

        [DisplayName("メール送信(確認)")]
        [HtmlTag("button")]
        [Event("edit/confirm","edit", isVerify: true)]
        [Redirect("confirm/load","confirm", paramItems: "mailId")]
        public string BtnConfirm { get; set; }

        [DisplayName("下書き保存")]
        [HtmlTag("button")]
        [Event("edit/draft","edit", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnDraft { get; set; }


    }

    public class Edit : ViewModel
    {
        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public EditPanel Panel { get; set; } 


        //システム項目
        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        public string MailId { get; set; }

        [DisplayName("IsDraft")]
        [HtmlTag("input", "hidden")]
        public bool IsDraft { get; set; } = true;

        //ユーザ項目

        [DisplayName("送信者")]
        [HtmlTag("input", "text")]
        [StringLength(30)]
        [Required]
        [Help("メールの送信者の日本語名です。30文字以内で記載ください。")]
        [InvalidFeedback("This field is required. Please register within 100 characters.")]
        public string Sender { get; set; } = "ながのとびっくランinわかほ 事務局";

        [DisplayName("送信者アドレス")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        [StringLength(100)]
        [Placeholder("enter mail address")]
        [Required]
        [Help("メールの送信者アドレスは100文字以内で入力ください。")]
        [InvalidFeedback("100文字以内で入力してください。")]
        public string FromEmail { get; set; } = "info@tobicrun.jp";

        [DisplayName("件名")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [Required]
        [Help("件名はメールのタイトルとなります。100文字以内で記載ください。")]
        [InvalidFeedback("100文字以内で入力してください。")]
        [Autofocus]
        public string Subject { get; set; }

        [DisplayName("本文")]
        [HtmlTag("input", "textarea")]
        [StringLength(2000)]
        [Required]
        [Help("本文はメールの内容となります。1000文字以内で記載ください。")]
        [InvalidFeedback("1000文字以内で入力してください。")]
        public string Body { get; set; }

        [DisplayName("署名")]
        [HtmlTag("input", "textarea")]
        [StringLength(150)]
        [Help("当メールの送信者情報を記載ください")]
        [InvalidFeedback("150文字以内で入力してください。")]
        public string Signature { get; set; } = "============\nながのとびっくランinわかほ\n事務局";

        [DisplayName("添付ファイル１")]
        [HtmlTag("input", "file")]
        [Help("10Mbyte以下のファイルをしてください。")]
        public File File1 { get; set; }

        [DisplayName("添付ファイル２")]
        [HtmlTag("input", "file")]
        [Help("10Mbyte以下のファイルをしてください。")]
        public File File2 { get; set; }

        [DisplayName("添付ファイル３")]
        [HtmlTag("input", "file")]
        [Help("10Mbyte以下のファイルをしてください。")]
        public File File3 { get; set; }



        [DisplayName("ユーザリスト")]
        [HtmlTag("tabs")]
        public Tabs Tabs { get; set; } = new Tabs();


   
    }
}
