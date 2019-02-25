using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VueMSFramework.Models.Common
{
    public abstract class BaseModel
    {
        //システム項目
        public string Owner { get; set; }
        public DateTime? Registed { get; set; }
        public string Updater { get; set; }
        public DateTime? Updated { get; set; }
        public int Version { get; set; } = 0;
    }
    public abstract class DeleteBaseModel : BaseModel
    {
        //システム項目
        [Key]
        [Column(Order = 0)]
        public string DeleteId { get; set; } = (Guid.NewGuid()).ToString();
        public DateTime? Deleted { get; set; } = DateTime.Now;
        public string Deleter { get; set; }

    }

}
