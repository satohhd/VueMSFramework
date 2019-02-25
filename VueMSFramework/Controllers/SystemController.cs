using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VueMSFramework.Data;
using VueMSFramework.ViewModels.System;

namespace Vue2Spa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : PageController
    {
        public SystemController(ApplicationDbContext context, IStringLocalizer<SystemController> localizer) : base(context, localizer)
        {
        }

        // Post api/fukuri/setting/load
        [HttpPost(), Route("setting/load")]
        public ActionResult<Setting> Load([FromBody]Setting param)
        {
            if (param == null)
            {
                param = new Setting();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);

            }
        }
        // Post api/fukuri/setting/load
        [HttpPost(), Route("database/load")]
        public ActionResult<Database> Load([FromBody]Database param)
        {
            if (param == null)
            {
                param = new Database();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);

            }
        }

        //// GET api/system/{tablename}/erase
        //[HttpGet, Route("{tablename}/erase")]
        //public ActionResult Erase(string tablename)
        //{
        //    try
        //    {
        //        SeedData.Erase(_context, tablename);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        var param = new Index();
        //        param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
        //        return BadRequest(param);
        //    }


        //}

        [HttpPost(), Route("migrate")]
        public ActionResult<Database> Migrate([FromBody]Database param)
        {
            try
            {
                var fileList = new List<string>();

                _context.Database.OpenConnection();
                var conn = _context.Database.GetDbConnection();


                var result = _context.Tables.FromSql("select name from sqlite_master where type='table'").ToList();
                foreach (var r in result)
                {

                    //テーブル名に日付がついたもの除く
                    if (Regex.IsMatch(r.Name, "^.+_[0-9]+$"))
                    {
                        continue;
                    }

                    //ストレージにEXCELファイルで退避
                    var exposrtFileFullPath = ExportTableData(r.Name, _context);
                    if (exposrtFileFullPath != null)
                    {
                        fileList.Add(exposrtFileFullPath);

                    }

                }

                result.Clear();
                conn.Close();
                conn.Dispose();
                _context.Database.CloseConnection();


                //データベースの再作成
                try
                {
                    SeedData.Erase(_context);
                    SeedData.Make(_context);
                }
                catch (Exception ex)
                {
                    param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                    return BadRequest(param);
                }

                foreach (var f in fileList)
                {
                    //既にファイルが読み込み済みの場合
                    var tableData = LoadExcel(f);
                    RepaceData(tableData);
                }
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);

            }

        }

        // GET api/system/table/create
        [Authorize]
        [HttpPost(), Route("tablecreate")]
        public ActionResult<Database> TableCreate([FromBody]Database param)
        {
            try
            {
                SeedData.Make(_context);
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }


        }



        // GET api/system/tabledrop
        [Authorize]
        [HttpGet, Route("tabledrop")]
        public ActionResult<Database> TableDrop([FromBody]Database param)
        {
            try
            {
                SeedData.Erase(_context);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);

            }
        }


    }
}
