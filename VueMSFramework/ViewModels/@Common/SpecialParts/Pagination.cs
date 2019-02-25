using System;

namespace VueMSFramework.ViewModels.Common.SpecialParts
{
    public class Pagination
    {
        public Pagination()
        {
            RecordsPerPage = 5;
        }
        public int RecordCount { set; get; }
        public int PageSize
        {
            get
            {
                decimal rc = RecordCount;
                decimal rpp = RecordsPerPage;

                return (int)Math.Ceiling(rc / rpp);
            }
        }
        public int CurrentPageNumber { set; get; } = 1;
        public int RecordsPerPage { set; get; }



    }

}
