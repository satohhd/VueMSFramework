using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models.Keicho;
using VueMSFramework.ViewModels.Keicho;

namespace Vue2Spa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class KeichoController : PageController
    {
        public KeichoController(ApplicationDbContext context, IStringLocalizer<KeichoController> localizer) : base(context, localizer)
        {
        }

        /**
         *  GetGiveList
         *  param 
         */
        private List<GiveList> GetGiveList(Search param)
        {
            try
            {
                //件数を取得する
                var count = (from a in _context.Keichos
                             where a.KeichoClass.Equals("GIVE")
                             where (a.Title.Contains(param.Keywords) || a.Notes.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                             select a).Count();

                //ページ情報
                var pagination = param.Tabs.Tab2.GIvePager;
                pagination.RecordCount = count;

                var list = (from a in _context.Keichos
 
                            where a.KeichoClass.Equals("GIVE")
                            where (a.Title.Contains(param.Keywords) || a.Notes.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                            orderby a.KeichoDate ascending
                            select new GiveList
                            {
                                KeichoId = a.KeichoId,

                                Title = a.Title,
                                //KeichoTypeId = a.KeichoTypeId,
                                KeichoTypeName = (from b in _context.KeichoTypes
                                                  where b.KeichoTypeId.Equals(a.KeichoTypeId)
                                                  select b.KeichoTypeName).FirstOrDefault(),
                                GiveUserName = a.GiveUserName,
                                TakeUserName = a.TakeUserName,
                                Money = a.Money,
                                KeichoDate = a.KeichoDate == null ? null : ((DateTime)a.KeichoDate).ToString("yyyy-MM-dd"),
                                Notes = a.Notes

                            }).Skip(pagination.RecordsPerPage * (pagination.CurrentPageNumber - 1)).Take(pagination.RecordsPerPage).ToList<GiveList>();
                return list;

            }
            catch (Exception ex)
            {
                throw new Exception("検索エラー:" + ex.Message);
            }
        }
        /**
         *  GetTakeList
         *  param 
         */
        private List<TakeList> GetTakeList(Search param)
        {

            try
            {
                var v = param;

                //件数を取得する
                var count = (from a in _context.Keichos

                             where a.KeichoClass.Equals("TAKE")
                             where (a.Title.Contains(param.Keywords) || a.Notes.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                             select a).Count();


                //ページ情報
                var pagination = v.Tabs.Tab2.GIvePager;
                pagination.RecordCount = count;

                var list = (from a in _context.Keichos
 
                            where a.KeichoClass.Equals("TAKE")
                            where (a.Title.Contains(param.Keywords) || a.Notes.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                            orderby a.KeichoDate ascending
                            select new TakeList
                            {
                                KeichoId = a.KeichoId,

                                Title = a.Title,
                                //KeichoTypeId = a.KeichoTypeId,
                                KeichoTypeName = (from b in _context.KeichoTypes
                                                  where b.KeichoTypeId.Equals(a.KeichoTypeId)
                                                  select b.KeichoTypeName).FirstOrDefault(),
                                GiveUserName = a.GiveUserName,
                                TakeUserName = a.TakeUserName,
                                Money = a.Money,
                                KeichoDate = a.KeichoDate == null ? null : ((DateTime)a.KeichoDate).ToString("yyyy-MM-dd"),
                                Notes = a.Notes

                            }).Skip(pagination.RecordsPerPage * (pagination.CurrentPageNumber - 1)).Take(pagination.RecordsPerPage).ToList<TakeList>();
                return list;

            }
            catch (Exception ex)
            {
                throw new Exception("検索エラー:" + ex.Message);
            }
        }


        // Post api/keicho/search/load
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
                param.Tabs.Tab1.TakeList = GetTakeList(param);
                param.Tabs.Tab2.GiveList = GetGiveList(param);
                System.Threading.Thread.Sleep(500);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        // Post api/keicho/create/load
        [HttpPost(), Route("create/load")]
        public ActionResult<Create> Load([FromBody]Create param)
        {
            if (param == null)
            {
                param = new Create();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            return Ok(param);

        }
        // Post api/keicho/edit
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {

            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            try
            {

                if (param.KeichoId == null)
                {
                    param._message = _localizer["The key parameter has not been set yet."];
                    return BadRequest(param);
                }

                var model = (from a in _context.Keichos

                              where a.KeichoId.Equals(param.KeichoId)
                              select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                ReflectionUtility.Model2Model(model, param);

                System.Threading.Thread.Sleep(500);

                return Ok(param);


            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }



        }
        // Post api/keicho/delete
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {
            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            try
            {

                if (param.KeichoId == null)
                {
                    param._message = _localizer["The key parameter has not been set yet."];
                    return BadRequest(param);
                }

                var model = (from a in _context.Keichos
                             where a.KeichoId.Equals(param.KeichoId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                ReflectionUtility.Model2Model(model, param);

                System.Threading.Thread.Sleep(500);

                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }


        }

        // POST api/keicho/create/insert
        [HttpPost(), Route("create/insert")]
        public ActionResult Insert([FromBody] Create param)
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
                var storeModel = new Keicho();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.KeichoId = (Guid.NewGuid()).ToString();
                storeModel.Owner = HttpContext.User.Identity.Name;
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Keichos.Add(storeModel);
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

        // PUT api/keicho/edit/update
        [HttpPost(), Route("edit/update")]
        public ActionResult Update([FromBody] Edit param)
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
                var model = (from a in _context.Keichos
                                  where a.KeichoId.Equals(param.KeichoId)
                                  select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                if (model.Version != param.Version)
                {
                    return Conflict("他の人が更新済みです。");
                }

                ReflectionUtility.Model2Model(param, model);
                model.Updated = DateTime.Now;
                model.Version += 1;


                //更新
                _context.Keichos.Update(model);
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

        // DELETE api/keicho/remove/delete
        [HttpPost(), Route("remove/delete")]
        public ActionResult Delete([FromBody] Remove param)
        {

            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }


            try
            {
                var model = (from a in _context.Keichos
                         where a.KeichoId.Equals(param.KeichoId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                _context.Keichos.Remove(model);
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


    }
}
