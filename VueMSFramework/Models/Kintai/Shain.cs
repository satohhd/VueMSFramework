using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models.kintai
{
    //【社員 Shain】
    public class Shain :BaseModel
    {

        [Key]
        public string ShainId { get; set; }

        //ユーザ項目
        [DisplayName("社員番号")]
        [Required]
        [StringLength(100)]
        public string Shinbng { get; set; }

        [DisplayName("氏名")]
        [Required]
        [StringLength(100)]
        public string Shmi { get; set; }

        [DisplayName("よみがな")]
        [StringLength(100)]
        public string Ymgn { get; set; }

        [DisplayName("雇用形態")]
        [StringLength(100)]
        public string Kykti { get; set; }


        [DisplayName("部門名")]
        [StringLength(100)]
        public string Bmnmi { get; set; }

        [DisplayName("拠点名")]
        [StringLength(100)]
        public string Kytnmi { get; set; }

    }
}
