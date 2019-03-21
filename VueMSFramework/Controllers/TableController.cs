using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using VueMSFramework.Data;
using VueMSFramework.ViewModels.Table;

namespace VueMSFramework.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TableController : PageController<TableController>
    {
        public TableController(ApplicationDbContext context, IStringLocalizer<TableController> localizer) : base(context, localizer)
        {
        }

        [HttpPost(), Route("search/load")]
        public ActionResult<Search> Load([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var result = _context.Tables.FromSql("select name from sqlite_master where type='table'").ToList();
                var tableList = new List<SearchList>();
                foreach (var r in result)
                {
                    tableList.Add(new SearchList { TableId = r.Name, TableName = r.Name });
                }
                param.SearchList = tableList;
                System.Threading.Thread.Sleep(200);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("import/load")]
        public ActionResult<Import> Load([FromBody]Import param)
        {
            if (param == null)
            {
                param = new Import();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {

                //既にファイルが読み込み済みの場合
                LoadExcel(param.FileName);
                //System.Threading.Thread.Sleep(500);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("refere/load")]
        public ActionResult<Refere> Load([FromBody]Refere param)
        {
            if (param == null)
            {
                param = new Refere();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (param.TableId == null)
            {
                param._message = _localizer["key argument does not contain required information."];
                return BadRequest(param);
            }
            try
            {
                _context.Database.OpenConnection();
                var conn = _context.Database.GetDbConnection();
                var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
                var dynamicColumnObjectOneRow = new ExpandoObject() as IDictionary<string, Object>;


                //カラムリスト
                using (var command = conn.CreateCommand())
                {
                    //employeeテーブル情報取得
                    command.CommandText = "PRAGMA TABLE_INFO('" + param.TableId + "');";

                    var columnList = new List<ColumnList>();

                    using (var reader = command.ExecuteReader())
                    {
                         while (reader.Read())
                        {
                            //name: カラム名, type: データ型
                            var cl = new ColumnList
                            {
                                ColumnId = reader["cid"].ToString(),
                                ColumnName = reader["name"].ToString(),
                                DataType = reader["type"].ToString()
                            };
                            columnList.Add(cl);

                            dynamicColumnObjectOneRow.Add(reader["name"].ToString(), "");

                        }


                    }
                    param.TableName = param.TableId;
                    param.Tabs.Tab2.ColumnList = columnList;
                }

                //データリスト
                using (var command = conn.CreateCommand())
                {
                    //employeeテーブル情報取得
                    command.CommandText = "select * from " + param.TableId + ";";
  

                    using (var reader = command.ExecuteReader())
                    {
                        var i = 0;
                        while (reader.Read())
                        {

                            //name: カラム名, type: データ型
                            var obj = new Object[reader.FieldCount];
                            var v = reader.GetValues(obj);
                            var dynamicColumnObject = new ExpandoObject() as IDictionary<string, Object>;
                            for (var j = 0; j < reader.FieldCount;j++)
                            {
                                dynamicColumnObject.Add(reader.GetName(j), obj[j]);

                            }
                            //KeyItemの追加
                            //dynamicColumnObject.Add("KeyItem", "rowDataId");
                            //dynamicColumnObject.Add("RowDataId", i);
                            dynamicObject.Add((i++).ToString(), dynamicColumnObject);
                        }

                        if (i == 0)
                        {
                            dynamicObject.Add((i++).ToString(), dynamicColumnObjectOneRow);
                        }
                    }
                    param.Tabs.Tab1.RowDataList = dynamicObject.Values.ToList();

                }
                System.Threading.Thread.Sleep(200);
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
            finally
            {
                _context.Database.CloseConnection();
            }

        }

        [HttpPost(), Route("import/execute")]
        public ActionResult<Import> Execute([FromBody]Import param)
        {
            if (param == null)
            {
                param = new Import();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (!TryValidateModel(param))
            {
                param._message = _localizer["The input is incorrect."];
                return BadRequest(param);
            }
            try
            {
                //既にファイルが読み込み済みの場合
                var info = new Dictionary<string, object>();
                info["TableName"] = param.TableId;
                info["RowDataList"] = param.RowDataList;
                base.RepaceData(info);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }
        [HttpGet(), Route("{table}/export")]
        public IActionResult Export(string table)
        {
            if (table == null)
            {
                var param = new Index();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fileName = table + ".xlsx";
            var filePath = Path.GetTempFileName();
            try
            {
                //ブック作成
                var book = CreateNewBook(fileName);

                //シート無しのexcelファイルは保存は出来るが、開くとエラーが発生する
                var sheet = book.CreateSheet(table);


                //データリスト
                _context.Database.OpenConnection();
                var conn = _context.Database.GetDbConnection();
                using (var command = conn.CreateCommand())
                {
                    //employeeテーブル情報取得
                    command.CommandText = "select * from " + table + ";";

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

                        }
                    }

                }
                //ブックを保存
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    book.Write(fs);
                    book.Close();
                }
            }
            catch (Exception ex)
            {
                var param = new Index();
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }


            var file = System.IO.File.ReadAllBytes(filePath);
  
            //return Ok(base64);
            return Ok(File(file, "application/vnd.ms-excel", fileName));

        }
    }
}
