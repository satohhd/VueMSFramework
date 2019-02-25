using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Exam
{
    [DisplayName("ｶﾃｺﾞﾘ")]
    [Description("説明")]
    public class CategoryCrudPanel
    {
        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("close","category")]
        public string BtnBack { get; set; }

        [DisplayName("保存")]
        [HtmlTag("button")]
        [Event("category/store","category", isVerify: true,isConfirm:true)]
        [Redirect("close", "category")]
        public string BtnUpdate { get; set; }

        [DisplayName("クリア")]
        [HtmlTag("button")]
        [Event("clear","category", isVerify: true)]
        public string BtnCreate { get; set; }

    }
    public class CategoryCrud : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public CategoryCrudPanel Panel { get; set; }


        [DisplayName("カテゴリID")]
        [HtmlTag("input", "hidden")]
        public string CategoryId { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        [ReadOnly(true)]
        public string ExamineeId { get; set; }

        [DisplayName("カテゴリ")]
        [StringLength(100)]
        [Required]
        [HtmlTag("input", "text")]
        [Grid(8)]
        public string CategoryName { get; set; }

        [DisplayName("表示順")]
        [Range(0, 1000)]
        [HtmlTag("input", "number")]
        [Required]
        [Grid(4)]
        public int? OrderBy { get; set; }


        [DisplayName("リスト")]
        [HtmlTag("table")]
        public List<CategoryList> CategoryList { get; set; }

    }
    public class CategoryList : ViewModel
    {

        [DisplayName("編集")]
        [HtmlTag("button")]
        [Event("category/edit","category")]
        public string BtnEdit { get; set; }

        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string CategoryId { get; set; }

        [DisplayName("カテゴリ")]
        [StringLength(100)]
        [Required]
        [HtmlTag("input", "text")]
        [Grid(8)]
        public string CategoryName { get; set; }

        [DisplayName("受験者")]
        [HtmlTag("input", "text")]
        public string ExamineeId { get; set; }


        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        [Required]
        [Range(0, 1000)]
        [Grid(4)]
        public int? OrderBy { get; set; }


        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("category/delete","category")]
        public string BtnRemove { get; set; }


    }
}
