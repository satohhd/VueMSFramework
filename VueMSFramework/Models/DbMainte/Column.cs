using System.ComponentModel.DataAnnotations;

namespace VueMSFramework.Models
{
    //【DbMainte DBメンテナンス】
    public class Column
    {
        [Key]
        public int Cid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Notnull { get; set; }
        public string Dflt_value { get; set; }
        public int Pk { get; set; }

    }
}
