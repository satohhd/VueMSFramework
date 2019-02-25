using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Group
{

    public class Edit : ViewModel
    {


        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public EditPanel Panel { get; set; }

        //システム項目
        [DisplayName("GroupId")]
        [HtmlTag("input", "hidden")]
        [ReadOnly(true)]
        public string GroupId { get; set; }

        //ユーザ項目
        [DisplayName("グループ名")]
        [Required]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string GroupName { get; set; }

        [DisplayName("表示順")]
        [HtmlTag("input", "number")]
        [Range(0,99999)]
        public int OrderBy { get; set; }

        [DisplayName("表示色")]
        [HtmlTag("input", "color")]
        [StringLength(10)]
        public string Color { get; set; }

        [DisplayName("アイコン")]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string IconUrl { get; set; }

        [DisplayName("ユーザリスト")]
        [HtmlTag("table")]
        public List<GroupUserList> SelectList { get; set; }

    }

    public class EditPanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","edit")]
        public string BtnBack { get; set; }

        [DisplayName("更新")]
        [HtmlTag("button")]
        [Event("edit/update","edit", isVerify:true)]
        [Redirect("search/load","search")]
        public string BtnUpdate { get; set; }

    }

}
