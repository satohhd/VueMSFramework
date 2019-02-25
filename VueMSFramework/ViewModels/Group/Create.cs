using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Group
{

    [DisplayName("作成")]
    [Description("新規作成します")]
    public class Create : ViewModel
    {

        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public CreatePanel Panel { get; set; } 

        //システム項目
        [DisplayName("グループID")]
        [HtmlTag("input", "hidden")]
        public string GroupId { get; set; }

        //ユーザ項目
        [DisplayName("グループ名")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string GroupName { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        [Range(0, 99999)]
        [Help("0～8000の範囲で入力してください。")]
        public int OrderBy { get; set; }

        [DisplayName("表示色")]
        [HtmlTag("input", "color")]
        [StringLength(10)]
        public string Color { get; set; } = "#80ffff";

        [DisplayName("アイコン")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string IconUrl { get; set; }


        [DisplayName("ユーザリスト")]
        [HtmlTag("table")]
        public List<GroupUserList> SelectList { get; set; }

    }
    public class CreatePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","create")]
        public string BtnBack { get; set; }

        [DisplayName("登録")]
        [HtmlTag("button")]
        [Event("create/insert","create", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnInsert { get; set; }


    }
    public class GroupUserList : ViewModel
    {

        [DisplayName("選択")]
        [HtmlTag("input", "checkbox")]
        public bool IsSelect { get; set; } = false;

        //システム項目
        [DisplayName("GroupId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string GroupId { get; set; }

        [DisplayName("TuserId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string TuserId { get; set; }

        //ユーザ項目
        [DisplayName("ユーザ名")]
        [HtmlTag("input", "text")]
        [Required]
        [StringLength(100)]
        public string TuserName { get; set; }

        [DisplayName("よみがな")]
        [HtmlTag("input", "text")]
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

        [DisplayName("送信メールアドレス2")]
        [HtmlTag("input", "email")]
        [StringLength(100)]
        public string ToEmail2 { get; set; }

        [DisplayName("送信メールアドレス3")]
        [HtmlTag("input", "email")]
        [StringLength(100)]
        public string ToEmail3 { get; set; }



        [DisplayName("HTMLメール")]
        [HtmlTag("input", "radio")]
        public bool? IsHtmlMail { get; set; }

        [DisplayName("電話")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Tel { get; set; }

        [DisplayName("送信日時")]
        [HtmlTag("input", "text")]
        public DateTime? SentDate { get; set; }

        [DisplayName("状態")]
        [HtmlTag("input", "text")]
        public string Status { get; set; }

        public bool IsError { get; set; } = false;
        public bool IsSent { get; set; } = false;
        public string ErrorMessage { get; set; }

    }
}

