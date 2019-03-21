using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models.Auth;
using VueMSFramework.ViewModels.Auth;

namespace VueMSFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : PageController<AuthController>
    {
        public AuthController(ApplicationDbContext context, IStringLocalizer<AuthController> localizer) : base(context, localizer)
        {
        }

        // Post api/auth/confirm
        [HttpGet("confirm/{id}")]
        public IActionResult Confirm(string id)
        {
            var param = new SignIn();
            if (id == null)
            {
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                //ログインユーザの取得
                var user = (from a in _context.Accounts
                            where a.EmailConfirmeKey.Equals(id)
                            select a).SingleOrDefault();

                if (user == null)
                {
                    return RedirectToAction("Defect", "Home");
                }

                if (user.EmailConfirmed)
                {
                    //既に確認済みです。
                    return RedirectToAction("Confirmed", "Home");
                }

                user.EmailConfirmed = true;
                user.ManagerConfirmed = true;
                user.Updated = DateTime.Now;
                user.Version += 1;
                user.Updater = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                _context.Accounts.Update(user);
                _context.SaveChanges();

                return RedirectToAction("Confirmed", "Home");

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("signUp")]
        public ActionResult<SignUp> Load([FromBody] SignUp param)
        {
            if (param == null)
            {
                param = new SignUp();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
 
            return Ok(param);

   
        }

        [HttpPost(), Route("signUp/request")]
        public ActionResult<SignUp> Insert([FromBody] SignUp param)
        {
            if (param == null)
            {
                param = new SignUp();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            param.TermAddr = "0:0:0:0"; //TODO今後端末単位の管理をするなら
            param.RemoteAddr = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();


            //if (!TryValidateModel(param))
            //{
            //    param._message = _localizer["The input is incorrect."];
            //    return BadRequest(param);
            //}

            try
            {

                //既にチケットがある場合は、データベースに存在するか確認しあれば中身チェック

                //ログインユーザの取得
                var acount = (from a in _context.Accounts
                              where a.Email.Equals(param.Email)
                              where a.TermAddr.Equals(param.TermAddr)
                              where a.RemoteAddr.Equals(param.RemoteAddr)
                              select a).FirstOrDefault();

                //データが取得できた場合　中身を確認する
                if (acount != null)
                {
                    //未確認アカウントの場合は削除
                    if (!acount.EmailConfirmed)
                    {
                        _context.Accounts.Remove(acount);
                        _context.SaveChanges();
                    }
                    else
                    {
                        param._message = _localizer["Account has already been registered."]; ;
                        return BadRequest(param);

                    }

                    ////期限をチェック
                    ////前のユーザを破棄して、再発行する
                    //_context.Accounts.Remove(acount);
                    //_context.SaveChanges();
                    //

                }

                // チケットがない場合
                var account = new Account();
                try
                {
                    ReflectionUtility.Model2Model(param, account);

                    account.AccountId = Guid.NewGuid().ToString();
                    account.UserName = account.UserName ?? account.Email.Split("@")[0];
                    account.EmailConfirmeKey = Guid.NewGuid().ToString();
                    account.Owner = HttpContext.User.Identity.Name ?? param.UserName;
                    account.Registed = DateTime.Now;
                    account.Updated = DateTime.Now;
                    account.Version = 1;

                    //登録
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    //発行
                    var ticket = CreateJwtSecurityToken(account);
                    account.Ticket = new JwtSecurityTokenHandler().WriteToken(ticket);
                    _context.Accounts.Update(account);
                    _context.SaveChanges();
                    //認証済みということで返却
                    ReflectionUtility.Model2Model(account, param);

                }
                catch (Exception ex)
                {
                    param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                    return BadRequest(param);
                }

                try
                {
                    var callbackUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/api/auth/confirm/" + account.EmailConfirmeKey;

                    //本人確認メール送信
                    var mail = new MailCreate();
                    mail.FromEmail = "info@tobicrun.jp";
                    mail.ToEmail = param.Email;
                    mail.Sender = _localizer["システム管理者"];
                    mail.Subject = _localizer["本人確認メールです。"];
                    mail.Body = _localizer["If you use the system please click this URL. {0} ", callbackUrl];

                    if (SendMail(mail))
                    {
                        param._message = _localizer["We sent a person confirmation mail. Please SignIn after approval"];

                    }
                    else
                    {
                        param._message = _localizer["Please check that the specified mail is correct."];
                    };

                    return Ok(param);

                }
                catch (Exception ex)
                {
                    param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                    return BadRequest(param);
                }


            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }


        [HttpPost(), Route("edit/load")]
        [Authorize]
        public ActionResult<Edit> Load([FromBody] Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            //認証ユーザ
            var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            if (id != param.AccountId)
            {
                param._message = _localizer["Unauthorized access"];
                return BadRequest(param);
            }

            try
            {
                //ログインユーザの取得
                var model = (from a in _context.Accounts
                             where a.AccountId.Equals(id)
                             select a).SingleOrDefault();

                //データが取得できた場合　中身を確認する
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
        [HttpPost(), Route("refer/load")]
        //[Authorize]
        public ActionResult<Refer> Load([FromBody]Refer param)
        {
            if (param == null)
            {
                param = new Refer();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            //認証ユーザ
            var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            param.AccountId = id;
            //if (id != param.AccountId)
            //{
            //    param._message = _localizer["Unauthorized access"];
            //    return BadRequest(param);
            //}

            try
            {
                //ログインユーザの取得
                var model = (from a in _context.Accounts
                             where a.AccountId.Equals(param.AccountId)
                             select a).SingleOrDefault();

                //データが取得できた場合　中身を確認する
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
        [HttpPost(), Route("signIn")]
        public ActionResult<SignIn> Load([FromBody]SignIn param)
        {

            if (param == null)
            {
                param = new SignIn();
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

    

        [HttpPost(), Route("signIn/execute")]
         public ActionResult<SignIn> SingIn([FromBody]SignIn param)
        {
            if (param == null)
            {
                param = new SignIn();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            param.TermAddr = "0:0:0:0"; //TODO今後端末単位の管理をするなら
            param.RemoteAddr = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            //if (!TryValidateModel(param))
            //{
            //    param._message = _localizer["The input is incorrect"];
            //    return BadRequest(param);
            //}

            if (param.Email == null || param.TermAddr == null || param.RemoteAddr == null || param.Password == null)
            {
                param._message = _localizer["The input is incorrect"];
                return BadRequest(param);
            }
            try
            {   //ログインユーザの取得
                var act = (from a in _context.Accounts
                            where a.Email.Equals(param.Email.Trim())
                            where a.TermAddr.Equals(param.TermAddr.Trim())
                            where a.RemoteAddr.Equals(param.RemoteAddr.Trim())
                            select a).FirstOrDefault();

                //該当する条件に一致しない
                if (act == null)
                {
                    param._message = _localizer["Your identity is unregistered."];
                    param.Authorized = false;
                    param.Applying = false;
                    param._errorRedirect = new VueMSFramework.Core.Event() {Page="auth",Section="create", Action="create/load",ParamItems= "email,termAddr,remoteAddr" };
                    return BadRequest(param);

                }
                //データが取得できた場合　中身を確認する
                else if (!act.EmailConfirmed)
                {
                    param._message = _localizer["Your identity is being confirmed."];
                    param.Authorized = false;
                    param.Applying = true;
                    return BadRequest(param);

                }
                //管理者確認待ち
                else if (!act.ManagerConfirmed)
                {

                    param._message = _localizer["The administrator is checking it."];
                    param.Authorized = false;
                    param.Applying = true;
                    return BadRequest(param);

                }
                //使用禁止
                else if (act.LockoutEnabled)
                {

                    param._message = _localizer["Your identity is locked."];
                    param.Authorized = false;
                    param.Applying = true;
                    return BadRequest(param);

                }
                else if (!param.Password.Equals(act.Password))
                {
                    param._message = _localizer["Pssword or Account bad."];
                    param.Authorized = false;
                    param.Applying = false;
                    return BadRequest(param);
                }
                //本人確認済みであること
                else if (act.EmailConfirmed && act.ManagerConfirmed)
                {
                    //
                    act.AccessCount += 1;
                    act.Updated = DateTime.Now;
                    act.Updater = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                    _context.Accounts.Update(act);

                    _context.SaveChanges();

                    ReflectionUtility.Model2Model(act, param);

                    param.Authorized = true;
                    param.Applying = false;
                    return Ok(param);
                }
                else
                {
                    param._message = _localizer["unknown"];
                    param.Authorized = false;
                    param.Applying = false;
                    return BadRequest(param);
                }
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                param.Authorized = false;
                param.Applying = false;
                return BadRequest(param);
            }
        }
 
        private bool SendMail(MailCreate param)
        {
            try
            {
     
                var sc = new System.Net.Mail.SmtpClient
                {
                    //SMTPサーバーを指定する
                    //Host = "XXXXXX", // or "mailgate.ipentec.com";
                    Host = "XXXXXX",
                    Port = 587,   // or 587;
                                  //string smtpserver = "XXXXXX"; 
                                  //int port = 587;

                    //ユーザー名とパスワードを設定する
                    Credentials = new System.Net.NetworkCredential(param.FromEmail, "XXXXXX"),
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,

                    //現在は、EnableSslがtrueでは失敗する
                    EnableSsl = true
                };
                var msg = new System.Net.Mail.MailMessage
                {
                    From = new System.Net.Mail.MailAddress(param.FromEmail,param.Sender),
                    IsBodyHtml = false,
                    Subject = param.Subject,
                    Body = param.Body,
                };
                msg.To.Add(new System.Net.Mail.MailAddress(param.ToEmail));

                //メッセージを送信する
                sc.Send(msg);
                //後始末
                msg.Dispose();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
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
        //[HttpPost(), Route("signOut/load")]
        //[Authorize]
        //public ActionResult<SignOut> Load([FromBody] SignOut param)
        //{
        //    if (param == null)
        //    {
        //        param = new SignOut();
        //        param._message = _localizer["No parameters."];
        //        return BadRequest(param);
        //    }
        //    try
        //    {
        //        //認証ユーザ
        //        var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
        //        param.AccountId = id;
        //        param.Message = _localizer["Signing out..."];
        //        return Ok(param);

        //    }
        //    catch (Exception ex)
        //    {
        //        param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
        //        return BadRequest(param);
        //    }

        //}

        //[HttpPost(), Route("signOut")]
        //public ActionResult<SignOut> SignOut([FromBody]SignOut param)
        //{
        //    if (param == null)
        //    {
        //        param = new SignOut();
        //        param._message = _localizer["No parameters."];
        //        return BadRequest(param);
        //    }
        //    param.Message = _localizer["Signed out"];
        //    return Ok(param);

        //}

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            var user = _context.Accounts.First(u => u.AccountId == id);
            return Ok(
            new {
                user.AccountId,
                user.UserName,
                _Message = "接続検証"
            });
        }
    }
}
