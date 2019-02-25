using System;

namespace VueMSFramework.ViewModels.Common.SpecialParts
{
    public class File
    {
        public File()
        {
        }
        public File(string str)
        {

            var arr = str.Split(',');
            if (arr.Length == 4)
            {
                FileName = arr[0];
                FileType = arr[1];
                Url = arr[2];
                FileSize = int.Parse(arr[3]);
            }

        }

        public string FileName { get; set; }
        public string Url { get; set; }
        public string UrlFileName { get; set; }
        public long FileSize { get; set; } = 0;
        public string FileType { get; set; }
        public string Base64StringContents { get; set; }
        public virtual string FileSizeMBorKB
        {
            get {

                if (FileSize >= 1000000)
                {
                    return Math.Round((decimal)FileSize / (1024 * 1024),1).ToString() + "MByte";
                }
                else
                {
                    return Math.Round((decimal)FileSize / (1024), 1).ToString() + "KByte";
                }

            }
        }

    }
}
