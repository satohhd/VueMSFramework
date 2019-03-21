using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models;
using VueMSFramework.ViewModels.Activity;

namespace VueMSFramework.Controllers
{
    [Route("api/[controller]")]
    public class ActivityController : PageController<ActivityController>
    {
        public ActivityController(ApplicationDbContext context, IStringLocalizer<ActivityController> localizer) : base(context, localizer)
        {
        }



        // Post api/activity/search/load
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

                var list = (from a in _context.Activities
                            where a.Title.Contains(param.Keywords) || String.IsNullOrEmpty(param.Keywords)
                            orderby a.Updated descending
                            select new ActivityList {
                                ActivityId = a.ActivityId,
                                Title = a.Title,
                                Content = a.Content,
                                Category = a.Category,
                            }
                            ).ToList();

                param.ActivityList = list;
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

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
                param = new Create();
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }
        // Post api/activity/edit/load
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
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
            //if (jsonStr != null)
            //{
            //    var p = JsonConvert.DeserializeObject<Edit>(jsonStr);
            //    param.ActivityId = p.ActivityId;
            //}


            if (param.ActivityId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }



            try
            {
                var model = (from a in _context.Activities
                             where a.ActivityId.Equals(param.ActivityId)
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


            ////JWT→抽出
            //var jsonStr = DecodeJwt(param._sectionParam, User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
            //if (jsonStr == null)
            //{
            //    param._message = _localizer["System error Please inform system personnel.({0})", _localizer["We could not restore it."]];
            //    return BadRequest(param);
            //}
            //if (jsonStr != null)
            //{
            //    var p = JsonConvert.DeserializeObject<Delete>(jsonStr);
            //    //パラメータ項目
            //    param.ActivityId = p.ActivityId;
            //}

            if (param.ActivityId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Activities
                             where a.ActivityId.Equals(param.ActivityId)
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



        // POST api/activitiy/create/insert
        //[Authorize]
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

            var context = new ValidationContext(param, null, null);
            var result = new List<ValidationResult>();

            if (!Validator.TryValidateObject(param, context, result, true))
            {
                param._message = _localizer["parameter invalid"];
                return BadRequest(param);
            }
            try
            {
                var storeModel = new Activity();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.ActivityId = (Guid.NewGuid()).ToString();
                storeModel.Owner = HttpContext.User.Identity.Name;
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Activities.Add(storeModel);
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

        // PUT api/activitiy/edit/update
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
                var model = (from a in _context.Activities
                             where a.ActivityId.Equals(param.ActivityId)
                             select a).FirstOrDefault();


                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                ReflectionUtility.Model2Model(param, model);
                model.Updated = DateTime.Now;
                model.Version += 1;
                _context.Activities.Update(model);
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

        // DELETE api/activitiy/remove/delete
        [HttpPost(), Route("remove/delete")]
        public ActionResult Delete(Remove param)
        {
            if (param == null) { 
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Activities
                         where a.ActivityId.Equals(param.ActivityId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                _context.Activities.Remove(model);
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
