using System.Collections.Generic;
using System.ComponentModel;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Mail
{

    public class RemovePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }


        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove")]
        [Redirect("search/load","search")]
        public string BtnDelete { get; set; }

    }
    [DisplayName("削除")]
    [Description("削除します")]
    public class Remove : ViewModel
    {
        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public RemovePanel Panel { get; set; } 


        //システム項目
        [DisplayName("MailId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string MailId { get; set; }

        //ユーザ項目

        [DisplayName("送信者")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string Sender { get; set; }

        [DisplayName("送信者アドレス")]
        [HtmlTag("input", "email")]
        [ReadOnly(true)]
        public string FromEmail { get; set; }

        [DisplayName("件名")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        [Autofocus]
        public string Subject { get; set; }

        [DisplayName("本文")]
        [HtmlTag("input", "textarea")]
        [ReadOnly(true)]
        public string Body { get; set; }

        [DisplayName("署名")]
        [HtmlTag("input", "textarea")]
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


        [DisplayName("選択済みリスト")]
        [HtmlTag("table")]
        [ReadOnly(true)]
        public List<UserList> SelectList { get; set; }


    }

}

