using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Mail
{
    public class CreatePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("search/load","search")]
        public string BtnBack { get; set; }

        [DisplayName("メール送信確認")]
        [HtmlTag("button")]
        [Event("create/confirm","create",isVerify: true, isConfirm: false)]
        [Redirect("confirm/load","confirm",paramItems:"mailId")]
        public string BtnConfirm { get; set; }

        [DisplayName("下書き保存")]
        [HtmlTag("button")]
        [Event("create/draft","create", isVerify: true, isConfirm: false)]
        [Redirect("search/load","search")]
        public string BtnDraft { get; set; }


    }

    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {

        public Create()
        {
            Signature = "このメールは配信専用アドレスから自動で送信しております。\n";
            Signature += "本メールへの返信はできません。\n";

            Signature = "* *****************\n"
                        + "ながのとびっくラン \n"
                        + "inわかほ 大会事務局 \n"
                        + "HP //tobicrun.jp \n"
                        + "//tobicrun.jp/m2 \n"
                        + "****************** *";

        }

        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public CreatePanel CreatePanel { get; set; } 


        //システム項目
        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        public string MailId { get; set; }

        [DisplayName("IsDraft")]
        [HtmlTag("input", "hidden")]
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


        [DisplayName("ユーザリスト")]
        [HtmlTag("tabs")]
        public Tabs Tabs { get; set; } = new Tabs();
  
  
    }
    public class Tabs
    {
        [DisplayName("アクティブタブ")]
        [HtmlTag("input", "hidden")]
        public string ActiveTabName { get; set; } = "tab1";


        [DisplayName("送信先リスト")]
        [HtmlTag("tab")]
        public Tab1 Tab1 { get; set; } = new Tab1();

    }

    public class Tab1
    {

        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public SelectPanel SelectPanel { get; set; } = new SelectPanel();

        [DisplayName("ユーザリスト")]
        [HtmlTag("table")]
        public List<UserSelectList> SelectList { get; set; }


    }
    public class SelectPanel
    {


        [DisplayName("全選択/解除")]
        [HtmlTag("button")]
        [Event("allselect","create")]
        public string BtnAllSelect { get; set; }


        [DisplayName("グループ選択")]
        [HtmlTag("buttons")]
        [Options("Groups", "GroupName", "GroupId")]
        [Event("select","create")]
        public string BtnGroup { get; set; }

        [DisplayName("グループ登録")]
        [HtmlTag("button")]
        [Event("groupadd","create")]
        public string BtnGroupAdd { get; set; }

    }
    public class UserSelectList
    {

        [DisplayName("選択")]
        [HtmlTag("input", "checkbox")]
        public bool? IsSelect { get; set; } = false;


        //ユーザ項目
        [DisplayName("ユーザID")]
        [HtmlTag("input", "hidden")]
        public string TuserId { get; set; }


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
        [Required]
        [StringLength(100)]
        public string TuserName { get; set; }

        [DisplayName("よみがな")]
        [HtmlTag("input", "hidden")]
        [StringLength(100)]
        public string TuserKana { get; set; }

        [DisplayName("所属団体")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Affiliation { get; set; }


        [DisplayName("送信メールアドレス")]
        [HtmlTag("input", "email")]
        [Required]
        [StringLength(100)]
        public string ToEmail { get; set; }

        [DisplayName("送信メールアドレス")]
        public string ToEmail2 { get; set; }
        [DisplayName("送信メールアドレス")]
        public string ToEmail3 { get; set; }
        public bool? IsHtmlMail { get; set; }

        [DisplayName("電話")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Tel { get; set; }

        [DisplayName("送信日時")]
        [HtmlTag("input", "hidden")]
        public string SentDate { get; set; }

        [DisplayName("状態")]
        [HtmlTag("input", "hidden")]
        public string Status { get; set; }

        [DisplayName("エラー有無")]
        [HtmlTag("input", "hidden")]
        public string IsError { get; set; }

        [DisplayName("エラー")]
        [HtmlTag("input", "hidden")]
        public string ErrorMessage { get; set; }

        [DisplayName("送信済み")]
        [HtmlTag("input", "hidden")]
        public string IsSent { get; set; }

    }
}

