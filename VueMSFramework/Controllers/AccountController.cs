using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models.Auth;
using VueMSFramework.ViewModels.Account;

namespace VueMSFramework.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : PageController<AccountController>
    {
        public AccountController(ApplicationDbContext context, IStringLocalizer<AccountController> localizer) : base(context, localizer)
        {
        }


        // Post api/account/search/load
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


                var list = (from a in _context.Accounts
                            where a.Email.Contains(param.Keywords) || a.UserName.Contains(param.Keywords)  || String.IsNullOrEmpty(param.Keywords)
                            orderby a.Email ascending
                            select new SearchList
                            {
                                AccountId = a.AccountId,
                                Email = a.Email,
                                UserName = a.UserName,
                                TermAddr = a.TermAddr,
                                RemoteAddr = a.RemoteAddr,
                                EmailConfirmed = a.EmailConfirmed,
                                LockoutEnabled = a.LockoutEnabled,
                                AccessCount = a.AccessCount,
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


        // Post api/account/create/load
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
        // Post api/account/edit
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }


            if (param.AccountId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Accounts
                                where a.AccountId.Equals(param.AccountId)
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
        // Post api/account/delete
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {
            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.AccountId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }

            try
            {
                var account = (from a in _context.Accounts
                             where a.AccountId.Equals(param.AccountId)
                             select a).FirstOrDefault();

                if (account == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(account);
                }
                ReflectionUtility.Model2Model(account, param);

                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }

        // POST api/account
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
                var storeModel = new Account();
                ReflectionUtility.Model2Model(param, storeModel);
                storeModel.AccountId = (Guid.NewGuid()).ToString();
                storeModel.Owner = HttpContext.User.Identity.Name;
                storeModel.Registed = DateTime.Now;
                storeModel.Updated = DateTime.Now;
                storeModel.Version = 1;

                //登録
                _context.Accounts.Add(storeModel);
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
                var storeModel = (from a in _context.Accounts
                                  where a.AccountId.Equals(param.AccountId)
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
                _context.Accounts.Update(storeModel);
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

        // DELETE api/account/remove/delete
        [HttpPost(), Route("remove/delete")]
        public ActionResult Delete(string id)
        {
            var param = new Remove();
            if (id == null)
            {
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            try
            {
                var model = (from a in _context.Accounts
                         where a.AccountId.Equals(id)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }

                model.Updated = DateTime.Now;
                model.Version += 1;
                _context.Accounts.Remove(model);
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
