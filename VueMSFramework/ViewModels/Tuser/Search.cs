using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Tuser
{
    public class Search : ViewModel
    {
    

        [DisplayName("ユーザ新規登録")]
        [HtmlTag("button")]
        [Event("create/load","create", isVerify: true)]
        public string BtnCreate { get; set; }


        [DisplayName("絞込条件")]
        [HtmlTag("h", "2")]
        public string Condition { get; set; }

        [DisplayName("氏名、よみがな、メール　いずれかの部分一致")]
        [HtmlTag("input", "text")]
        [Autofocus]
        public string Keywords { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search", isVerify: true)]
        public string BtnSearch { get; set; }
        
  
        [DisplayName("検索結果")]
        [HtmlTag("h","2")]
        public string Condition2 { get; set; }

        [DisplayName("リスト")]
        [HtmlTag("table")]
        public List<TuserList> TuserList { get; set; }

    }

    public class TuserList
    {

        [DisplayName("編集")]
        [SortableAttribute(false)]
        [Event("edit/load","edit", paramItems: "tuserId")]
        public string BtnEdit { get; set; }


        //システム項目
        [DisplayName("ユーザID")]
        [HtmlTag("input", "hidden")]
        public string TuserId { get; set; }

        [DisplayName("氏名")]
        [HtmlTag("input", "text")]
        public string TuserName { get; set; }

        [DisplayName("送信メールアドレス")]
        [HtmlTag("input", "email")]
        [Required]
        [StringLength(100)]
        public string ToEmail { get; set; }

        [DisplayName("送信メールアドレス")]
        public string ToEmail2 { get; set; }

        [DisplayName("送信メールアドレス")]
        public string ToEmail3 { get; set; }


        [DisplayName("電話")]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Tel { get; set; }

        [DisplayName("住所")]
        [HtmlTag("input", "hidden")]
        [StringLength(1000)]
        public string Address { get; set; }

        public bool? IsSecretariat { get; set; }
        [DisplayName("事務局")]
        [HtmlTag("input", "radio")]
        public string IsSecretariatP
        {

            get
            {
                return IsSecretariat == true ? "◎" : "";
            }
        }


        [DisplayName("削除")]
        [SortableAttribute(false)]
        [Event("remove/load","remove", paramItems: "tuserId")]
        public string BtnRemove { get; set; }

    }

}

