using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VueMSFramework.Data;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.Core.Utils.DataAnnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class OptionsAttribute : Attribute
    {
        private readonly ApplicationDbContext _context;


        // Private fields.
        //private string _options;
        private readonly IEnumerable<Option> opts = null;

        //public OptionsAttribute(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        // This constructor defines two required parameters: name and level.

        //public OptionsAttribute(string options)
        //{

        //    this._options = options;
        //}
        public OptionsAttribute(string field)
        {
            //DBコンテンツ取得
            var appArgs = new[] { "TestApp" };
            var adf = new ApplicationDbContextFactory();
            _context = adf.CreateDbContext(appArgs);

            _context.Database.OpenConnection();
            var conn = _context.Database.GetDbConnection();


            //データリスト
            using (var command = conn.CreateCommand())
            {
                //employeeテーブル情報取得
                command.CommandText = "select * from Options where Field = '" + field + "' order by OrderBy";


                using (var reader = command.ExecuteReader())
                {
                    var options = new List<Option>();
                    while (reader.Read())
                    {

                        //name: カラム名, type: データ型
                        var obj = new Object[reader.FieldCount];
                        var v = reader.GetValues(obj);
                        var opt = new Option();
                        for (var j = 0; j < reader.FieldCount; j++)
                        {
                            if (reader.GetName(j).Equals("Text")) opt.Text = obj[j].ToString();
                            if (reader.GetName(j).Equals("Value")) opt.Value = obj[j].ToString();
                            if (reader.GetName(j).Equals("Color")) opt.Color = obj[j].ToString();
                            if (reader.GetName(j).Equals("IconUrl")) opt.IconUrl = obj[j].ToString();
                            if (reader.GetName(j).Equals("Action")) opt.Action = obj[j].ToString();
                        }
                        options.Add(opt);

                    }
                    if (options.Count > 0)
                    {
                        this.opts = options.ToArray();
                    }
                }

            }


        }

        public OptionsAttribute(string table,string type)
        {
            var appArgs = new[] { "TestApp" };
            var adf = new ApplicationDbContextFactory();
            //DBコンテンツ取得
            _context = adf.CreateDbContext(appArgs);

            if (table.Equals("Contents"))
            {

                var list = (from a in _context.Contents
                           where a.ContentType.Equals(type)
                           orderby a.OrderBy ascending
                           select new Option
                           {
                               Value = a.ContentId,
                               Text = a.ContentName
                           }).ToList();

                if (list.Count > 0)
                {
                    this.opts = list.ToArray();
                }


            }
            else if (table.Equals("Categories"))
            {
                //カテゴリテーブルを検索
                var list = (from a in _context.Categories
                            orderby a.OrderBy ascending
                            select new Option
                            {
                                Value = a.CategoryId,
                                Text = a.CategoryName
                            }).ToList();


                if (list.Count > 0)
                {
                    this.opts = list.ToArray();
                }

            }

        }
        //public OptionsAttribute(string table, string text, string value, string type)
        //{
        //    //DBコンテンツ取得
        //    var appArgs = new[] { "TestApp" };
        //    var adf = new ApplicationDbContextFactory();
        //    _context = adf.CreateDbContext(appArgs);

        //    _context.Database.OpenConnection();
        //    var conn = _context.Database.GetDbConnection();


        //    //データリスト
        //    using (var command = conn.CreateCommand())
        //    {
        //        //employeeテーブル情報取得
        //        command.CommandText = "select * from " + table + " where ContentType = '" + type + "' order by OrderBy";
        //        using (var reader = command.ExecuteReader())
        //        {
        //            var options = new List<Option>();
        //            while (reader.Read())
        //            {

        //                //name: カラム名, type: データ型
        //                var obj = new Object[reader.FieldCount];
        //                var v = reader.GetValues(obj);
        //                var opt = new Option();
        //                for (var j = 0; j < reader.FieldCount; j++)
        //                {
        //                    if (reader.GetName(j).Equals(text)) opt.Text = obj[j].ToString();
        //                    if (reader.GetName(j).Equals(value)) opt.Value = obj[j].ToString();

        //                }
        //                options.Add(opt);

        //            }
        //            if (options.Count > 0)
        //            {
        //                this.opts = options.ToArray();
        //            }
        //        }

        //    }


        //}
        public OptionsAttribute(string table, string text, string value)
        {
            //DBコンテンツ取得
            var appArgs = new[] { "TestApp" };
            var adf = new ApplicationDbContextFactory();
            _context = adf.CreateDbContext(appArgs);

            _context.Database.OpenConnection();
            var conn = _context.Database.GetDbConnection();


            //データリスト
            using (var command = conn.CreateCommand())
            {
                //employeeテーブル情報取得
                command.CommandText = "select * from " + table + " order by OrderBy";
                using (var reader = command.ExecuteReader())
                {
                    var options = new List<Option>();
                    while (reader.Read())
                    {

                        //name: カラム名, type: データ型
                        var obj = new Object[reader.FieldCount];
                        var v = reader.GetValues(obj);
                        var opt = new Option();
                        for (var j = 0; j < reader.FieldCount; j++)
                        {
                            if (reader.GetName(j).Equals(text)) opt.Text = obj[j].ToString();
                            if (reader.GetName(j).Equals(value)) opt.Value = obj[j].ToString();

                        }
                        options.Add(opt);

                    }
                    if (options.Count > 0)
                    {
                        this.opts = options.ToArray();
                    }
                }

            }


        }
        public virtual IEnumerable<Option> Options
        {
            get {
                return opts;
            }
        }
 

    }
    [AttributeUsage(AttributeTargets.All)]
    public class HtmlTagAttribute : Attribute
    {
        private string _tag;
        private string _type;
        private string _inputMode;
        //private bool _isVerify;
        //private bool _isConfirm;

        private bool _multiple;

        private string _sortBy;
        private bool _sortDesc;
        private EnumPosition _position = EnumPosition.Top;

        public enum EnumPosition
        {
            Top, Buttom, Both, Free
        }

        public HtmlTagAttribute()
        {
            //_isVerify = true;
            //_isConfirm = true;
            _multiple = false;
            _inputMode = "verbatim";
        }

        // This constructor defines two required parameters: name and level.

        public HtmlTagAttribute(string tag)
        {
            this._tag = tag;
        }
        public HtmlTagAttribute(string tag,string type)
        {
            this._tag = tag;
            this._type = type;

        }
        public HtmlTagAttribute(string tag, string type,string inputMode)
        {
            this._tag = tag;
            this._type = type;
            this._inputMode = inputMode;

        }
        public HtmlTagAttribute(string tag, string type, string inputMode, bool multiple)
        {
            this._tag = tag;
            this._type = type;
            this._inputMode = inputMode;
            this._multiple = multiple;
        }
        public HtmlTagAttribute(string tag, string type, EnumPosition position)
        {
            this._tag = tag;
            this._type = type;
            this._position = position;
        }

        public HtmlTagAttribute(string tag, string sortBy, bool sortDesc)
        {
            this._tag = tag;
            this._sortBy = sortBy;
            this._sortDesc = sortDesc;
        }
        public virtual string Tag
        {
            get { return _tag; }
            set => _tag = value;
        }
        public virtual string Type
        {
            get { return _type; }
            set => _type = value;
        }
        public virtual bool Multiple
        {
            get { return _multiple; }
            set => _multiple = value;
        }

        public virtual string SortBy
        {
            get { return _sortBy; }
            set => _sortBy = value;
        }
        public virtual bool SortDesc
        {
            get { return _sortDesc; }
            set => _sortDesc = value;
        }
        public virtual string InutMode
        {
            get { return _inputMode; }
            set => _inputMode = value;
        }

        public virtual HtmlTagAttribute.EnumPosition Position
        {
            get { return _position; }
            set => _position = value;
        }

    }
    [AttributeUsage(AttributeTargets.All)]
    public class FormatAttribute : Attribute
    {
        private string _pattern;

        public FormatAttribute(string pattern)
        {
            this._pattern = pattern;
        }
        public virtual string Pattern
        {
            get { return _pattern; }
            set => _pattern = value;
        }

    }

    [AttributeUsage(AttributeTargets.All)]
    public class EventAttribute : Attribute
    {

        private string _pageName;
        private string _actionName;
        private string _sectionName;
        private string _paramItems;
        private bool _isVerify = false;
        private bool _isConfirm = false;
        private string _confirmMessage = "Do you want to execute the process?";

        public EventAttribute(string actionName, string sectionName, string pageName = null,string paramItems = null,bool isVerify = false, bool isConfirm = false, string confirmMessage = "Do you want to execute the process?")
        {
            this._sectionName = sectionName;
            this._actionName = actionName;
            this._pageName = pageName;
            this._paramItems = paramItems;
            this._isVerify = isVerify;
            this._isConfirm = isConfirm;
            this._confirmMessage = confirmMessage;

        }
 
        public virtual Event Event
        {
            get { return new Event {ParamItems = _paramItems, Action = _actionName, Section = _sectionName ,Page = _pageName}; }
        }
        public virtual string Action
        {
            get { return _actionName; }
            set => _actionName = value;
        }
        public virtual string Section
        {
            get { return _sectionName; }
            set => _sectionName = value;
        }
        public virtual string Page
        {
            get { return _pageName; }
            set => _pageName = value;
        }
        public virtual bool IsVerify
        {
            get { return _isVerify; }
            set => _isVerify = value;
        }
        public virtual bool IsConfirm
        {
            get { return _isConfirm; }
            set => _isConfirm = value;
        }
        public virtual string ConfirmMessage
        {
            get { return _confirmMessage; }
            set => _confirmMessage = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class RedirectAttribute : Attribute
    {

        private string _pageName;
        private string _actionName;
        private string _sectionName;
        private string _paramItems;
        private bool _isVerify = false;
        private bool _isConfirm = false;
        private string _confirmMessage = "Do you want to execute the process?";


        public RedirectAttribute(string actionName, string sectionName, string pageName = null, string paramItems = null)
        {
            this._sectionName = sectionName;
            this._actionName = actionName;
            this._pageName = pageName;
            this._paramItems = paramItems;
        }

        public virtual Event Event
        {
            get { return new Event { ParamItems = _paramItems, Action = _actionName, Section = _sectionName, Page = _pageName}; }
        }
        public virtual string Action
        {
            get { return _actionName; }
            set => _actionName = value;
        }
        public virtual string Section
        {
            get { return _sectionName; }
            set => _sectionName = value;
        }
        public virtual string Page
        {
            get { return _pageName; }
            set => _pageName = value;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class DecimalPointAttribute : Attribute
    {
        private string _step;

        public DecimalPointAttribute(string param)
        {
            this._step = param;
        }
        public virtual string Step 
        {
            get { return _step; }
            set => _step = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class GridAttribute : Attribute
    {
        private int _col;
        private int _offset = 0;
        private int _rightOffset = 0;
        private bool _firstRow = false;

        // This constructor defines two required parameters: name and level.

        public GridAttribute(int col)
        {
            this._col = col;
        }
        public GridAttribute(int col,int offset)
        {
            this._col = col;
            this._offset = offset;
        }
        public GridAttribute(int col, int offset, int rightOffset)
        {
            this._col = col;
            this._offset = offset;
            this._rightOffset = rightOffset;
        }
        public GridAttribute(bool firstRow,int col)
        {
            this._firstRow = firstRow;
            this._col = col;
        }
        public GridAttribute(bool firstRow, int col, int offset)
        {
            this._firstRow = firstRow;
            this._col = col;
            this._offset = offset;
        }
        public GridAttribute(bool firstRow, int col, int offset, int rightOffset)
        {
            this._firstRow = firstRow;
            this._col = col;
            this._offset = offset;
            this._rightOffset = rightOffset;
        }

        public virtual int Col
        {
            get { return _col; }
            set => _col = value;
        }
        public virtual int Offset
        {
            get { return _offset; }
            set => _offset = value;
        }
        public virtual int RightOffset
        {
            get { return _rightOffset; }
            set => _rightOffset = value;
        }
        public virtual bool IsGridFirstRow
        {
            get { return _firstRow; }
            set => _firstRow = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class PlaceholderAttribute : Attribute
    {
        private string placeHolderStr;

        // This constructor defines two required parameters: name and level.

        public PlaceholderAttribute(string str)
        {
            this.placeHolderStr = str;
        }
        public virtual string Placeholder
        {
            get { return placeHolderStr; }
            set => placeHolderStr = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class HelpAttribute : Attribute
    {
        private string text;

        // This constructor defines two required parameters: name and level.

        public HelpAttribute(string str)
        {
            this.text = str;
        }
        public virtual string Text
        {
            get { return text; }
            set => text = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class InvalidFeedbackAttribute : Attribute
    {
        private string text;

        // This constructor defines two required parameters: name and level.

        public InvalidFeedbackAttribute(string str)
        {
            this.text = str;
        }
        public virtual string Text
        {
            get { return text; }
            set => text = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class AutofocusAttribute : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.All)]
    public class DisplayOrderAttribute : Attribute
    {
        // Private fields.
        private int order;

        // This constructor defines two required parameters: name and level.

        public DisplayOrderAttribute(int order)
        {
            this.order = order;
        }
        public virtual int DisplayOrder
        {
            get { return order; }
            set => order = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class SortableAttribute : Attribute
    {
        private bool _enable;

        // This constructor defines two required parameters: name and level.

        public SortableAttribute(bool value)
        {
            this._enable = value;
        }
        public virtual bool Enable
        {
            get { return _enable; }
            set => _enable = value;
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class AuthAttribute : Attribute
    {
        public enum AuthType { UnAuth, Auth, Both }
        private AuthType _authType;
        // This constructor defines two required parameters: name and level.

        public AuthAttribute(AuthType param)
        {
            this._authType = param;
        }
        public virtual AuthType Type
        {
            get { return _authType; }
            set => _authType = value;
        }
    }

}
