using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VueMSFramework.Core;
using VueMSFramework.Data;
using VueMSFramework.Helpers;
using VueMSFramework.Models.Auth;

namespace Vue2Spa.Controllers
{

    public abstract class PageController : ControllerBase
    {
        protected ApplicationDbContext _context;
        protected IStringLocalizer _localizer;
        protected DnaManager _dm;

        public PageController(ApplicationDbContext context, IStringLocalizer localizer)
        {
            _context = context;
            _localizer = localizer;
            _dm = new DnaManager(_localizer);
            Console.WriteLine("SharedMethodController constractor");
        }


        /**
         *  コントローラー公開メソッド
         *
         *
         */


        [HttpGet, Route("dna/{modelName}")]
        public ActionResult<IEnumerable<Field>> GetDna(string modelName)
        {
            //var _dm = new DnaManager(_localizer);
            Dna dna = null;
            try
            {
                dna = _dm.GetDna(modelName);
                return Ok(dna);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost, Route("upload")]
        public ActionResult<VueMSFramework.ViewModels.Common.SpecialParts.File> Upload([FromBody]VueMSFramework.ViewModels.Common.SpecialParts.File file)
        {
            Guid urlFileName = Guid.NewGuid();

            file.FileName = Path.GetFileName(file.FileName);

            //うまく取り除けないので / を後ろから取得する
            var pos = file.FileName.LastIndexOf(@"\");
            if (pos > 0)
            {
                file.FileName = file.FileName.Substring(pos + 1);
            }
            string ext = Path.GetExtension(file.FileName);
            file.UrlFileName = urlFileName.ToString() + ext;
            var f = Base64toPhysicalFile(file);
            return Ok(f);
        }



        /**
         *  メソッド群
         *
         *
         */

        public static string DecodeJwt(string tokenString)
        {
            return DecodeJwt(tokenString, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        }
        public static string DecodeJwt(string tokenString,string keyString)
        {

            if (keyString == null) return null;

            // 鍵 チケットの一部をキーとする
            //var keyString = tiket.Substring(5,20);
 
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            // トークン操作用のクラス
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            // トークンの文字列表現
            //var tokenString = model.Param;
            // トークン検証用のパラメータを用意
            // Audience, Issuer, Lifetimeに関してはデフォルトで検証が有効になっている
            // audが空でexpが期限切れなのでValidateAudienceとValidateLifetimeはfalseにしておく
            var validationParams = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = keyString,
                ValidateLifetime = false,
                IssuerSigningKey = key,
            };
            try
            {
                // 第三引数にSecurityToken型の変数を参照で渡しておくと、検証済みのトークンが出力される
                handler.ValidateToken(tokenString, validationParams, out SecurityToken token);
                var json = ((System.IdentityModel.Tokens.Jwt.JwtSecurityToken)token).Payload.SerializeToJson();
                //Dictionary<string, object> dic = list.ToDictionary(item => item.Key, item => item.Value);
                return json;

            }
            catch (Exception e)
            {
                // ValidateTokenで検証に失敗した場合はここにやってくる
                Console.WriteLine("トークンが無効です: " + e.Message);
                return null;

            }

        }

        protected  JwtSecurityToken CreateJwtSecurityToken(Account account)
        {
            // JWT に含めるクレーム
            var claims = new List<Claim>()
            {
                // JwtBearerAuthentication 用
                new Claim(JwtRegisteredClaimNames.Jti, account.AccountId),
                new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                // User.Identity プロパティ用
                new Claim(ClaimTypes.Sid, account.AccountId),
                new Claim(ClaimTypes.Name, account.UserName),
            };



            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var appSettingsSection = configurationBuilder.Build().GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var token = new JwtSecurityToken(
             issuer: appSettings.SiteUrl,
              claims: claims,
              expires: DateTime.UtcNow.AddDays(7),
              signingCredentials: new SigningCredentials(
                  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                  SecurityAlgorithms.HmacSha256
              )
          );

            return token;
        }



        /* Base64データ⇒物理ファイル化 */
        protected VueMSFramework.ViewModels.Common.SpecialParts.File Base64toPhysicalFile(VueMSFramework.ViewModels.Common.SpecialParts.File file)
        {
            //データがない場合は抜ける 
            if (file.Base64StringContents == null) return file;

            var currentPath = Directory.GetCurrentDirectory();
            var wwwroot = "/wwwroot";
            var filedeploypath = "/files/";

            var strarr = file.Base64StringContents.Split(',');
            byte[] bs;
            if (strarr.Length == 2)
            {

                //バイト型配列に戻す
                bs = Convert.FromBase64String(strarr[1]);

                file.FileType = strarr[0];
                //ファイルサイズ
                file.FileSize = bs.Length;

            }
            else
            {

                //バイト型配列に戻す
                bs = Convert.FromBase64String(strarr[0]);

                //ファイルサイズ
                file.FileSize = bs.Length;
            }


            //ファイルに保存する
            //保存するファイル名
            string url = filedeploypath + file.UrlFileName;

            //ファイルに書き込む
            FileStream outFile = new FileStream(currentPath + wwwroot + url, FileMode.Create, FileAccess.Write);
            outFile.Write(bs, 0, bs.Length);
            outFile.Close();

            file.Url = url;
            file.Base64StringContents = null;

            return file;
        }
        /* 物理ファイル⇒Base64データ */
        protected void PhysicalFiletoBase64(VueMSFramework.ViewModels.Common.SpecialParts.File file)
        {
            //データがない場合は抜ける 
            if (file.Url == null) return;
            var currentPath = Directory.GetCurrentDirectory();
            var wwwroot = "/wwwroot";
            using (var fin = new StreamReader(wwwroot + file.Url))
            {
                string body = fin.ReadToEnd();
                byte[] data = System.Text.Encoding.ASCII.GetBytes(body);
                var cn = Convert.ToBase64String(data);
                file.Base64StringContents = cn;
            }
        }


        protected string ExportTableData(string tableName, ApplicationDbContext context)
        {
            var fileName = tableName + ".xlsx";
            var filePath = Path.GetTempFileName();
            try
            {
                //ブック作成
                var book = CreateNewBook(fileName);

                //シート無しのexcelファイルは保存は出来るが、開くとエラーが発生する
                var sheet = book.CreateSheet(tableName);

                //データ有無
                var _isExists = false;

                //データリスト
                context.Database.OpenConnection();
                var conn = context.Database.GetDbConnection();
                using (var command = conn.CreateCommand())
                {
                    //テーブル情報取得
                    command.CommandText = "select * from " + tableName + ";";

                    var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;

                    using (var reader = command.ExecuteReader())
                    {
                        var i = 0;
                        //ヘッダ
                        for (var j = 0; j < reader.FieldCount; j++)
                        {
                            WriteCell(sheet, j, 0, reader.GetName(j));
                        }

                        while (reader.Read())
                        {

                            //name: カラム名, type: データ型
                            var obj = new Object[reader.FieldCount];
                            var v = reader.GetValues(obj);

                            //データ
                            i += 1;
                            for (var k = 0; k < reader.FieldCount; k++)
                            {
                                WriteCell(sheet, k, i, obj[k].ToString());
                            }

                            _isExists = true;

                        }
                    }
                    conn.Close();
                    conn.Dispose();
                    context.Database.CloseConnection();


                }
                //ブックを保存
                if (_isExists)
                {
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        book.Write(fs);
                        book.Close();
                    }
                    return filePath;

                }
                else
                {
                    book.Close();

                    return null;
                }
            }
            catch (Exception ex)
            {
                

                Console.WriteLine(ex);
                return null;
            }
        }
        //ブック作成
        protected static IWorkbook CreateNewBook(string filePath)
        {
            IWorkbook book;
            var extension = Path.GetExtension(filePath);

            // HSSF => Microsoft Excel(xls形式)(excel 97-2003)
            // XSSF => Office Open XML Workbook形式(xlsx形式)(excel 2007以降)
            if (extension == ".xls")
            {
                book = new HSSFWorkbook();
            }
            else if (extension == ".xlsx")
            {
                book = new XSSFWorkbook();
            }
            else
            {
                throw new ApplicationException("CreateNewBook: invalid extension");
            }

            return book;
        }
        //セル値
        protected static String ReadCell(ISheet sheet, int columnIndex, int rowIndex)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
            String cellValue = null;

            if (cell != null)
            {
                if (cell.CellType == NPOI.SS.UserModel.CellType.String)
                {
                    cellValue = cell.StringCellValue;

                }
                else if (cell.CellType == NPOI.SS.UserModel.CellType.Blank)
                {
                    cellValue = cell.StringCellValue;

                }
                else if (cell.CellType == NPOI.SS.UserModel.CellType.Numeric)
                {
                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                    {
                        cellValue = cell.DateCellValue.ToString();
                    }
                    else
                    {
                        cellValue = cell.NumericCellValue.ToString();

                    }

                }
            }

            return cellValue;
        }


        //セル設定(文字列用)
        protected static void WriteCell(ISheet sheet, int columnIndex, int rowIndex, string value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        //セル設定(数値用)
        protected static void WriteCell(ISheet sheet, int columnIndex, int rowIndex, double value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        //セル設定(日付用)
        protected static void WriteCell(ISheet sheet, int columnIndex, int rowIndex, DateTime value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        //書式変更
        protected static void WriteStyle(ISheet sheet, int columnIndex, int rowIndex, ICellStyle style)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.CellStyle = style;
        }

        /// <summary>
        /// メッセージヘッダのためのRFC2047形式の文字列に変換する（Base64）
        /// </summary>
        /// <param name="str">変換もとの文字列</param>
        /// <param name="enc">エンコーディング</param>
        /// <returns></returns>
        protected string EncodeMailHeader(string str, System.Text.Encoding enc)
        {
            //Base64でエンコードする
            string ret = System.Convert.ToBase64String(enc.GetBytes(str));
            //RFC2047形式に
            ret = string.Format("=?{0}?B?{1}?=", enc.BodyName, ret);
            return ret;
        }



        /// <summary>
        /// LoadExcel
        /// </summary>
        /// <param name="filefullName"></param>
        /// <returns></returns>
        protected Dictionary<string, object> LoadExcel(string filefullName)
        {


            //パラメータチェック
            if (filefullName == null)
            {
                return null;
            }

            //主処理
            var currentPath = Directory.GetCurrentDirectory();
            var tableName = "";

            _context.Database.OpenConnection();
            var conn = _context.Database.GetDbConnection();

            //エクセルファイルを開く
            IWorkbook book = null;
            var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
            try
            {
                //ブック読み込み
                book = WorkbookFactory.Create(filefullName);
                var sheet = book.GetSheetAt(0);
                tableName = sheet.SheetName;
                var columnList = new List<VueMSFramework.ViewModels.Table.ColumnList>();

                //カラムリスト
                using (var command = conn.CreateCommand())
                {
                    //テーブル情報取得
                    command.CommandText = "PRAGMA TABLE_INFO('" + tableName + "');";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //name: カラム名, type: データ型
                            var cl = new VueMSFramework.ViewModels.Table.ColumnList
                            {
                                ColumnId = reader["cid"].ToString(),
                                ColumnName = reader["name"].ToString(),
                                DataType = reader["type"].ToString()
                            };
                            columnList.Add(cl);
                        }
                    }
                }
                var i = 0;
                var rowIndex = 0;
                var headeInfo = new Dictionary<string, int>();
                for (; ; )
                {
                    try
                    {
                        //1行目はヘッダの情報
                        if (rowIndex == 0)
                        {
                            var colIndex = 0;
                            for (; ; )
                            {

                                if (ReadCell(sheet, colIndex, rowIndex).Equals(string.Empty))
                                {

                                    break;
                                }
                                else
                                {   //カラム列と名のマッピングリスト
                                    headeInfo.Add(ReadCell(sheet, colIndex, rowIndex), colIndex);
                                }

                                colIndex++;
                            }
                        }
                        else
                        {
                            //1列目が未設定の場合は、データの終了と判断する
                            if (ReadCell(sheet, 0, rowIndex).Equals(string.Empty))
                            {

                                break;
                            }
                            else
                            {

                                //2行目以降はデータ
                                var dynamicColumnObject = new ExpandoObject() as IDictionary<string, Object>;
                                foreach (var col in columnList)
                                {

                                    if (headeInfo.ContainsKey(col.ColumnName))
                                    {
                                        var ret = ReadCell(sheet, headeInfo[col.ColumnName], rowIndex);
                                        dynamicColumnObject.Add(col.ColumnName, ret);

                                    }
                                    else
                                    {
                                        dynamicColumnObject.Add(col.ColumnName, "");
                                    }
                                }
                                dynamicObject.Add((i++).ToString(), dynamicColumnObject);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("エラー:" + ex.Message);
                        return null;
                    }
                    rowIndex++;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (book != null)
                {
                    book.Close();
                    book = null;

                }
            }
            var obj = new Dictionary<string, object>
            {
                { "TableName", tableName },
                { "RowDataList", dynamicObject.Values.ToList() }
            };
            return obj;
        }

        protected void RepaceData(Dictionary<string, object> param)
        {

            var TableName = (string)param["TableName"];
            var RowDataList = param["RowDataList"];


            //メイン処理
            _context.Database.OpenConnection();
            var conn = _context.Database.GetDbConnection();
            try
            {
                using (var command = conn.CreateCommand())
                {
                    //テーブルバックアップ
                    command.CommandText = "CREATE TABLE " + TableName + "_" + DateTime.Now.ToString("yyMMddHHmm") + " AS SELECT * FROM " + TableName + ";";
                    command.ExecuteNonQuery();

                    command.CommandText = "delete from " + TableName;
                    command.ExecuteNonQuery();

                    //カラムリスト
                    //employeeテーブル情報取得
                    command.CommandText = "PRAGMA TABLE_INFO('" + TableName + "');";

                    var columnList = new Dictionary<string, VueMSFramework.ViewModels.Table.ColumnList>();

                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //name: カラム名, type: データ型
                                var cl = new VueMSFramework.ViewModels.Table.ColumnList
                                {
                                    ColumnId = reader["cid"].ToString(),
                                    ColumnName = reader["name"].ToString(),
                                    DataType = reader["type"].ToString()
                                };
                                columnList.Add(cl.ColumnName, cl);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }


                    foreach (var r in (List<object>)RowDataList)
                    {

                        string vals = "";
                        string keys = "";
                        foreach (KeyValuePair<string, object> kvp in (IDictionary<string, object>)r)
                        {
                            var col = kvp.Key;
                            var val = kvp.Value ?? "";

                            //型の判断
                            var c = columnList[col];
                            switch (c.DataType)
                            {
                                case "TEXT":
                                    if (val.ToString().Length == 0)
                                    {
                                        //vals += ",null";
                                    }
                                    else
                                    {
                                        keys += "," + col;
                                        vals += ",'" + val.ToString() + "'";

                                    }
                                    break;
                                case "INTEGER":
                                    if (val.ToString().Length == 0)
                                    {
                                        //  vals += ",null" ;
                                    }
                                    else
                                    {
                                        keys += "," + col;
                                        vals += "," + int.Parse(val.ToString()).ToString();

                                    }
                                    break;
                                case "REAL":
                                    if (val.ToString().Length == 0)
                                    {
                                        //  vals += ",null";
                                    }
                                    else
                                    {
                                        keys += "," + col;
                                        vals += "," + Double.Parse(val.ToString()).ToString();

                                    }
                                    break;
                                case "BOOL":
                                    if (val.ToString().Length == 0)
                                    {
                                        //  vals += ",null";
                                    }
                                    else
                                    {
                                        keys += "," + col;
                                        vals += "," + Boolean.Parse(val.ToString()).ToString();

                                    }
                                    break;
                                default:
                                    if (val.ToString().Length == 0)
                                    {
                                        //  vals += ",null";
                                    }
                                    else
                                    {
                                        keys += "," + col;
                                        vals += ",'" + val + "'";

                                    }
                                    break;
                            }
                        }
                        try
                        {
                            command.CommandText = "insert into " + TableName + "(" + keys.Substring(1) + ") values (" + vals.Substring(1) + ")";
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }
    }
}
