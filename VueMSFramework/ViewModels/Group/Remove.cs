using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Group
{

    public class RemovePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove",isVerify:true,isConfirm:true)]
        [Redirect("search/load","search")]
        public string BtnRemove { get; set; }


    }
    public class Remove : ViewModel
    {

        [DisplayName("操作パネル")]
        [HtmlTag("panel")]
        public RemovePanel Panel { get; set; }

        //システム項目
        [DisplayName("グループID")]
        [HtmlTag("input", "hidden")]
        public string GroupId { get; set; }


        //ユーザ項目
        [DisplayName("グループ名")]
        [ReadOnly(true)]
        [HtmlTag("input", "text")]
        [StringLength(100)]
        public string GroupName { get; set; }

        [DisplayName("表示順")]
        [ReadOnly(true)]
        [HtmlTag("input", "number")]
        public int OrderBy { get; set; }

        [DisplayName("表示色")]
        [ReadOnly(true)]
        [HtmlTag("input", "color")]
        [StringLength(10)]
        public string Color { get; set; }

        [DisplayName("アイコン")]
        [ReadOnly(true)]
        [HtmlTag("input", "text")]
        [StringLength(255)]
        public string IconUrl { get; set; }

        [DisplayName("ユーザリスト")]
        [ReadOnly(true)]
        [HtmlTag("table")]
        public List<GroupUserList> SelectList { get; set; }


    }

}

