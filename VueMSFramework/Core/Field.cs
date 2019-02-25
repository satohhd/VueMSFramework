using System.Collections.Generic;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common.SpecialParts;
using static VueMSFramework.Core.Utils.DataAnnotations.HtmlTagAttribute;

namespace VueMSFramework.Core
{
    public class Event
    {
        public string ParamItems { get; set; }
        public string Param { get; set; }
        public string Action { get; set; }
        public string Section { get; set; }
        public string Page { get; set; }
    }

    public class Meta
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public Event Event { get; set; } = new Event();
        public string Description { get; set; }
        public string AssemblyQualifiedName { set; get; }
        public IEnumerable<Field> Fields { get; set; }

    }

    public class Field
    {
        public Field()
        {
            IsPrimaryKey = false;
            Autofocus = false;
            Readonly = false;
            Required = false;
            Hidden = true;
            MinLength = 0;
            MaxLength = 10;
            MinRange = 0;
            MaxRange = 999999;
            IsVerify = true;
            IsConfirm = true;
            SortDesc = false;
            Multiple = false;
            GridCol = 12;
            GridOffset = 0;
            IsGridFirstRow = false;
            DecimalPointStep = "0";


        }
        public string AssemblyQualifiedName { set; get; }
        public bool IsPrimaryKey { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public string Placeholder { get; set; }
        public bool Autofocus { get; set; }
        public bool Readonly { get; set; }

        public bool Required { get; set; }
        public string InputMode { get; set; }
        public string RequiredErrorMessage { get; set; }
        public string Pattern { get; set; }
        public string PatternErrorMessage { get; set; }
        public string FormatPattern { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string LengthErrorMessage { get; set; }
        public int MaxRange { get; set; }
        public int MinRange { get; set; }
        public string RangeErrorMessage { get; set; }
        public object DefaultValue { get; set; }
        public string Help { get; set; }
        public string Invalid { get; set; }
        public bool Hidden { get; set; }
        public int DisplayOrder { get; set; }
        public string Method { get; set; }
        public string Param { get; set; }

        public bool Sortable { get; set; }
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }

        public IEnumerable<Option> Options { get; set; }
        public bool IsVerify { get; set; }
        public bool IsConfirm { get; set; }
        public string ConfirmMessage { get; set; }

        public Meta Child {get; set;}
        public IEnumerable<Field>  ChildFields { get; set; }
        public bool Multiple { get; set; }

        public int GridCol { get; set; }
        public int GridOffset { get; set; }
        public bool IsGridFirstRow { get; set; }
        public EnumPosition Position { get; set; }
        public AuthAttribute.AuthType AuthType { get; set; }
        public Event Event { get; set; } = new Event();
        public Event Redirect { get; set; } = new Event();
        public string DecimalPointStep { get; set; }

        public string Key {
            get
            {
                return Name;
            }
        }
        public string Label {
            get
            {
                return Caption;
            }
        }

    }
}
