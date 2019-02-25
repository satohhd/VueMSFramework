using System;
using System.Collections.Generic;
using VueMSFramework.Core;

namespace VueMSFramework.ViewModels.Common
{
    public abstract class ViewModel
    {

        //System control item
        public string _caller { get; set; }
        public string _rowVariant { get; set; }
        public string _message { get; set; }

        [Obsolete("今後、invokeを使うようにしてください。",true)]
        public string _sectionParam { get; set; }
        public Event _event { get; set; } = new Event();
        public Event _redirect { get; set; } = new Event();
        [Obsolete("今後、利用できません", true)]
        public string _redirectParam { get; set; }
        public Event _errorRedirect { get; set; } = new Event();
        public IDictionary<string, object> Options { get; set; } = new Dictionary<string, object>();

        //Users common items derived from DB
        public string Owner { get; set; }
        public string Updater { get; set; }
        public DateTime? Registed { get; set; }
        public DateTime? Updated { get; set; }

        public int Version { get; set; } = 0;


    }
}
