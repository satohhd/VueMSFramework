using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Auth
{

   
    public class MailCreate : ViewModel
    {

        public MailCreate()
        {
            Signature = "このメールは配信専用アドレスから自動で送信しております。\n";
            Signature += "本メールへの返信はできません。\n";

            Signature =   "* *****************\n"
                        + "ながのとびっくラン \n"
                        + "inわかほ 大会事務局 \n"
                        + "HP //tobicrun.jp \n"
                        + "//tobicrun.jp/m2 \n"
                        + "****************** *";

        }


        //システム項目
        public string MailId { get; set; }

        public bool IsDraft { get; set; } = true;

        //ユーザ項目

        [DisplayName("送信者")]
        [HtmlTag("input", "text", "kana-name")]
        [StringLength(30)]
        [Required]
        [Help("メールの送信者の日本語名です。30文字以内で記載ください。")]
        [InvalidFeedback("This field is required. Please register within 30 characters.")]
        public string Sender { get; set; } = "ながのとびっくランinわかほ 大会事務局";

        [DisplayName("送信者アドレス")]
        [HtmlTag("input", "email", "email")]
        [StringLength(100)]
        [Placeholder("enter mail address")]
        [Required]
        [ReadOnly(true)]
        [Help("メールの送信者アドレスは100文字以内で記載ください。")]
        [InvalidFeedback("100文字以内で入力してください。")]
        public string FromEmail { get; set; } = "info@tobicrun.jp";

        [DisplayName("件名")]
        [HtmlTag("input", "text", "kana")]
        [StringLength(100)]
        [Required]
        [Help("件名はメールのタイトルとなります。100文字以内で記載ください。")]
        [InvalidFeedback("100文字以内で入力してください。")]
        [Autofocus]
        public string Subject { get; set; }

        [DisplayName("本文")]
        [HtmlTag("input", "textarea", "kana")]
        [StringLength(2000)]
        [Required]
        [Help("本文はメールの内容となります。1000文字以内で記載ください。")]
        [InvalidFeedback("1000文字以内で入力してください。")]
        public string Body { get; set; }

        [DisplayName("署名")]
        [HtmlTag("input", "textarea", "kana")]
        [StringLength(150)]
        [Help("当メールの送信者情報を記載ください")]
        [InvalidFeedback("150文字以内で入力してください。")]
        public string Signature { get; set; }

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
        public string ToEmail { get; internal set; }
    }
}

