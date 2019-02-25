using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.ViewModels.Shain
{
    public class RemovePanel
    {

        [DisplayName("戻る")]
        [HtmlTag("button")]
        [Event("back","remove")]
        public string BtnBack { get; set; }

        [DisplayName("削除")]
        [HtmlTag("button")]
        [Event("remove/delete","remove", isVerify: true)]
        [Redirect("search/load","search")]
        public string BtnRemove { get; set; }

    }
    [DisplayName("削除")]
    [Description("削除します")]
    public class Remove : ViewModel
    {

        [DisplayName("ボタン")]
        [HtmlTag("panel")]
        public RemovePanel Panel { get; set; }

        [DisplayName("ID")]
        [HtmlTag("input", "hidden")]
        public string ShainId { get; set; }

        //ユーザ項目
        [DisplayName("社員番号")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [Required]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Shmi { get; set; }

        [DisplayName("よみがな")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Ymgn { get; set; }

        [DisplayName("雇用形態")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Kykti { get; set; }


        [DisplayName("部門名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Bmnmi { get; set; }

        [DisplayName("拠点名")]
        [StringLength(100)]
        [HtmlTag("input", "text")]
        [Grid(4)]
        public string Kytnmi { get; set; }


    }

}

