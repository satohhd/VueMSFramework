using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

namespace Vue2Spa.Controllers
{

    public class  Param
    {
        public string Lang { get; set; }
        public string ReturnUrl { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : PageController
    {
        public MenuController(ApplicationDbContext context, IStringLocalizer<MenuController> localizer) : base(context, localizer)
        {
        }


        //[HttpPost(), Route("setLanguage")]
        //public IActionResult SetLanguage([FromBody] param para)
        //{
        //    Response.Cookies.Append(
        //        CookieRequestCultureProvider.DefaultCookieName,
        //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(para.Lang)),
        //        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        //    );

        //    return LocalRedirect(para.ReturnUrl);
        //}


        // Post api/auth/confirm
        [HttpGet("confirm/{id}")]
        public IActionResult Confirm(string id)
        {
            var param = new Index();
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


        // Post api/auth/index
        [HttpPost(), Route("index/load")]
        public ActionResult<Index> Index([FromBody]Index param)
        {
            if (param == null)
            {
                param = new Index();
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

        // Post api/auth/create
        [HttpPost(), Route("create/load")]
        public ActionResult<SignUp> CreateLoad([FromBody] SignUp param)
        {
            if (param == null)
            {
                param = new SignUp();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            try
            {
                param.TermAddr = "0:0:0:0";
                param.RemoteAddr = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }


        // Post api/auth/edit
        [HttpPost(), Route("edit/load")]
        [Authorize]
        public ActionResult<Edit> InitEdit([FromBody] Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }


            //認証ユーザ
            var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
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

        // Post api/auth/refer
        [HttpPost(), Route("refer/load")]
        [Authorize]
        public ActionResult<Refer> InitRefer([FromBody] Refer param)
        {
            if (param == null)
            {
                param = new Refer();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            //認証ユーザ
            var id = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
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
        // Post api/auth/signIn
        [HttpPost(), Route("signIn/load")]
        
        public ActionResult<SignIn> InitSignIn([FromBody]SignIn param)
        {

            if (param == null)
            {
                param = new SignIn();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                param = new SignIn
                {
                    //Email = p.Email,
                    TermAddr = "0:0:0:0",
                    RemoteAddr = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString()
                };

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }
        [HttpPost("signIn")]
        public IActionResult SingIn([FromBody]SignIn param)
        {

            if (param == null)
            {
                param = new SignIn();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (!TryValidateModel(param))
            {
                param._message = _localizer["入力に誤りがあります"];
                return BadRequest(param);
            }

            Account acount = null;
            if (param.Ticket == null)
            {
                //承認後かもしれないので
                //ログインユーザの取得
                acount = (from a in _context.Accounts
                              where a.Email.Equals(param.Email)
                              where a.TermAddr.Equals(param.TermAddr)
                              where a.RemoteAddr.Equals(param.RemoteAddr)
                              select a).FirstOrDefault();

                //データが取得できた場合　中身を確認する
                if (acount == null)
                {


                    param._message = _localizer["Please sign-in after registering e-mail."];
                    param.Authorized = false;
                    param.Applying = false;
                    return BadRequest(param);

                }
                else
                {
                    param.Ticket = acount.Ticket;
                }

            }
            try
            {   //ログインユーザの取得
                var user = (from a in _context.Accounts
                            where a.Ticket.Equals(param.Ticket)
                            select a).FirstOrDefault();
                
                if (user == null)
                {

                    if (acount != null)
                    {
                        _context.Remove(acount);
                        _context.SaveChanges();

                    }

                    param._message = _localizer["未登録です"];
                    param.Authorized = false;
                    param.Applying = false;
                    return BadRequest(param);

                }
                //使用禁止
                else if(user.LockoutEnabled)
                {

                    param._message = _localizer["ロックされています。"];
                    param.Authorized = false;
                    param.Applying = true;
                    return BadRequest(param);

                }
                //データが取得できた場合　中身を確認する
                else if(!user.EmailConfirmed)
                {
                    param._message = _localizer["本人確認中です。"];
                    param.Authorized = false;
                    param.Applying = true;
                    return BadRequest(param);

                }
                //管理者確認待ち
                else if(!user.ManagerConfirmed)
                {

                    param._message = _localizer["管理者確認中です。"];
                    param.Authorized = false;
                    param.Applying = true;
                    return BadRequest(param);

                }
                //本人確認済みであること
                else if(user.EmailConfirmed && user.ManagerConfirmed)
                {
                    //
                    user.AccessCount += 1;
                    user.Updated = DateTime.Now;
                    user.Updater = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

                    _context.Accounts.Update(user);

                    _context.SaveChanges();

                    ReflectionUtility.Model2Model(user, param);

                    param.Authorized = true;
                    param.Applying = false;
                    return Ok(param);
                }
                else
                {
                    param._message =_localizer["不明"];
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
                    //Host = "pop3.lolipop.jp", // or "mailgate.ipentec.com";
                    Host = "smtp8.gmoserver.jp",
                    Port = 587,   // or 587;
                                  //string smtpserver = "pop3.lolipop.jp"; 
                                  //int port = 587;

                    //ユーザー名とパスワードを設定する
                    Credentials = new System.Net.NetworkCredential(param.FromEmail, "ewgya$0k"),
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



    
        // POST api/auth
        [HttpPost]
        public IActionResult Create([FromBody] SignUp param)
        {
            if (param == null)
            {
                param = new SignUp();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            if (!TryValidateModel(param))
            {
                param._message = _localizer["The input is incorrect."];
                
                return BadRequest(param);
            }
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
                param._message = _localizer["Account has already been registered."]; ;
                return BadRequest(param);

                ////期限をチェック
                ////前のユーザを破棄して、再発行する
                //_context.Accounts.Remove(acount);
                //_context.SaveChanges();
                //param.Ticket = null;

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
                mail.ToEmail = "satohhd@asystem-design.co.jp";
                mail.Sender = _localizer["システム管理者"];
                mail.Subject = _localizer["本人確認メールです。"];
                //mail.Body = "Confirm your account Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
                mail.Body = _localizer["If you use the system please click this URL. {0} ", callbackUrl];



                if (SendMail(mail))
                {
                    param._message = _localizer["We sent a person confirmation mail. Please SignIn after approval"];

                }
                else
                {
                    param._message =_localizer["Please check that the specified mail is correct."];
                };
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }

        // PUT api/auth/id
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult ActionResult(string id, [FromBody] Edit param)
        {
            if (id == null || param == null)
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
            //内部では、メールアドレス、暗号キー、アクセスアドレス、時間を退避
            try
            {
                var user = (from a in _context.Accounts
                            where a.AccountId.Equals(id)
                            select a).FirstOrDefault();


                if (user.Version != param.Version)
                {
                    param._message = _localizer["Other people have been updated. Please try again."];

                    return BadRequest(param);
                }
                //ReflectionUtility.Model2Model(param, user, "AccountId,Registed,Owner");
                user.UserName = param.UserName;
                user.Updated = DateTime.Now;
                user.Updater = HttpContext.User.Identity.Name;
                user.Version += 1;


                //登録
                _context.Accounts.Update(user);
                _context.SaveChanges();

                var signIn = new SignIn();

                ReflectionUtility.Model2Model(user, signIn);

                if (user.EmailConfirmed && user.ManagerConfirmed)
                {
                    signIn.Authorized = true;
                    signIn.Applying = false;
                }
                else
                {
                    signIn.Authorized = false;
                    signIn.Applying = true;
                }


                return Ok(signIn);
                ///////////////////////////////////////
                //同時にAspUserにも登録
                //ユーザーの追加
                //var user = new ApplicationUser();

                //user.UserName = "test@test.com";
                //user.Email = "test@test.com";
                ////ハッシュ化する
                //user.PasswordHash = new PasswordHasher().HashPassword("test@test.com");
                //SecurityStampを設定する（これがNullだと認証でエラーとなる）
                //user.SecurityStamp = Guid.NewGuid().ToString();

                ////追加処理
                //_context.Users.Add(user);
                //_context.SaveChanges();

                //そのままサインイン処理
                //var signInManager = this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                //signInManager.SignIn(user, false, false);



                ///////////////////////////////////////


            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
    

        }


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
