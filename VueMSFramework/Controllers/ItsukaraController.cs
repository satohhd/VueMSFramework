using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models.Itsukara;
using VueMSFramework.ViewModels.Itsukara;

namespace Vue2Spa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ItsukaraController : PageController
    {
        public ItsukaraController(ApplicationDbContext context, IStringLocalizer<ItsukaraController> localizer) : base(context, localizer)
        {
        }
        // Post api/itsukara/search/load
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
                var list = (from a in _context.Itsukaras
                            where (a.Notes.Contains(param.Keywords) || a.Goods.Contains(param.Keywords) || a.Std.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                            orderby a.ExpirationDate descending
                            select new ItsukaraList
                            {
                                ItsukaraId = a.ItsukaraId,
                                Goods = a.Goods,
                                Std = a.Std,
                                //ExpirationDate = a.ExpirationDate == null ? null: ((DateTime)a.ExpirationDate).ToString("yyyy-MM-dd"),
                                PurchaseDate = a.PurchaseDate == null ? null : ((DateTime)a.PurchaseDate).ToString("yyyy-MM-dd"),
                                Notes = a.Notes
                            }).ToList();


                param.ItsukaraList = list;
                //System.Threading.Thread.Sleep(2000);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        // Post api/itsukara/create/load
        [HttpPost(), Route("create/load")]
        public ActionResult<Create> Load([FromBody]Create param)
        {
            if (param == null)
            {
                param = new Create();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            //System.Threading.Thread.Sleep(2000);
            return Ok(param);
        }
     
        //Post api/itsukara/edit/load
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
 

            if (param.ItsukaraId == null)
            {
                param._message = _localizer["Section'sURL argument does not contain required information."];
                return BadRequest(param);
            }

            try
            {
                var model = (from a in _context.Itsukaras
                             where a.ItsukaraId.Equals(param.ItsukaraId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(model, param);

               // System.Threading.Thread.Sleep(2000);
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }
        // Post api/itsukara/remove/load
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {
            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.ItsukaraId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }

            try
            {
                var model = (from a in _context.Itsukaras
                             where a.ItsukaraId.Equals(param.ItsukaraId)
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

        // POST api/itsukara/create/insert
        [HttpPost, Route("create/insert")]
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
                var storeModel = new Itsukara();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.ItsukaraId = (Guid.NewGuid()).ToString();
                storeModel.Owner = HttpContext.User.Identity.Name;
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                _context.Itsukaras.Add(storeModel);
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

        // POST api/itsukara/edit/update
        [HttpPost, Route("edit/update")]
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
                var storeModel = (from a in _context.Itsukaras
                                    where a.ItsukaraId.Equals(param.ItsukaraId)
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
                _context.Itsukaras.Update(storeModel);
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

        // POST api/itsukara/remove/delete
        [HttpPost, Route("remove/delete")]
        public ActionResult<Remove> Delete([FromBody] Remove param)
        {
            if (param == null)
            {
                param = new Remove();
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
                var model = (from a in _context.Itsukaras
                                where a.ItsukaraId.Equals(param.ItsukaraId)
                                select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                if (model.Version != param.Version)
                {
                    param._message = _localizer["Other people have been updated."];
                    return BadRequest(param);
                }

                ReflectionUtility.Model2Model(model, param);

                var del = new ItsukaraDel();
                ReflectionUtility.Model2Model(model, del);
                del.Deleter = HttpContext.User.Identity.Name;
                _context.ItsukaraDels.Add(del);
                _context.SaveChanges();


                //更新
                _context.Itsukaras.Remove(model);
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
