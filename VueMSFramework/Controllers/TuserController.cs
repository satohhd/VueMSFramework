using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.IO;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models;
using VueMSFramework.ViewModels.Tuser;

namespace VueMSFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuserController : PageController<TuserController>
    {
        public TuserController(ApplicationDbContext context, IStringLocalizer<TuserController> localizer) : base(context, localizer)
        {
        }

        // Post api/tuser/Search
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
                var list = (from a in _context.Tusers
                            where a.TuserName.Contains(param.Keywords) || a.TuserKana.Contains(param.Keywords) || a.ToEmail.Contains(param.Keywords) || String.IsNullOrEmpty(param.Keywords)
                            orderby a.OrderBy ascending
                            select new TuserList
                            {
                                TuserId = a.TuserId,
                                TuserName = a.TuserName,
                                ToEmail = a.ToEmail,
                                ToEmail2 = a.ToEmail2,
                                ToEmail3 = a.ToEmail3,
                                Tel = a.Tel,
                                Address = a.Address,
                                IsSecretariat = a.IsSecretariat
                            }).ToList();


                param.TuserList = list;

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        // Post api/tuser/create/load
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
        // Post api/tuser/Edit
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (param.TuserId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }


            try
            {
                var model = (from a in _context.Tusers
                             where a.TuserId.Equals(param.TuserId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["No parameters."];
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
        // Post api/tuser/remove/load
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {
            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (param.TuserId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Tusers
                             where a.TuserId.Equals(param.TuserId)
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


        // POST api/tuser
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
                var storeModel = new Tuser();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.TuserId = (Guid.NewGuid()).ToString();
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Tusers.Add(storeModel);
                _context.SaveChanges();

                Console.WriteLine("OK");
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }

        // PUT api/tuser/id
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
                var storeModel = (from a in _context.Tusers
                                  where a.TuserId.Equals(param.TuserId)
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
                storeModel.TuserName = param.TuserName;

                storeModel.Updated = DateTime.Now;
                storeModel.Version += 1;


                //更新
                _context.Tusers.Update(storeModel);
                _context.SaveChanges();
                ReflectionUtility.Model2Model(storeModel,param);

                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);

            }
        }

        // DELETE api/tuser/id
        [HttpPost(), Route("remove/delete")]
        public ActionResult<Remove> Delete([FromBody]Remove param)
        {

            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Tusers
                            where a.TuserId.Equals(param.TuserId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                //削除
                _context.Tusers.Remove(model);
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
