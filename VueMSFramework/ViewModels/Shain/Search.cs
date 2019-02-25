using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.ViewModels.Shain
{

    public class SearchPanel
    {

        [DisplayName("新規登録")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search", isVerify: true)]
        public string BtnSearch { get; set; }


    }
    public class  Search : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public SearchPanel Panel { get; set; } 

        [DisplayName("絞込条件：名称の部分一致")]
        [HtmlTag("input", "text")]
        [Autofocus]
        [Grid(4)]
        public string Keywords { get; set; }

        [DisplayName("結果リスト")]
        [HtmlTag("table")]
        public List<SearchList> SearchList { get; set; }
    }
    public class SearchList : ViewModel
    {

        [DisplayName("編集")]
        [SortableAttribute(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit", isVerify: true, paramItems: "shainId")]
        public string BtnEdit { get; set; }

        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        public string ShainId { get; set; }

        //ユーザ項目
        [DisplayName("よみ順")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string YmgnIchimoji { get; set; }


        [DisplayName("氏名")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Shmi { get; set; }

        [DisplayName("社員番号")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Shinbng { get; set; }


        [DisplayName("部門名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Bmnmi { get; set; }

        [DisplayName("拠点名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        public string Kytnmi { get; set; }


    }


}

