using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models;
using VueMSFramework.ViewModels.Exam;

namespace Vue2Spa.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : PageController
    {
        public ExamController(ApplicationDbContext context, IStringLocalizer<ExamController> localizer) : base(context, localizer)
        {
        }

        /**
         *  ExamList
         *  param 
         */
        private List<ExamList> GetExamList(Search param)
        {

            if (param.CategoryIds == null)
            {
                param.CategoryIds = new String[0];
            }

            try
            {
                //件数を取得する
                var count = (from a in _context.Exams
                             where ((from b in _context.ExamCategories
                                     where param.CategoryIds.Contains(b.CategoryId)
                                     select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                             where a.ExamineeId.Equals(param.ExamineeId)
                             select a).Count();

                //検索条件
                var t = param.Tabs.Tab1;
                t.ExamineeId = param.ExamineeId;
                t.Keywords = param.Keywords;


                //ページ情報
                var pagination = param.Tabs.Tab1.Pagination;
                pagination.RecordCount = count;

                var list = (from a in _context.Exams
                            where ((from b in _context.ExamCategories
                                    where param.CategoryIds.Contains(b.CategoryId)
                                    select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                            where a.ExamineeId.Equals(param.ExamineeId)
                            where ((a.ExamName.Contains(param.Keywords) || a.Memo.Contains(param.Keywords) || a.Memo.Contains(param.Keywords)) || String.IsNullOrEmpty(param.Keywords))
                            orderby a.ExamDate descending
                            select new ExamList
                            {
                                ExamId = a.ExamId,
                                ExamName = a.ExamName,
                                ExamineeId = a.ExamineeId,

                                ExamDate = a.ExamDate == null ? null : ((DateTime)a.ExamDate).ToString("yyyy-MM-dd"),
                                KokugoScore = a.KokugoScore,
                                ShakaiScore = a.ShakaiScore,
                                RikaScore = a.RikaScore,
                                SugakuScore = a.SugakuScore,
                                EigoScore = a.EigoScore,

                                KokugoAveScore = a.KokugoAveScore,
                                ShakaiAveScore = a.ShakaiAveScore,
                                RikaAveScore = a.RikaAveScore,
                                SugakuAveScore = a.SugakuAveScore,
                                EigoAveScore = a.EigoAveScore,

                            }).Skip(pagination.RecordsPerPage * (pagination.CurrentPageNumber - 1)).Take(pagination.RecordsPerPage).ToList<ExamList>();
                return list;

            }
            catch (Exception ex)
            {
                throw new Exception("検索エラー:" + ex.Message);
            }
        }
        // Post api/exam/search
        [HttpPost(), Route("select/load")]
        public ActionResult<Select> GetExamSelect([FromBody]Select param)
        {

            if (param == null)
            {
                param = new Select();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {

                var list = (from a in _context.Exams
                            group a by a.ExamineeId into cgroup
                            select new ExamineeList
                            {
                                ExamineeId = cgroup.Key
                            }).ToList();

                param.ExamineeList = list;
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }


        // Post api/exam/search
        [HttpPost(), Route("search/load")]
        public ActionResult<Search> GetExamSearch([FromBody]Search param)
        {


            if (param == null)
            {
                param = new Search();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (param.ExamineeId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }


            try
            {


                //オプション情報取得
                if (param.ExamineeId == null)
                {
                    param.Options["categoryIds"] = null;

                }
                else {

                    var options = (from a in _context.Categories
                                   where a.ExamineeId.Equals(param.ExamineeId)
                                   orderby a.OrderBy ascending
                                   select new
                                   {
                                       Value = a.CategoryId,
                                       Text = a.CategoryName
                                   }).ToList();

                    param.Options["categoryIds"] = options;


                }

                //一覧
                param.Tabs.Tab1.DataList = GetExamList(param);


                var titleList = (from a in _context.Exams
                                 where ((from b in _context.ExamCategories
                                         where param.CategoryIds.Contains(b.CategoryId)
                                         select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                 where a.ExamineeId.Equals(param.ExamineeId)
                                 orderby a.ExamDate ascending
                                 select a.ExamName).ToList();

                var valueList = (from a in _context.Exams
                                 where ((from b in _context.ExamCategories
                                         where param.CategoryIds.Contains(b.CategoryId)
                                         select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                 where a.ExamineeId.Equals(param.ExamineeId)
                                 orderby a.ExamDate ascending
                                 select (int)a.KokugoScore + (int)a.RikaScore + (int)a.ShakaiScore + (int)a.EigoScore + (int)a.SugakuScore
                                 ).ToList();

                var ave_valueList = (from a in _context.Exams
                                     where ((from b in _context.ExamCategories
                                             where param.CategoryIds.Contains(b.CategoryId)
                                             select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                     where a.ExamineeId.Equals(param.ExamineeId)
                                     orderby a.ExamDate ascending
                                     select (int)a.KokugoAveScore + (int)a.RikaAveScore + (int)a.ShakaiAveScore + (int)a.EigoAveScore + (int)a.SugakuAveScore
                                 ).ToList();

                var ko_valueList = (from a in _context.Exams
                                    where ((from b in _context.ExamCategories
                                            where param.CategoryIds.Contains(b.CategoryId)
                                            select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                    where a.ExamineeId.Equals(param.ExamineeId)
                                    orderby a.ExamDate ascending
                                    select (int)a.KokugoScore
                                 ).ToList();

                var ave_ko_valueList = (from a in _context.Exams
                                        where ((from b in _context.ExamCategories
                                                where param.CategoryIds.Contains(b.CategoryId)
                                                select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                        where a.ExamineeId.Equals(param.ExamineeId)
                                        orderby a.ExamDate ascending
                                        select (int)a.KokugoAveScore
                                 ).ToList();

                var ri_valueList = (from a in _context.Exams
                                    where ((from b in _context.ExamCategories
                                            where param.CategoryIds.Contains(b.CategoryId)
                                            select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                    where a.ExamineeId.Equals(param.ExamineeId)
                                    orderby a.ExamDate ascending
                                    select (int)a.RikaScore
                                 ).ToList();

                var ave_ri_valueList = (from a in _context.Exams
                                        where ((from b in _context.ExamCategories
                                                where param.CategoryIds.Contains(b.CategoryId)
                                                select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                        where a.ExamineeId.Equals(param.ExamineeId)
                                        orderby a.ExamDate ascending
                                        select (int)a.RikaAveScore
                                 ).ToList();

                var sh_valueList = (from a in _context.Exams
                                    where ((from b in _context.ExamCategories
                                            where param.CategoryIds.Contains(b.CategoryId)
                                            select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                    where a.ExamineeId.Equals(param.ExamineeId)
                                    orderby a.ExamDate ascending
                                    select (int)a.ShakaiScore
                                 ).ToList();

                var ave_sh_valueList = (from a in _context.Exams
                                        where ((from b in _context.ExamCategories
                                                where param.CategoryIds.Contains(b.CategoryId)
                                                select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                        where a.ExamineeId.Equals(param.ExamineeId)
                                        orderby a.ExamDate ascending
                                        select (int)a.ShakaiAveScore
                                 ).ToList();


                var ei_valueList = (from a in _context.Exams
                                    where ((from b in _context.ExamCategories
                                            where param.CategoryIds.Contains(b.CategoryId)
                                            select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                    where a.ExamineeId.Equals(param.ExamineeId)
                                    orderby a.ExamDate ascending
                                    select (int)a.EigoScore
                                  ).ToList();

                var ave_ei_valueList = (from a in _context.Exams
                                        where ((from b in _context.ExamCategories
                                                where param.CategoryIds.Contains(b.CategoryId)
                                                select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                        where a.ExamineeId.Equals(param.ExamineeId)
                                        orderby a.ExamDate ascending
                                        select (int)a.EigoAveScore
                  ).ToList();

                var su_valueList = (from a in _context.Exams
                                    where ((from b in _context.ExamCategories
                                            where param.CategoryIds.Contains(b.CategoryId)
                                            select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                    where a.ExamineeId.Equals(param.ExamineeId)
                                    orderby a.ExamDate ascending
                                    select (int)a.SugakuScore
                                  ).ToList();

                var ave_su_valueList = (from a in _context.Exams
                                        where ((from b in _context.ExamCategories
                                                where param.CategoryIds.Contains(b.CategoryId)
                                                select b.ExamId).Contains(a.ExamId) || param.CategoryIds.Count().Equals(0))
                                        where a.ExamineeId.Equals(param.ExamineeId)
                                        orderby a.ExamDate ascending
                                        select (int)a.SugakuAveScore
                                    ).ToList();

                //総合計
                param.Tabs.Tab2.Option = new HigthChartOption
                {
                    Title = new Title { Text = "総合計" },
                    Subtitle = new Title { Text = "５教科総合計グラフです" },
                    XAxis = new XAxis { Categories = titleList.ToArray() },
                    YAxis = new YAxis
                    {
                        PlotLines = new PlotLine[] { new PlotLine { Value = 0, Color = "#808080", Width = 1 } },
                        Max = 500,
                        Title = new Title { Text = "採点" }
                    },
                    Tooltip = new Tooltip { ValueSuffix = "点" },
                    Series = new Sery[] { new Sery { Data = valueList, Name = "合計", Type = "column" },
                                             new Sery { Data = ave_valueList, Name = "平均", Type = "line" } },
                };

                //国語
                param.Tabs.Tab3.Option = new HigthChartOption
                {
                    Title = new Title { Text = "国語" },
                    Subtitle = new Title { Text = "国語グラフです" },
                    XAxis = new XAxis { Categories = titleList.ToArray() },
                    YAxis = new YAxis
                    {
                        PlotLines = new PlotLine[] { new PlotLine { Value = 0, Color = "#808080", Width = 1 } },
                        Max = 100,
                        Title = new Title { Text = "採点" }
                    },
                    Tooltip = new Tooltip { ValueSuffix = "点" },
                    Series = new Sery[] { new Sery { Data = ko_valueList ,Name = "国語",Type= "column" } ,
                                         new Sery { Data = ave_ko_valueList, Name = "平均", Type = "line" } },



                };

                //理科
                param.Tabs.Tab4.Option = new HigthChartOption
                {
                    Title = new Title { Text = "理科" },
                    Subtitle = new Title { Text = "理科グラフです" },
                    XAxis = new XAxis { Categories = titleList.ToArray() },
                    YAxis = new YAxis
                    {
                        PlotLines = new PlotLine[] { new PlotLine { Value = 0, Color = "#808080", Width = 1 } },
                        Max = 100,
                        Title = new Title { Text = "採点" }
                    },
                    Tooltip = new Tooltip { ValueSuffix = "点" },
                    Series = new Sery[] { new Sery { Data = ri_valueList, Name = "理科", Type = "column" } ,
                                             new Sery { Data = ave_ri_valueList, Name = "平均", Type = "line" } },
                };

                //社会
                param.Tabs.Tab5.Option = new HigthChartOption
                {
                    Title = new Title { Text = "社会" },
                    Subtitle = new Title { Text = "社会グラフです" },
                    XAxis = new XAxis { Categories = titleList.ToArray() },
                    YAxis = new YAxis
                    {
                        PlotLines = new PlotLine[] { new PlotLine { Value = 0, Color = "#808080", Width = 1 } },
                        Max = 100,
                        Title = new Title { Text = "採点" }
                    },
                    Tooltip = new Tooltip { ValueSuffix = "点" },
                    Series = new Sery[] { new Sery { Data = sh_valueList, Name = "社会", Type = "column" } ,
                                             new Sery { Data = ave_sh_valueList, Name = "平均", Type = "line" } },
                };

                //数学
                param.Tabs.Tab6.Option = new HigthChartOption
                {
                    Title = new Title { Text = "数学" },
                    Subtitle = new Title { Text = "数学グラフです" },
                    XAxis = new XAxis { Categories = titleList.ToArray() },
                    YAxis = new YAxis
                    {
                        PlotLines = new PlotLine[] { new PlotLine { Value = 0, Color = "#808080", Width = 1 } },
                        Max = 100,
                        Title = new Title { Text = "採点" }
                    },
                    Tooltip = new Tooltip { ValueSuffix = "点" },
                    Series = new Sery[] { new Sery { Data = su_valueList, Name = "数学", Type = "column" } ,
                                             new Sery { Data = ave_su_valueList, Name = "平均", Type = "line" } },
                };

                //英語
                param.Tabs.Tab7.Option = new HigthChartOption
                {
                    Title = new Title { Text = "英語" },
                    Subtitle = new Title { Text = "英語グラフです" },
                    XAxis = new XAxis { Categories = titleList.ToArray() },
                    YAxis = new YAxis
                    {
                        PlotLines = new PlotLine[] { new PlotLine { Value = 0, Color = "#808080", Width = 1 } },
                        Max = 100,
                        Title = new Title { Text = "採点" }
                    },
                    Tooltip = new Tooltip { ValueSuffix = "点" },
                    Series = new Sery[] { new Sery { Data = ei_valueList, Name = "英語", Type = "column" },
                                             new Sery { Data = ave_ei_valueList, Name = "平均", Type = "line" } },
                };

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        // Post api/exam/create/load
        [HttpPost(), Route("create/load")]
        public ActionResult<Create> GetCreate([FromBody]Create param)
        {

            if (param == null)
            {
                param = new Create();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            ////JWT→抽出
            //var jsonStr = DecodeJwt(param._sectionParam, User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            //if (jsonStr == null)
            //{
            //    param._message = _localizer["System error Please inform system personnel.({0})", _localizer["We could not restore it."]];
            //    return BadRequest(param);
            //}
            //var p = JsonConvert.DeserializeObject<Create>(jsonStr);

            //パラメータ項目
            //model.ExamId = param.ExamId;
            //param.ExamineeId = p.ExamineeId;

            //var vm = new Create();
            //vm.ExamineeId = model.ExamineeId;


            //オプション情報取得
            var options = (from a in _context.Categories
                           where a.ExamineeId.Equals(param.ExamineeId)
                           orderby a.OrderBy ascending
                           select new
                           {
                               Value = a.CategoryId,
                               Text = a.CategoryName
                           }).ToList();


            param.Options["categoryIds"] = options;



            return Ok(param);

        }
        // Post api/exam/edit
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> GetEdit([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.ExamId == null || param.ExamineeId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var exam = (from a in _context.Exams
                            where a.ExamId.Equals(param.ExamId)
                            select a).FirstOrDefault();

                if (exam == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);

                }

                ReflectionUtility.Model2Model(exam, param);

                var item = (from b in _context.ExamCategories
                            where b.ExamId.Equals(param.ExamId)
                            select b.CategoryId).ToArray();

                param.CategoryIds = item;

                //オプション情報取得
                var options = (from a in _context.Categories
                               where a.ExamineeId.Equals(param.ExamineeId)
                               orderby a.OrderBy ascending
                               select new
                               {
                                   Value = a.CategoryId,
                                   Text = a.CategoryName
                               }).ToList();


                param.Options["categoryIds"] = options;


                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }
        // Post api/exam/remove/load
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {
            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.ExamId == null || param.ExamineeId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var exam = (from a in _context.Exams

                            where a.ExamId.Equals(param.ExamId)
                            select a).FirstOrDefault();

                if (exam == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);

                }

                ReflectionUtility.Model2Model(exam, param);
                var item = (from b in _context.ExamCategories
                            where b.ExamId.Equals(param.ExamId)

                            select b.CategoryId).ToArray();

                param.CategoryIds = item;

                //オプション情報取得
                var options = (from a in _context.Categories
                               where a.ExamineeId.Equals(param.ExamineeId)
                               orderby a.OrderBy ascending
                               select new
                               {
                                   Value = a.CategoryId,
                                   Text = a.CategoryName
                               }).ToList();


                param.Options["categoryIds"] = options;

                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }
        // Post api/exam/categoryCrud
        [HttpPost(), Route("category/load")]
        public ActionResult<CategoryCrud> Load([FromBody]CategoryCrud param)
        {
            if (param == null)
            {
                param = new CategoryCrud();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            //オプション情報取得
            var options = (from a in _context.Categories
                           where a.ExamineeId.Equals(param.ExamineeId)
                           orderby a.OrderBy ascending
                           select new { a.CategoryId, a.CategoryName }).ToList();

            param.Options["categoryIds"] = options;

            return Ok(param);

        }

        // Post api/exam/categoryCrud
        [HttpPost(), Route("category/edit")]
        public ActionResult<CategoryCrud> Edit([FromBody]CategoryCrud param)
        {
            if (param == null)
            {
                param = new CategoryCrud();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
   
            //パラメータ項目
            param.CategoryId = null;
            param.CategoryName = null;
            param.OrderBy = null;
            //param.ExamineeId = p.ExamineeId;

            //リスト表示
            var list = (from a in _context.Categories
                        where a.ExamineeId.Equals(param.ExamineeId)
                        orderby a.OrderBy
                        select new CategoryList
                        {
                            CategoryId = a.CategoryId,
                            CategoryName = a.CategoryName,
                            ExamineeId = a.ExamineeId,
                            OrderBy = a.OrderBy,
                        }
                        ).ToList();

            if (list == null)
            {
                param._message = _localizer["It has already been deleted."];
                return BadRequest(param);

            }
            param.CategoryList = list;

    
            //オプション情報取得
            var options = (from a in _context.Categories
                           where a.ExamineeId.Equals(param.ExamineeId)
                           orderby a.OrderBy ascending
                           select new { a.CategoryId, a.CategoryName }).ToList();

            param.Options["categoryIds"] = options;

            return Ok(param);

        }

  

        [HttpPost(), Route("category/store")]
        public ActionResult<CategoryCrud> Store([FromBody]CategoryCrud param)
        {
            if (param == null)
            {
                param = new CategoryCrud();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (!TryValidateModel(param))
            {
                param._message = _localizer["The input is incorrect."];

                return BadRequest(param);
            }

            //新規の場合
            if (param.CategoryId == null || param.CategoryId.Length == 0) 
            {

                var storeModel = new Category();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.CategoryId = (Guid.NewGuid()).ToString();
                storeModel.Owner = HttpContext.User.Identity.Name;
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Categories.Add(storeModel);
                _context.SaveChanges();

                ReflectionUtility.Model2Model(storeModel, param);
            }
            else
            {
                var storeModel = (from a in _context.Categories
                                  where a.CategoryId.Equals(param.CategoryId)
                                  select a).FirstOrDefault();

                if (storeModel == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);

                }

                storeModel.CategoryName = param.CategoryName;
                storeModel.ExamineeId = param.ExamineeId;
                storeModel.OrderBy = param.OrderBy;
                storeModel.Updated = DateTime.Now;
                storeModel.Version += 1;

                //登録
                _context.Categories.Update(storeModel);
                _context.SaveChanges();

                ReflectionUtility.Model2Model(storeModel, param);
            }

            //オプション情報取得
            var options = (from a in _context.Categories
                           where a.ExamineeId.Equals(param.ExamineeId)
                           orderby a.OrderBy ascending
                           select new
                           {
                               Value = a.CategoryId,
                               Text = a.CategoryName
                           }).ToList();


            param.Options["categoryIds"] = options;


            return Ok(param);
        }

        // DELETE api/exam/category/id
        [HttpPost() ,Route("category/delete")]
        public ActionResult<CategoryCrud> Delete(CategoryCrud param)
        {
            if (param == null)
            {
                param = new CategoryCrud();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Categories
                         where a.CategoryId.Equals(param.CategoryId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);

                }

                _context.Categories.Remove(model);
                _context.SaveChanges();


                ReflectionUtility.Model2Model(model, param);
                return Ok(param);


            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("create/insert")]
        public ActionResult<Create> Insert([FromBody] Create param)
        {

            if (param == null)
            {
                param = new Create();
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
                var storeModel = new Exam();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.ExamId = (Guid.NewGuid()).ToString();
                //storeModel.ExamCategoryIds = string.Join(",", model.ExamCategoryIds);

                storeModel.Owner = HttpContext.User.Identity.Name;
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Exams.Add(storeModel);
                //_context.SaveChanges();

                if (param.CategoryIds != null) { 
                    foreach (var r in param.CategoryIds)
                    {
                        var m = new ExamCategory
                        {
                            ExamCategoryId = (Guid.NewGuid()).ToString(),
                            ExamId = storeModel.ExamId,
                            CategoryId = r.ToString(),
                            Owner = HttpContext.User.Identity.Name,
                            Registed = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1
                        };

                        //登録
                        _context.ExamCategories.Add(m);

                    }
                }
                _context.SaveChanges();

                ReflectionUtility.Model2Model(storeModel, param);
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }

        [HttpPost(), Route("edit/update")]
        public ActionResult<Edit> Update([FromBody] Edit param)
        {
            if (param == null)
            {
                param = new Edit();
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
                //更新前データを取得する
                var storeModel = (from a in _context.Exams
                                  where a.ExamId.Equals(param.ExamId)
                                  select a).FirstOrDefault();


                if (storeModel == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                if (storeModel.Version != param.Version)
                {
                    param._message = _localizer["Other people have been updated."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(param, storeModel);

                storeModel.Updated = DateTime.Now;
                storeModel.Version += 1;
                //更新
                _context.Exams.Update(storeModel);
                //_context.SaveChanges();

                //更新前データを取得する
                var ec = from a in _context.ExamCategories
                          where a.ExamId.Equals(param.ExamId)
                          select a;
                //削除
                _context.ExamCategories.RemoveRange(ec);

                if (param.CategoryIds != null)
                {
                    foreach (var r in param.CategoryIds)
                    {
                        var m = new ExamCategory
                        {
                            ExamCategoryId = (Guid.NewGuid()).ToString(),
                            ExamId = storeModel.ExamId,
                            CategoryId = r.ToString(),
                            Owner = HttpContext.User.Identity.Name,
                            Registed = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1
                        };

                        //登録
                        _context.ExamCategories.Add(m);

                    }
                }
                _context.SaveChanges();
                ReflectionUtility.Model2Model(storeModel, param);

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
