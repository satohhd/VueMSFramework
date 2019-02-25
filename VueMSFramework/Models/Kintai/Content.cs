using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models
{
    public class Content : BaseModel
    {

        [Key]
        public string ContentId { get; set; }


        [DisplayName("内容種類")]
        [StringLength(30)]
        public string ContentType { get; set; }

        [DisplayName("内容コード")]
        [StringLength(10)]
        public string ContentCode { get; set; }

        [DisplayName("内容名")]
        [StringLength(100)]
        public string ContentName { get; set; }

        [DisplayName("表示順")]
        [Range(0, 1000)]
        public int OrderBy { get; set; } = 0;

    }
}
