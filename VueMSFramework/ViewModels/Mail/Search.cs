using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Mail
{
    public class Search : ViewModel
    {

        [DisplayName("新しくメールを作成する")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        [Grid(6)]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search", isVerify: true)]
        [Grid(1)]
        public string BtnSearch { get; set; }

        [DisplayName("絞込条件(件名部分一致)")]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(5)]
        public string Keywords { get; set; }



        [DisplayName("送信履歴")]
        [HtmlTag("tabs")]
        public MailSearchTabs Tabs { get; set; } = new MailSearchTabs();

    }
    public class MailSearchTabs
    {
        [DisplayName("アクティブタブ")]
        [HtmlTag("input", "hidden")]
        public string ActiveTabName { get; set; } = "tab1";


        [DisplayName("送信済みリスト")]
        [HtmlTag("tab")]
        public MailSearchTab1 Tab1 { get; set; } = new MailSearchTab1();

        [DisplayName("下書きリスト")]
        [HtmlTag("tab")]
        public MailSearchTab2 Tab2 { get; set; } = new MailSearchTab2();

    }
    public class MailSearchTab1
    {

        [DisplayName("送信済みリスト")]
        [HtmlTag("table")]
        public List<SentList> SentList { get; set; }

        [DisplayName("Page")]
        [HtmlTag("pagination")]
        [Event("search/load","search")]
        public Pagination SentListPager { get; set; } = new Pagination();

    }
    public class MailSearchTab2
    {
        [DisplayName("下書きリスト")]
        [HtmlTag("table")]
        public List<DraftList> DraftList { get; set; }

        [DisplayName("Page")]
        [HtmlTag("pagination")]
        [Event("search/load","search")]
        public Pagination DraftListPager { get; set; } = new Pagination();
    }

    public class SentList
    {
        [DisplayName("参照")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("refer/load","refer", isVerify: true, paramItems: "mailId")]
        public string BtnRefer { get; set; }

        //ユーザ項目
        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        public string MailId { get; set; }

        [DisplayName("件名")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        [Required]
        [Help("This is IDm. It is an important ID for identifying the card.")]
        [InvalidFeedback("This field is required. Please register within 100 characters.")]
        public string Subject { get; set; }

        [DisplayName("送信日時")]
        [HtmlTag("input", "datetime")]
        public DateTime? SentDate { get; set; }

        [DisplayName("状態")]
        [HtmlTag("input", "text")]
        public string Status { get; set; }

        [DisplayName("送信者数")]
        [HtmlTag("input", "number")]
        public int? SentCount { get; set; }

        [DisplayName("既読者数")]
        [HtmlTag("input", "number")]
        public int? ReadCount { get; set; }

        [DisplayName("エラー件数")]
        [HtmlTag("input", "number")]
        public int? ErrorCount { get; set; }

        [DisplayName("削除")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("remove/load","remove", paramItems: "mailId")]
        public string BtnRemove { get; set; }

    }


    public class DraftList
    {
        [DisplayName("作成")]
        [Sortable(false)]
        [Event("edit/load","edit",paramItems:"mailId")]
        public string BtnEdit { get; set; }

        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        public string MailId { get; set; }


        [DisplayName("件名")]
        public string Subject { get; set; }

        [DisplayName("作成日時")]
        public DateTime? Created { get; set; }

        [DisplayName("作成者")]
        public string Owner { get; set; }

        [DisplayName("状態")]
        public string Status { get; set; }

        [DisplayName("削除")]
        [Sortable(false)]
        [Event("remove/load","remove", paramItems: "mailId")]
        public string BtnRemove { get; set; }

    }
    public class UserList
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
        public string TuserKana { get; set; }

        [DisplayName("所属団体")]
        [HtmlTag("input", "hidden")]
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
        [HtmlTag("input", "text")]
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

