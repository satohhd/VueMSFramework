using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Option
{

    public class SearchPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","search")]
        public string BtnBack { get; set; }

        [DisplayName("グループ登録")]
        [HtmlTag("button")]
        [Event("create/load","create")]
        public string BtnCreate { get; set; }

        [DisplayName("絞込")]
        [HtmlTag("button")]
        [Event("search/load","search", isVerify: true)]
        public string BtnSearch { get; set; }

    }

    public class Search : ViewModel
    {


        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public SearchPanel Panel { get; set; }

        [DisplayName("絞込条件(項目部分一致)")]
        [HtmlTag("input", "text")]
        [Autofocus]
        public string Keywords { get; set; }


        [DisplayName("リスト")]
        [HtmlTag("table")]
        public List<OptionList> OptionList { get; set; }
    }

    public class OptionList : ViewModel
    {

        [DisplayName("編集")]
        [Sortable(false)]
        [HtmlTag("button")]
        [Event("edit/load","edit", paramItems: "optionId")]
        public string BtnEdit { get; set; }

        //システム項目
        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        public string OptionId { get; set; }

        //ユーザ項目
        [DisplayName("項目")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string Field { get; set; }

        [DisplayName("表示名称")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Text { get; set; }

        [DisplayName("値")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string Value { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        [Range(0, 8000)]
        [Help("0～8000の範囲で入力してください。")]
        public int OrderBy { get; set; } = 0;

        [DisplayName("表示色")]
        [HtmlTag("input", "color")]
        [StringLength(10)]
        public string Color { get; set; }

        [DisplayName("アイコン")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string IconUrl { get; set; }



    }

}

