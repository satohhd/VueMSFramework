using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using VueMSFramework.Core.Utils.DataAnnotations;
using VueMSFramework.ViewModels.Common;

namespace VueMSFramework.Core
{
    public class Dna
    {
        public Meta Meta { get; set; }
        public IEnumerable<Field> Fields { get; set; }
        public Object ViewModel { get; set; }
    }
 
    public class DnaManager
    {
        //private IStringLocalizerFactory _factory;
        private IStringLocalizer _localizer = null;

        public DnaManager(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public DnaManager()
        {
           //モデルコピーのとき利用
        }

        private bool _IsList(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(List<>);
        }
        public Dna GetDna(string viewModelClassName)
        {
            try {

                Type model;
                if (viewModelClassName.Contains("System.Object"))
                {
                    model = typeof(ViewModel);

                }
                else
                {
                    model = Type.GetType(viewModelClassName);

                }
                if (model == null)
                {
                    var d1 = new Dna
                    {
                        Fields = new Field[0],
                        //ViewModel = new ViewModel()
                    };
                    return d1;
                }

                //var assemblyName = new AssemblyName(model.GetTypeInfo().Assembly.FullName);
                //if (_factory != null)
                //{
                //    try
                //    {
                //        _localizer = _factory.Create(model);
                //        //_localizer2 = factory.Create("SharedResource", assemblyName.Name);
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //    }

                //}


                var dic = new Dictionary<String, Field>();
                List<Field> orderFields = new List<Field>();

                //meta info

                var meta = new Meta();
                meta.Name = model.Name;
                var modelDisplayName = (DisplayNameAttribute)Attribute.GetCustomAttribute(model, typeof(DisplayNameAttribute));
                if (modelDisplayName != null)
                {
                    meta.Caption = modelDisplayName.DisplayName;
                }
                else
                {
                    meta.Caption = meta.Name;

                }
                var modelDescription = (DescriptionAttribute)Attribute.GetCustomAttribute(model, typeof(DescriptionAttribute));
                if (modelDescription != null)
                {
                    meta.Description = modelDescription.Description;
                }
                var modelAction = (EventAttribute)Attribute.GetCustomAttribute(model, typeof(EventAttribute));
                if (modelAction != null)
                {
                    meta.Event = modelAction.Event;
                }

                //field define

                MemberInfo[] MyMemberInfo = model.GetMembers();
                for (int i = 0; i < MyMemberInfo.Length; i++)
                {
                    var key = (KeyAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(KeyAttribute));
                    var readOnly = (ReadOnlyAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(ReadOnlyAttribute));
                    var required = (RequiredAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RequiredAttribute));
                    var stringLength = (StringLengthAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(StringLengthAttribute));
                    var valueRange = (RangeAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RangeAttribute));
                    var placeHolder = (PlaceholderAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(PlaceholderAttribute));
                    var htmlTag = (HtmlTagAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(HtmlTagAttribute));
                    var format = (FormatAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(FormatAttribute));
                    var options = (OptionsAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(OptionsAttribute));
                    var auth = (AuthAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(AuthAttribute));
                    var grid = (GridAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(GridAttribute));
                    var regularExpression = (RegularExpressionAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RegularExpressionAttribute));
                    var displayName = (DisplayNameAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DisplayNameAttribute));
                    var description = (DescriptionAttribute)Attribute.GetCustomAttribute(model, typeof(DescriptionAttribute));

                    var defaultValue = (DefaultValueAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DefaultValueAttribute));
                    var help = (HelpAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(HelpAttribute));
                    var invalid = (InvalidFeedbackAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(InvalidFeedbackAttribute));
                    var autofocus = (AutofocusAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(AutofocusAttribute));
                    var displayOrder = (DisplayOrderAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DisplayOrderAttribute));
                    var sortable = (SortableAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(SortableAttribute));
                    var action = (EventAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(EventAttribute));
                    var redirect = (RedirectAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RedirectAttribute));
                    var decimalPoint = (DecimalPointAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DecimalPointAttribute));

                    if (displayName != null)
                    {
                        var sf = new Field();

                        //defaut
                        sf.Name = MyMemberInfo[i].Name.Substring(0, 1).ToLower() + MyMemberInfo[i].Name.Substring(1);
                        //sf.Name = MyMemberInfo[i].Name;
                        sf.Caption = sf.Name;
                        sf.Sortable = true;
                        sf.Readonly = false;
                        sf.Autofocus = false;
                        sf.IsPrimaryKey = false;
                        sf.LengthErrorMessage = "";
                        sf.Pattern = null;
                        sf.PatternErrorMessage = "";
                        sf.Placeholder = null;
                        sf.Required = false;
                        sf.RequiredErrorMessage = "";
                        sf.DefaultValue = null;
                        sf.Help = null;
                        sf.Invalid = null;
                        sf.DisplayOrder = 999;
                        sf.ChildFields = null;
                        sf.IsVerify = false;
                        sf.IsConfirm = false;
                        sf.Options = null;
                        sf.Multiple = false;
                        sf.GridCol = 12;
                        sf.GridOffset = 0;
                        sf.IsGridFirstRow = false;
                        sf.AuthType = AuthAttribute.AuthType.Both;

                        //個別指定
                        if (decimalPoint != null)
                        {
                            sf.DecimalPointStep = decimalPoint.Step;
                        }
                        if (auth != null)
                        {
                            sf.AuthType = auth.Type;
                        }
                        if (grid != null)
                        {
                            sf.GridCol = grid.Col;
                            sf.GridOffset = grid.Offset;
                            sf.IsGridFirstRow = grid.IsGridFirstRow;
                        }
                        if (displayName != null) { 
                            sf.Caption = displayName.DisplayName;
                            if (_localizer != null)
                            {
                                sf.Caption = _localizer.GetString(displayName.DisplayName);
                            }
                        }
                        if (key != null)
                        {
                            sf.IsPrimaryKey = true;
                            sf.Readonly = true;
                            sf.Required = true;
                        }
                        if (readOnly != null)
                        {
                            //sf.Readonly = readOnly.IsReadOnly;
                            sf.Readonly = true;
                        }
                        if (htmlTag != null)
                        {
                            //タグ指定がある場合に表示する

                            sf.Hidden = false;

                            if (htmlTag.Tag  != null)
                                sf.Tag = htmlTag.Tag;
                            if (htmlTag.Type != null)
                                sf.Type = htmlTag.Type;

                            if (htmlTag.Tag.Equals("input") && htmlTag.Type.Equals("hidden"))
                            {
                                continue;
                            }
                            else if (htmlTag.Tag.Equals("section"))
                            {

                                sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                sf.ChildFields = d2.Fields;
                                sf.Child = d2.Meta;
                                

                            }
                            else if (htmlTag.Tag.Equals("panel"))
                            {

                                sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                sf.ChildFields = d2.Fields;
                                sf.Child = d2.Meta;


                            }
                            else if (htmlTag.Tag.Equals("dialog"))
                            {

                                sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                sf.ChildFields = d2.Fields;
                                sf.Child = d2.Meta;


                            }
                            else if (htmlTag.Tag.Equals("table"))
                            {

                                if (_IsList(((PropertyInfo)MyMemberInfo[i]).PropertyType))
                                {
                                    Type elementType = ((PropertyInfo)MyMemberInfo[i]).PropertyType.GetGenericArguments()[0];
                                    sf.AssemblyQualifiedName = elementType.AssemblyQualifiedName;
                                }
                                else
                                {
                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                }
                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                sf.ChildFields = d2.Fields;
                                sf.Child = d2.Meta;

                                if (htmlTag.SortBy != "")
                                {
                                    sf.SortBy = htmlTag.SortBy;
                                    sf.SortDesc = htmlTag.SortDesc;

                                }
                                 
                            }
                            else if (htmlTag.Tag.Equals("cards"))
                            {

                                if (_IsList(((PropertyInfo)MyMemberInfo[i]).PropertyType))
                                {
                                    Type elementType = ((PropertyInfo)MyMemberInfo[i]).PropertyType.GetGenericArguments()[0];
                                    sf.AssemblyQualifiedName = elementType.AssemblyQualifiedName;
                                }
                                else
                                {
                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                }


                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                                if (htmlTag.SortBy != null)
                                    {
                                        sf.SortBy = htmlTag.SortBy;
                                        sf.SortDesc = htmlTag.SortDesc;

                                    }

                            }
                            //else if (htmlTag.Tag.Equals("buttons"))
                            //{

                            //if (_IsList(((PropertyInfo)MyMemberInfo[i]).PropertyType))
                            //{
                            //    Type elementType = ((PropertyInfo)MyMemberInfo[i]).PropertyType.GetGenericArguments()[0];
                            //    sf.AssemblyQualifiedName = elementType.AssemblyQualifiedName;
                            //}
                            //else
                            //{
                            //    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                            //}

                            ////var dm = new DnaManager(_localizer);
                            //var dm = new DnaManager(_localizer);
                            //var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                            //    sf.ChildFields = d2.Fields;
                            //}
                            else if (htmlTag.Tag.Equals("tabs"))
                            {
                                sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                            }
                            else if (htmlTag.Tag.Equals("tab"))
                            {
                                //var dm = new DnaManager(_localizer);
                                var dm = new DnaManager(_localizer);
                                var d2 = dm.GetDna(((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                                sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                            }
                            else if (htmlTag.Tag.Equals("file"))
                            {
                                sf.Multiple = htmlTag.Multiple;
                            }
                        }
                        if (format != null)
                        {
                            if (format.Pattern != null)
                            {
                                sf.FormatPattern = format.Pattern;
                            }
                        }

                        if (options != null)
                        {
                            if (options.Options != null)
                            {
                                sf.Options = options.Options;
                            }
                        }
                        if (stringLength != null)
                        {
                            sf.MaxLength = stringLength.MaximumLength;
                            sf.MinLength = stringLength.MinimumLength;
                            if (stringLength.ErrorMessage != null)
                                sf.LengthErrorMessage = stringLength.ErrorMessage;
                        }
                        else
                        {
                            sf.MaxLength = 100;
                            sf.MinLength = 0;
                        }
                        if (valueRange != null)
                        {
                            sf.MaxRange = (int)valueRange.Maximum;
                            sf.MinRange = (int)valueRange.Minimum;
                            if (valueRange.ErrorMessage != null)
                                sf.RangeErrorMessage = valueRange.ErrorMessage;
                        }
                        else
                        {
                            sf.MaxRange = 99999;
                            sf.MinRange = 0;
                        }

                        if (required != null)
                        {
                            sf.Required = true;
                            if (required.ErrorMessage != null)
                                sf.RequiredErrorMessage = required.ErrorMessage;
                        }
                        if (regularExpression != null)
                        {
                            sf.Pattern = regularExpression.Pattern.Replace(@"\\", @"\");
                            //if (regularExpression.ErrorMessage != null)
                            //    sf.PatternErrorMessage = regularExpression.ErrorMessage;
                        }
                        if (placeHolder != null)
                        {
                            if (placeHolder.Placeholder != null)
                            sf.Placeholder = placeHolder.Placeholder;
                        }
                        if (defaultValue != null)
                        {
                            if (defaultValue.Value != null)
                                sf.DefaultValue = defaultValue.Value;
                        }
                        if (help != null)
                        {
                            if (help.Text != null)
                                sf.Help = help.Text;
                        }
                        if (invalid != null)
                        {
                            if (invalid.Text != null)
                                sf.Invalid = invalid.Text;
                        }
                        if (autofocus != null)
                        {
                                sf.Autofocus = true;
                        }
                        if (displayOrder != null)
                        {
                            sf.DisplayOrder = displayOrder.DisplayOrder;
                        }
                        if (sortable != null)
                        {
                            sf.Sortable = sortable.Enable;
                        }
                        //イベント
                        if (action != null)
                        {
                            sf.Event = action.Event;
                            sf.IsVerify = action.IsVerify;
                            sf.IsConfirm = action.IsConfirm;
                            sf.ConfirmMessage= action.ConfirmMessage;

                        }
                        //リダイレクト
                        if (redirect != null)
                        {
                            sf.Redirect = redirect.Event;

                        }
                        
                        dic.Add(sf.Name,sf);

                        //レイアウト調整用のグリッドスペース
                        if (grid != null)
                        {
                            if (grid.RightOffset > 0)
                            {
                                var sf_space = new Field
                                {
                                    Name = Guid.NewGuid().ToString(),
                                    Tag = "space",
                                    GridCol = grid.RightOffset,
                                    DisplayOrder = sf.DisplayOrder
                                };
                                dic.Add(sf_space.Name, sf_space);
                            }
                        }
                    }
                }
                //フィールド情報が作成されない場合は、モデル情報を全部作成する
                if (dic.Count == 0)
                {
                    for (int i = 0; i < MyMemberInfo.Length; i++)
                    {
                        var key = (KeyAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(KeyAttribute));
                        var readOnly = (ReadOnlyAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(ReadOnlyAttribute));
                        var required = (RequiredAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RequiredAttribute));
                        var stringLength = (StringLengthAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(StringLengthAttribute));
                        var valueRange = (RangeAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RangeAttribute));
                        var placeHolder = (PlaceholderAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(PlaceholderAttribute));
                        var htmlTag = (HtmlTagAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(HtmlTagAttribute));
                        var format = (FormatAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(FormatAttribute));
                        var options = (OptionsAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(OptionsAttribute));
                        var auth = (AuthAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(AuthAttribute));
                        var grid = (GridAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(GridAttribute));
                        var regularExpression = (RegularExpressionAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(RegularExpressionAttribute));
                        var displayName = (DisplayNameAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DisplayNameAttribute));
                        var description = (DescriptionAttribute)Attribute.GetCustomAttribute(model, typeof(DescriptionAttribute));

                        var defaultValue = (DefaultValueAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DefaultValueAttribute));
                        var help = (HelpAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(HelpAttribute));
                        var invalid = (InvalidFeedbackAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(InvalidFeedbackAttribute));
                        var autofocus = (AutofocusAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(AutofocusAttribute));
                        var displayOrder = (DisplayOrderAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DisplayOrderAttribute));
                        var sortable = (SortableAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(SortableAttribute));
                        var action = (EventAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(EventAttribute));
                        var decimalPoint = (DecimalPointAttribute)Attribute.GetCustomAttribute(MyMemberInfo[i], typeof(DecimalPointAttribute));
                        var sf = new Field();
                        var nm = MyMemberInfo[i].Name.Substring(0, 4).ToLower();
                        if (nm.Equals("set_") || nm.Equals("get_"))
                        {
                        }
                        else
                        {

                            //defaut
                            sf.Name = MyMemberInfo[i].Name.Substring(0, 1).ToLower() + MyMemberInfo[i].Name.Substring(1);
                            sf.Caption = sf.Name;
                            sf.Sortable = true;
                            sf.Readonly = false;
                            sf.Autofocus = false;
                            sf.IsPrimaryKey = false;
                            sf.LengthErrorMessage = "";
                            sf.Pattern = null;
                            sf.PatternErrorMessage = "";
                            sf.Placeholder = null;
                            sf.Required = false;
                            sf.RequiredErrorMessage = "";
                            sf.DefaultValue = null;
                            sf.Help = null;
                            sf.Invalid = null;
                            sf.DisplayOrder = 999;
                            sf.ChildFields = null;
                            sf.IsVerify = true;
                            sf.IsConfirm = true;
                            sf.Options = null;
                            sf.Multiple = false;
                            sf.GridCol = 12;
                            sf.GridOffset = 0;
                            sf.IsGridFirstRow = false;
                            sf.AuthType = AuthAttribute.AuthType.Both;

                            //個別指定
                            if (decimalPoint != null)
                            {
                                sf.DecimalPointStep = decimalPoint.Step;
                            }
                            if (auth != null)
                            {
                                sf.AuthType = auth.Type;
                            }
                            if (grid != null)
                            {
                                sf.GridCol = grid.Col;
                                sf.GridOffset = grid.Offset;
                                sf.IsGridFirstRow = grid.IsGridFirstRow;
                            }
                            if (displayName != null)
                            {
                                sf.Caption = displayName.DisplayName;
                                if (_localizer != null)
                                {
                                    sf.Caption = _localizer.GetString(displayName.DisplayName);
                                }
                            }
                            if (key != null)
                            {
                                sf.IsPrimaryKey = true;
                                sf.Readonly = true;
                                sf.Required = true;
                            }
                            if (readOnly != null)
                            {
                                //sf.Readonly = readOnly.IsReadOnly;
                                sf.Readonly = true;
                            }
                            if (htmlTag != null)
                            {
                                //タグ指定がある場合に表示する

                                sf.Hidden = false;

                                if (htmlTag.Tag != null)
                                    sf.Tag = htmlTag.Tag;
                                if (htmlTag.Type != null)
                                    sf.Type = htmlTag.Type;

                                if (htmlTag.Tag.Equals("input") && htmlTag.Type.Equals("hidden"))
                                {
                                    continue;
                                }
                                else if (htmlTag.Tag.Equals("section"))
                                {

                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;


                                }
                                else if (htmlTag.Tag.Equals("panel"))
                                {

                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;


                                }
                                else if (htmlTag.Tag.Equals("dialog"))
                                {

                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;


                                }
                                else if (htmlTag.Tag.Equals("table"))
                                {

                                    if (_IsList(((PropertyInfo)MyMemberInfo[i]).PropertyType))
                                    {
                                        Type elementType = ((PropertyInfo)MyMemberInfo[i]).PropertyType.GetGenericArguments()[0];
                                        sf.AssemblyQualifiedName = elementType.AssemblyQualifiedName;
                                    }
                                    else
                                    {
                                        sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                    }
                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                                    if (htmlTag.SortBy != "")
                                    {
                                        sf.SortBy = htmlTag.SortBy;
                                        sf.SortDesc = htmlTag.SortDesc;

                                    }

                                }
                                else if (htmlTag.Tag.Equals("cards"))
                                {

                                    if (_IsList(((PropertyInfo)MyMemberInfo[i]).PropertyType))
                                    {
                                        Type elementType = ((PropertyInfo)MyMemberInfo[i]).PropertyType.GetGenericArguments()[0];
                                        sf.AssemblyQualifiedName = elementType.AssemblyQualifiedName;
                                    }
                                    else
                                    {
                                        sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                    }


                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                                    if (htmlTag.SortBy != null)
                                    {
                                        sf.SortBy = htmlTag.SortBy;
                                        sf.SortDesc = htmlTag.SortDesc;

                                    }

                                }
                                //else if (htmlTag.Tag.Equals("buttons"))
                                //{

                                //if (_IsList(((PropertyInfo)MyMemberInfo[i]).PropertyType))
                                //{
                                //    Type elementType = ((PropertyInfo)MyMemberInfo[i]).PropertyType.GetGenericArguments()[0];
                                //    sf.AssemblyQualifiedName = elementType.AssemblyQualifiedName;
                                //}
                                //else
                                //{
                                //    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                //}

                                ////var dm = new DnaManager(_localizer);
                                //var dm = new DnaManager(_localizer);
                                //var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                //    sf.ChildFields = d2.Fields;
                                //}
                                else if (htmlTag.Tag.Equals("tabs"))
                                {
                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;

                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(sf.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                                }
                                else if (htmlTag.Tag.Equals("tab"))
                                {
                                    //var dm = new DnaManager(_localizer);
                                    var dm = new DnaManager(_localizer);
                                    var d2 = dm.GetDna(((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName);
                                    sf.ChildFields = d2.Fields;
                                    sf.Child = d2.Meta;

                                    sf.AssemblyQualifiedName = ((PropertyInfo)MyMemberInfo[i]).PropertyType.AssemblyQualifiedName;
                                }
                                else if (htmlTag.Tag.Equals("file"))
                                {
                                    sf.Multiple = htmlTag.Multiple;
                                }
                            }
                            if (format != null)
                            {
                                if (format.Pattern != null)
                                {
                                    sf.FormatPattern = format.Pattern;
                                }
                            }

                            if (options != null)
                            {
                                if (options.Options != null)
                                {
                                    sf.Options = options.Options;
                                }
                            }
                            if (stringLength != null)
                            {
                                sf.MaxLength = stringLength.MaximumLength;
                                sf.MinLength = stringLength.MinimumLength;
                                if (stringLength.ErrorMessage != null)
                                    sf.LengthErrorMessage = stringLength.ErrorMessage;
                            }
                            else
                            {
                                sf.MaxLength = 100;
                                sf.MinLength = 0;
                            }
                            if (valueRange != null)
                            {
                                sf.MaxRange = (int)valueRange.Maximum;
                                sf.MinRange = (int)valueRange.Minimum;
                                if (valueRange.ErrorMessage != null)
                                    sf.RangeErrorMessage = valueRange.ErrorMessage;
                            }
                            else
                            {
                                sf.MaxRange = 99999;
                                sf.MinRange = 0;
                            }

                            if (required != null)
                            {
                                sf.Required = true;
                                if (required.ErrorMessage != null)
                                    sf.RequiredErrorMessage = required.ErrorMessage;
                            }
                            if (regularExpression != null)
                            {
                                sf.Pattern = regularExpression.Pattern.Replace(@"\\", @"\");
                                //if (regularExpression.ErrorMessage != null)
                                //    sf.PatternErrorMessage = regularExpression.ErrorMessage;
                            }
                            if (placeHolder != null)
                            {
                                if (placeHolder.Placeholder != null)
                                    sf.Placeholder = placeHolder.Placeholder;
                            }
                            if (defaultValue != null)
                            {
                                if (defaultValue.Value != null)
                                    sf.DefaultValue = defaultValue.Value;
                            }
                            if (help != null)
                            {
                                if (help.Text != null)
                                    sf.Help = help.Text;
                            }
                            if (invalid != null)
                            {
                                if (invalid.Text != null)
                                    sf.Invalid = invalid.Text;
                            }
                            if (autofocus != null)
                            {
                                sf.Autofocus = true;
                            }
                            if (displayOrder != null)
                            {
                                sf.DisplayOrder = displayOrder.DisplayOrder;
                            }
                            if (sortable != null)
                            {
                                sf.Sortable = sortable.Enable;
                            }
                            //トリガー
                            if (action != null)
                            {
                                sf.Event = action.Event;
                                sf.IsVerify = action.IsVerify;
                                sf.IsConfirm = action.IsConfirm;
                                sf.ConfirmMessage = action.ConfirmMessage;

                            }

                            dic.Add(sf.Name, sf);

                            //レイアウト調整用のグリッドスペース
                            if (grid != null)
                            {
                                if (grid.RightOffset > 0)
                                {
                                    var sf_space = new Field
                                    {
                                        Name = Guid.NewGuid().ToString(),
                                        Tag = "space",
                                        GridCol = grid.RightOffset,
                                        DisplayOrder = sf.DisplayOrder
                                    };
                                    dic.Add(sf_space.Name, sf_space);
                                }
                            }
                        }
                    }

                }
                // DisplayOrder でソートする (未使用)
                var _dns = dic.OrderBy((x) => x.Value.DisplayOrder);
                foreach (var v in _dns)
                {
                    orderFields.Add(v.Value);
                }
                meta.Fields = orderFields;
                var d = new Dna
                {
                    Meta = meta,
                    Fields = orderFields,
                    ViewModel = Activator.CreateInstance(model)
                };
                return d;
            }catch(Exception ex)
            {
                
                Console.WriteLine(ex.StackTrace);
                var d1 = new Dna
                {
                    Fields = new Field[0],
                    //ViewModel = new ViewModel()
                };
                return d1;
            }
        }
    }
 


}
