using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models.kintai;
using VueMSFramework.ViewModels.Shain;

namespace VueMSFramework.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ShainController : PageController<ShainController>
    {
        public ShainController(ApplicationDbContext context, IStringLocalizer<ShainController> localizer) : base(context, localizer)
        {
        }

        // Post api/shain/search/load
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

                var list = (from a in _context.Shains
                            where (a.Shmi.Contains(param.Keywords) || a.Shinbng.Contains(param.Keywords) || a.Bmnmi.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                            orderby a.Ymgn ascending
                            select new SearchList
                            {
                                ShainId = a.ShainId,
                                Shinbng = a.Shinbng,
                                Shmi = a.Shmi,
                                YmgnIchimoji = a.Ymgn == null ? "" : a.Ymgn.Substring(0, 1),
                                Bmnmi = a.Bmnmi,
                                Kytnmi = a.Kytnmi

                            }).ToList();


                param.SearchList = list;

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }


        // Post api/shain/create/load
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
        // Post api/shain/edit
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.ShainId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }

            try
            {
                var model = (from a in _context.Shains
   
                                where a.ShainId.Equals(param.ShainId)
                                select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(model, param);


                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }
        // Post api/shain/remove/load
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {
            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.ShainId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Shains

                             where a.ShainId.Equals(param.ShainId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(model, param);


                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }


        // POST api/shain/create/insert
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
                var storeModel = new Shain();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.ShainId = (Guid.NewGuid()).ToString();
                storeModel.Owner = HttpContext.User.Identity.Name;
                 storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Shains.Add(storeModel);
                _context.SaveChanges();
                ReflectionUtility.Model2Model(storeModel,param );
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
      }

        // PUT api/shain/edit/update
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
                var storeModel = (from a in _context.Shains
                                  where a.ShainId.Equals(param.ShainId)
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
                _context.Shains.Update(storeModel);
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

        // DELETE api/shain/remove/delete
        [HttpPost(), Route("remove/delete")]
        public ActionResult Delete([FromBody] Remove param)
        {

            if (param == null)
            {
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Shains
                         where a.ShainId.Equals(param.ShainId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                _context.Shains.Remove(model);
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
