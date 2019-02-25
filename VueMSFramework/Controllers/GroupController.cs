//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models;
using VueMSFramework.ViewModels.Group;

namespace Vue2Spa.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : PageController
    {
        public GroupController(ApplicationDbContext context, IStringLocalizer<GroupController> localizer) : base(context, localizer)
        {
        }

        // Post api/group/search/load
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
                var list = (from a in _context.Groups
                            //where a.GroupName.Contains(model.Keywords)
                            orderby a.OrderBy ascending
                            select new GroupList
                            {
                                GroupId = a.GroupId,
                                GroupName = a.GroupName,
                                OrderBy = a.OrderBy,
                                Color = a.Color,
                                IconUrl = a.IconUrl,
                                
                            }).ToList();

                param.GroupList = list;
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
                var userlist = (from a in _context.Tusers
                            orderby a.OrderBy ascending
                            select new GroupUserList
                         {
                             IsSelect = (from b in _context.GroupTusers
                                         where b.TuserId == a.TuserId
                                         where b.GroupId == param.GroupId
                                         select b).FirstOrDefault() == null?false:true,
                             TuserId = a.TuserId,
                             TuserName = a.TuserName,
                             TuserKana = a.TuserKana,
                             ToEmail = a.ToEmail,
                             GroupId = param.GroupId
                         }).ToList();


                param.SelectList = userlist;

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }
        [Authorize]
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
                if (param.GroupId == null)
                {
                    param._message = _localizer["The key parameter has not been set yet."];
                    return BadRequest(param);
                }


                var model = (from a in _context.Groups
                             where a.GroupId.Equals(param.GroupId)
                             select a).FirstOrDefault();


                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(model, param);

                var userlist = ( from a in _context.Tusers
                                join b in _context.GroupTusers
                                on new { a.TuserId , param.GroupId} equals new { b.TuserId,b.GroupId} into gt
                             orderby a.OrderBy ascending
                             select new GroupUserList
                             {
                                 IsSelect = gt.Count() == 0 ? false : true,
                                 TuserId = a.TuserId,
                                 TuserName = a.TuserName,
                                 TuserKana = a.TuserKana,
                                 ToEmail = a.ToEmail,
                                 GroupId = param.GroupId
                             }).ToList();

                param.SelectList = userlist;

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

                if (param.GroupId == null)
                {
                    param._message = _localizer["The key parameter has not been set yet."];
                    return BadRequest(param);
                }

                var model = (from a in _context.Groups
                             where a.GroupId.Equals(param.GroupId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(model, param);

                var tuser = (from a in _context.GroupTusers
                             join b in _context.Tusers
                             on a.TuserId equals b.TuserId
                             
                             where a.GroupId.Equals(param.GroupId)
                             orderby a.GroupId
                             select new GroupUserList
                             {
                                 TuserId = a.TuserId,
                                 TuserName = b.TuserName,
                                 TuserKana = b.TuserKana,
                                 ToEmail = b.ToEmail,
                                 ToEmail2 = b.ToEmail2,
                                 ToEmail3 = b.ToEmail3,
                             }).ToList();

                param.SelectList = tuser;

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
                var storeModel = new Group();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.GroupId = Guid.NewGuid().ToString();
                //model.PurchaseDate = ((DateTime)model.PurchaseDate).D;
                //model.ExpirationDate = ((DateTime)model.ExpirationDate).Date;


                //model.SatusId = "stage1";
                //model.Owner = Context.User.Identity.Name;
                //storeModel.Type = "select";
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Groups.Add(storeModel);

                //グループユーザを登録する。
                foreach (var user in param.SelectList)
                {
                    if (user.IsSelect)
                    {
                        var gt = new GroupTuser();
                        //ReflectionUtility.Model2Model(user, gt);
                        //gt.GroupTuserId = Guid.NewGuid().ToString();
                        
                        gt.GroupId = storeModel.GroupId;
                        gt.TuserId = user.TuserId;

                        gt.Registed = DateTime.Now;
                        gt.Owner = HttpContext.User.Identity.Name;
                        gt.Updated = DateTime.Now;
                        gt.Updater = HttpContext.User.Identity.Name;
                        gt.Version = 1;

                        _context.GroupTusers.Add(gt);
                    }
                }

                _context.SaveChanges();
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
        public ActionResult<Edit> ActionResult([FromBody] Edit param)
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
                var storeModel = (from a in _context.Groups
                                  where a.GroupId.Equals(param.GroupId)
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
                storeModel.GroupName = param.GroupName;

                storeModel.Updated = DateTime.Now;
                storeModel.Version += 1;


                //更新
                _context.Groups.Update(storeModel);

                //検索
                var list = (from a in _context.GroupTusers 
                                  where a.GroupId.Equals(param.GroupId)
                                  select a).ToList();

                _context.GroupTusers.RemoveRange(list);

                //グループユーザを登録する。
                foreach (var user in param.SelectList)
                {
                    if (user.IsSelect)
                    {
                        var gt = new GroupTuser();
                        ReflectionUtility.Model2Model(user, gt);

                        gt.Registed = DateTime.Now;
                        gt.Updated = DateTime.Now;
                        gt.Version = 1;

                        _context.GroupTusers.Add(gt);
                    }
                }


                _context.SaveChanges();
                return Ok(param);


            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        // DELETE api/group/remove/delete
        //[Authorize]
        [HttpPost(), Route("remove/delete")]
        public ActionResult<Remove> Delete([FromBody]Remove param)
        {
            if (param == null)
            {
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Groups
                         where a.GroupId.Equals(param.GroupId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                _context.Groups.Remove(model);
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
