
using System.ComponentModel.DataAnnotations;
using VueMSFramework.Models.Common;

namespace VueMSFramework.Models.Example
{

    // アカウント
    public class Example : BaseModel
    {
        [Key]
        [StringLength(128)]
        public string ExampleId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(40)]
        public string TermAddr { get; set; }

        [StringLength(40)]
        public string RemoteAddr { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public string Ticket { get; set; }

        [StringLength(256)]
        public string EmailConfirmeKey { get; set; }


        public bool EmailConfirmed { get; set; } = false;

        public bool ManagerConfirmed { get; set; } = false;

        public bool LockoutEnabled { get; set; } = false;

        public decimal AccessCount { get; set; }= 0;

        public decimal AccessFailedCount { get; set; } = 0;

    }
}
