using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Mail
{

    public class ConfirmPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","confirm")]
        public string BtnBack { get; set; }

        [DisplayName("メール送信")]
        [HtmlTag("button")]
        [Event("confirm/send","confirm", isVerify: true,isConfirm:true,confirmMessage:"メールを送信します。よろしいですか？")]
        [Redirect("search/load","search")]
        public string BtnSend { get; set; }


    }
    public class Confirm : ViewModel
    {
        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public ConfirmPanel Panel { get; set; } 


        //システム項目
        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string MailId { get; set; }


        //ユーザ項目
        [DisplayName("送信者")]
        [HtmlTag("input", "text")]
        [StringLength(30)]
        [ReadOnly(true)]
        public string Sender { get; set; }

        [DisplayName("送信者アドレス")]
        [HtmlTag("input", "email")]
        [StringLength(100)]
        [Placeholder("enter mail address")]
        [ReadOnly(true)]
        public string FromEmail { get; set; }

        [DisplayName("件名")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [ReadOnly(true)]
        public string Subject { get; set; }

        public string Body { get; set; }

        [DisplayName("メール本文")]
        [HtmlTag("input", "textarea")]
        [StringLength(2000)]
        [ReadOnly(true)]
        public string TerminalBody { get; set; }

        [DisplayName("リンク先WEBページ本文")]
        [HtmlTag("input", "hidden")]
        [StringLength(2000)]
        [ReadOnly(true)]
        public string WebPageBody { get; set; }

        [DisplayName("署名")]
        [HtmlTag("input", "textarea")]
        [StringLength(150)]
        [ReadOnly(true)]
        public string Signature { get; set; }


        [DisplayName("添付ファイル１")]
        [HtmlTag("input", "file")]
        [ReadOnly(true)]
        public File File1 { get; set; }

        [DisplayName("添付ファイル２")]
        [HtmlTag("input", "file")]
        [ReadOnly(true)]
        public File File2 { get; set; }

        [DisplayName("添付ファイル３")]
        [HtmlTag("input", "file")]
        [ReadOnly(true)]
        public File File3 { get; set; }

        [DisplayName("送信先リスト")]
        [HtmlTag("table")]
        public List<ConfirmUserList> SelectedList { get; set; }


    }

    public class ConfirmUserList : ViewModel
    {

        [DisplayName("選択")]
        [HtmlTag("input", "checkbox")]
        public bool IsSelect { get; set; } = false;



        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        public string MailId { get; set; }

        [DisplayName("TuserId")]
        [HtmlTag("input", "hidden")]
        public string TuserId { get; set; }

        //ユーザ項目
        [DisplayName("読み")]
        [HtmlTag("input", "text")]
        public string AiueoOrder
        {
            get
            {
                return TuserKana.Substring(0, 1);
            }
        }

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        public string TuserName { get; set; }

        [DisplayName("よみがな")]
        [HtmlTag("input", "hidden")]
        public string TuserKana { get; set; }

        [DisplayName("所属団体")]
        [HtmlTag("input", "hidden")]
        public string Affiliation { get; set; }


        [DisplayName("送信メールアドレス")]
        [HtmlTag("input", "email")]
        public string ToEmail { get; set; }
        [DisplayName("送信メールアドレス")]
        public string ToEmail2 { get; set; }
        [DisplayName("送信メールアドレス")]
        public string ToEmail3 { get; set; }

        public bool? IsHtmlMail { get; set; }
        public string Tel { get; set; }
        public string SentDate { get; set; }
        public string Status { get; set; }
        public string IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string IsSent { get; set; }

    }
}

