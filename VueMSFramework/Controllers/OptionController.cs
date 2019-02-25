using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models;
using VueMSFramework.ViewModels.Option;

namespace Vue2Spa.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : PageController
    {
        public OptionController(ApplicationDbContext context, IStringLocalizer<OptionController> localizer) : base(context, localizer)
        {
        }

        // Post api/Option/OptionSearch
        //[Authorize]
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
                var list = (from a in _context.Options
                            where (param.Keywords.Contains(a.Field) 
                            || param.Keywords.Contains(a.Text) 
                            || param.Keywords.Contains(a.Value) ||  String.IsNullOrEmpty(param.Keywords))
                            orderby a.OrderBy ascending
                            select new OptionList
                            {
                                OptionId = a.OptionId,
                                Field = a.Field,
                                Text = a.Text,
                                Value = a.Value,
                                OrderBy = a.OrderBy,
                                Color = a.Color,
                                IconUrl = a.IconUrl,
                                
                            }).ToList();


                if (list == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                param.OptionList = list;
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);

            }
        }

        //[Authorize]
        [HttpPost(), Route("create/load")]
        public ActionResult<Create> Load([FromBody]Create param)
        {
            if (param == null)
            {
                param = new Create();
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
        //[Authorize]
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

                if (param.OptionId == null)
                {
                    param._message = _localizer["The key parameter has not been set yet."];
                    return BadRequest(param);
                }
                var model = (from a in _context.Options
                             where a.OptionId.Equals(param.OptionId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(model);
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

        //[Authorize]
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

                if (param.OptionId == null)
                {
                    param._message = _localizer["The key parameter has not been set yet."];
                    return BadRequest(param);
                }

                var Option = (from a in _context.Options
                             where a.OptionId.Equals(param.OptionId)
                             select a).FirstOrDefault();

                if (Option == null)
                {
                    param._message = _localizer["Data is not registered."];
                    return BadRequest(param);
                }

                ReflectionUtility.Model2Model(Option, param);

  
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

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
                var storeModel = new Option();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.OptionId = Guid.NewGuid().ToString();
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Options.Add(storeModel);
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

        //[Authorize]
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
                var storeModel = (from a in _context.Options
                                  where a.OptionId.Equals(param.OptionId)
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
                _context.Options.Update(storeModel);
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

        // DELETE api/Option/id
        //[Authorize]
        [HttpPost(), Route("remove/delete")]
        public ActionResult Delete(Remove param)
        {

            if (param == null)
            {
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
                var model = (from a in _context.Options
                         where a.OptionId.Equals(param.OptionId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["Data is not registered."];
                    return BadRequest(param);
                }



                _context.Options.Remove(model);
                _context.SaveChanges();
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
