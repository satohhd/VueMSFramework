using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models;
using VueMSFramework.ViewModels.Mail;

namespace VueMSFramework.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MailController : PageController<MailController>
    {
        public MailController(ApplicationDbContext context, IStringLocalizer<MailController> localizer) : base(context, localizer)
        {
        }

        /**
        *  GetSentListByCondition
        *  param 
        */
        private List<SentList> GetSentListByCondition(Search param)
        {
            try
            {
                List<SentList> list = null;
                //var v = new VMailsSearch();
                var v = param;

                //件数を取得する
                var count = (from a in _context.Mails
                             where (a.Subject.Contains(param.Keywords) || a.Body.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                             
                             where a.IsSent.Equals(true)
                             select a).Count();


                //ページ情報
                var pagination = v.Tabs.Tab1.SentListPager;
                pagination.RecordCount = count;
                param.Tabs.Tab1.SentListPager = pagination;

                //送信済み
                list = (from a in _context.Mails
                        where (a.Subject.Contains(param.Keywords) || a.Body.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                        
                        where a.IsSent.Equals(true)
                        orderby a.Registed descending
                        select new SentList
                        {
                            MailId = a.MailId,
                            SentDate = a.Updated,
                            Subject = a.Subject,
                            Status = "送信済み",
                            SentCount = a.SentCount,
                            ReadCount = a.ReadCount,
                            ErrorCount = a.ErrorCount,
                        }).Skip(pagination.RecordsPerPage * (pagination.CurrentPageNumber - 1)).Take(pagination.RecordsPerPage).ToList<SentList>();

                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /**
         *  GetDraftListByCondition
         *  param 
         */
        private List<DraftList> GetDraftListByCondition(Search param)
        {
             try
            {
                List<DraftList> list = null;
                //var v = new VMailsSearch();
                var v = param;
                //件数を取得する
                var count = (from a in _context.Mails
                             where (a.Subject.Contains(param.Keywords) || a.Body.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                             where a.IsDraft.Equals(true)
                             select a).Count();

                //ページ情報
                var pagination = v.Tabs.Tab2.DraftListPager;
                pagination.RecordCount = count;
                param.Tabs.Tab2.DraftListPager = pagination;

                //下書き
                list = (from a in _context.Mails
                        where (a.Subject.Contains(param.Keywords) || a.Body.Contains(param.Keywords) || string.IsNullOrEmpty(param.Keywords))
                        
                        where a.IsDraft.Equals(true)
                        orderby a.Registed descending  /* descending, a.Title */
                        select new DraftList
                        {
                            MailId = a.MailId,
                            Created = a.Updated,
                            Subject = a.Subject,
                            Status = "下書き",
                        }).Skip(pagination.RecordsPerPage * (pagination.CurrentPageNumber - 1)).Take(pagination.RecordsPerPage).ToList<DraftList>();



                return list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Post api/mail/search/load
        [HttpPost(), Route("search/load")]
        public ActionResult<Search> Load([FromBody]Search param)
        {
            var sentList = GetSentListByCondition(param);
            var draftList = GetDraftListByCondition(param);

            param.Tabs.Tab1.SentList = sentList;
            param.Tabs.Tab2.DraftList = draftList;

            return Ok(param);
        }
        // Post api/mail/init/Create
        //[Authorize]
        [HttpPost(), Route("create/load")]
        public ActionResult<Create> Load([FromBody]Create param)
        {

            try
            {
                    //var initModel = new Create();

                    var tuser = (from a in _context.Tusers
                                 orderby a.TuserKana ascending
                                 select new UserSelectList
                                 {
                                     TuserId = a.TuserId,
                                     TuserName = a.TuserName,
                                     TuserKana = a.TuserKana,
                                     Affiliation = a.Affiliation,

                                     ToEmail = a.ToEmail,
                                     ToEmail2 = a.ToEmail2,
                                     ToEmail3 = a.ToEmail3,
                                     Tel = a.Tel,

                                 }).ToList();
                    param.Tabs.Tab1.SelectList = tuser;

                    var userInfo = (from a in _context.Tusers
                                    where a.TuserName.Equals(HttpContext.User.Identity.Name)
                                    select a).FirstOrDefault();
                    string userAddr = "";
                    if (userInfo != null)
                    {
                        userAddr = userInfo.ToEmail;
                    }
                    param.Signature = String.Format("\n送信者：{0}\n{1}\n{2}", HttpContext.User.Identity.Name, userAddr, param.Signature);


                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }

        }

        // Post api/mail/init/Edit
        //[Authorize]
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {

            try
            {

                var mail = (from a in _context.Mails
                            where a.MailId.Equals(param.MailId)
                            select a).FirstOrDefault();

                if (mail == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(mail, param);
                var tuser = (from a in _context.Tusers
                             join b in _context.MailTusers
                             on new { a.TuserId, param.MailId } equals new { b.TuserId, b.MailId } into abJoin
                             from c in abJoin.DefaultIfEmpty()
                             orderby a.TuserKana ascending
                             select new UserSelectList
                             {
                                 IsSelect = c.MailId == null ? false : true,
                                 TuserId = a.TuserId,
                                 TuserName = a.TuserName,
                                 TuserKana = a.TuserKana,
                                 ToEmail = a.ToEmail,
                                 ToEmail2 = a.ToEmail2,
                                 ToEmail3 = a.ToEmail3,
                             }).ToList();

                param.Tabs.Tab1.SelectList = tuser;

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }

        }

        [HttpPost(), Route("create/select")]
        public ActionResult<Create> Select([FromBody]Create param)
        {

            try
            {
 
                    string groupId = param.Tabs.Tab1.SelectPanel.BtnGroup;
                    var tuser = (from a in _context.Tusers
                                 join b in _context.GroupTusers
                                 on new { a = a.TuserId, b = groupId } equals new { a = b.TuserId, b = b.GroupId } into abJoin
                                 from c in abJoin.DefaultIfEmpty()
                                 orderby a.TuserKana ascending
                                 select new UserSelectList
                                 {
                                     IsSelect = c.TuserId == null ? false : true,
                                     TuserId = a.TuserId,
                                     TuserName = a.TuserName,
                                     TuserKana = a.TuserKana,
                                     ToEmail = a.ToEmail,
                                     ToEmail2 = a.ToEmail2,
                                     ToEmail3 = a.ToEmail3,
                                 }).ToList();

                    param.Tabs.Tab1.SelectList = tuser;



                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }

        }
  
        [HttpPost(), Route("create/recycle")]
        public ActionResult<Create> Recycle([FromBody]Create param)
        {
            try
            {

                var mail = (from a in _context.Mails
                            where a.MailId.Equals(param.MailId)
                            select a).FirstOrDefault();

                if (mail == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(mail, param);
                var tuser = (from a in _context.Tusers
                             join b in _context.MailTusers
                             on new { a.TuserId, param.MailId } equals new { b.TuserId, b.MailId } into abJoin
                             from c in abJoin.DefaultIfEmpty()
                             orderby a.TuserKana ascending
                             select new UserSelectList
                             {
                                 IsSelect = c.MailId == null ? false : true,
                                 TuserId = a.TuserId,
                                 TuserName = a.TuserName,
                                 TuserKana = a.TuserKana,
                                 ToEmail = a.ToEmail,
                                 ToEmail2 = a.ToEmail2,
                                 ToEmail3 = a.ToEmail3,
                             }).ToList();

                param.Tabs.Tab1.SelectList = tuser;


                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }

        }

        // Post api/mail/refer/load
        [HttpPost(), Route("refer/load")]
        public ActionResult<Refer> Load([FromBody]Refer param)
        {

            try
            {
                var mail = (from a in _context.Mails
                            
                            where a.MailId.Equals(param.MailId)
                            select a).FirstOrDefault();

                if (mail == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(mail, param);

                var tuser = (from a in _context.MailTusers
                             join t in _context.Tusers
                             on a.TuserId  equals t.TuserId
                             
                             where a.MailId.Equals(param.MailId)
                             orderby t.TuserKana ascending
                             select new UserList
                             {
                                 IsSelect = true,
                                 TuserId = a.TuserId,
                                 ToEmail = t.ToEmail,
                                 ToEmail2 = t.ToEmail2,
                                 ToEmail3 = t.ToEmail3,
                                 TuserName = t.TuserName,
                                 TuserKana = t.TuserKana,
                                 ErrorMessage = a.ErrorMessage,
                                 IsError = a.IsError ? "エラー":"",
                                 IsSent = a.IsSent? "済":"",
                                 Status = a.IsRead ? "既読":"未読"
                                 
                             }).ToList();


                param.SelectList = tuser;
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }


        }
        // Post api/mail/remove/load
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {

            try
            {
                var mail = (from a in _context.Mails
                            
                            where a.MailId.Equals(param.MailId)
                            select a).FirstOrDefault();

                if (mail == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(mail, param);

                var tuser = (from a in _context.MailTusers
                             join t in _context.Tusers
                            on a.TuserId equals t.TuserId
                             
                             where a.MailId.Equals(param.MailId)
                             orderby t.TuserKana ascending
                             select new UserList
                             {
                                 IsSelect = true,
                                 TuserId = a.TuserId,
                                 ToEmail = t.ToEmail,
                                 ToEmail2 = t.ToEmail2,
                                 ToEmail3 = t.ToEmail3,
                                 TuserName = t.TuserName,
                                 TuserKana = t.TuserKana,
                                 ErrorMessage = a.ErrorMessage,
                                 IsError = a.IsError ? "エラー" : "",
                                 IsSent = a.IsSent ? "済" : "",
                                 Status = a.IsRead ? "既読" : "未読"
                             }).ToList();


                param.SelectList = tuser;


                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }
        }
        [HttpPost(), Route("create/draft")]
        public ActionResult<Create> Draft([FromBody]Create param)
        {


            try
            {

                param.MailId = Guid.NewGuid().ToString();
                var mail = new Mail();
                ReflectionUtility.Model2Model(param, mail);


                try
                {

                    mail.IsSent = false;
                    mail.IsDraft = true;
                    mail.ErrorCount = 0;
                    mail.SentCount = 0;
                    mail.Updated = DateTime.Now;

                    mail.Registed = DateTime.Now;
                    mail.Version = 1;
                    _context.Mails.Add(mail);
                    _context.SaveChanges();

                    //var ret = new Confirm();
                    ReflectionUtility.Model2Model(mail, param);

                    return Ok(param);


                }
                catch (Exception ex)
                {
                    param._message = _localizer["下書き登録エラー" + ex.Message];
                    return BadRequest(param);
                }
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }
        }

        // Post api/mail/create/confirm
        //[Authorize]
        [HttpPost(), Route("create/confirm")]
        public ActionResult<Create> Confirm([FromBody]Create param)
        {

            //件数チェック
            bool existsSelected = false;
            var list = param.Tabs.Tab1.SelectList;
            foreach (var l in list)
            {
                if (l.IsSelect ?? false)
                {
                    existsSelected = true;
                    break;
                }
            }

            if (!existsSelected)
            {

                param._message = _localizer["送信先が選択されていません。"];
                return BadRequest(param);
            }

            try
            {

                param.MailId = Guid.NewGuid().ToString();


                var okCount = 0;
                var ngCount = 0;
                foreach (var i in param.Tabs.Tab1.SelectList)
                {
                    if (i.IsSelect ?? false)
                    {

                        var mt = new MailTuser();
                        ReflectionUtility.Model2Model(i, mt);

                        //mt.MailUserId = Guid.NewGuid().ToString();
                        mt.MailId = param.MailId;
                        mt.TuserId = i.TuserId;
                        _context.MailTusers.Add(mt);

                    }
                }
                _context.SaveChanges();
                var mail = new Mail();
                ReflectionUtility.Model2Model(param, mail);


                try
                {

                    mail.IsSent = true;
                    mail.IsDraft = false;
                    mail.ErrorCount = ngCount;
                    mail.SentCount = okCount;
                    mail.Updated = DateTime.Now;

                    mail.Registed = DateTime.Now;
                    mail.Version = 1;
                    _context.Mails.Add(mail);
                    _context.SaveChanges();

                    //var ret = new Confirm();
                    ReflectionUtility.Model2Model(mail, param);

                    return Ok(param);


                }
                catch (Exception ex)
                {
                    param._message = _localizer["登録エラー" + ex.Message];
                    return BadRequest(param);
                }
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }
        }
        [HttpPost(), Route("edit/draft")]
        public ActionResult<Edit> Draft([FromBody]Edit param)
        {


            try
            {

                var mail = new Mail();
                ReflectionUtility.Model2Model(param, mail);
                try
                {

                    mail.IsSent = false;
                    mail.IsDraft = true;
                    mail.Updated = DateTime.Now;
                    mail.Version += 1;
                    _context.Mails.Update(mail);
                    _context.SaveChanges();

                    //var ret = new Confirm();
                    ReflectionUtility.Model2Model(mail, param);

                    return Ok(param);


                }
                catch (Exception ex)
                {
                    param._message = _localizer["下書き登録エラー" + ex.Message];
                    return BadRequest(param);
                }
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }
        }
        // Post api/mail/edit/confirm
        [HttpPost(), Route("edit/confirm")]
        public ActionResult<Edit> Confirm([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            //件数チェック
            bool existsSelected = false;
            var list = param.Tabs.Tab1.SelectList;
            foreach (var l in list)
            {
                if (l.IsSelect ?? false)
                {
                    existsSelected = true;
                    break;
                }
            }

            if (!existsSelected)
            {

                param._message = _localizer["送信先が選択されていません。"];
                return BadRequest(param);
            }

            try
            {

                var okCount = 0;
                var ngCount = 0;
                foreach (var i in param.Tabs.Tab1.SelectList)
                {
                    if (i.IsSelect ?? false)
                    {

                        var mt = new MailTuser();
                        ReflectionUtility.Model2Model(i, mt);

                        //mt.MailUserId = Guid.NewGuid().ToString();
                        mt.MailId = param.MailId;
                        mt.TuserId = i.TuserId;
                        _context.MailTusers.Add(mt);

                    }
                }
                _context.SaveChanges();
                var mail = new Mail();
                ReflectionUtility.Model2Model(param, mail);


                try
                {

                    mail.IsSent = true;
                    mail.IsDraft = false;
                    mail.ErrorCount = ngCount;
                    mail.SentCount = okCount;
                    mail.Updated = DateTime.Now;

                    mail.Registed = DateTime.Now;
                    mail.Version = 1;
                    _context.Mails.Update(mail);
                    _context.SaveChanges();

                    ReflectionUtility.Model2Model(mail, param);

                    return Ok(param);


                }
                catch (Exception ex)
                {
                    param._message = _localizer["登録エラー" + ex.Message];
                    return BadRequest(param);
                }
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }
        }
   

        [HttpPost(), Route("confirm/load")]
        public IActionResult Load([FromBody]Confirm param)
        {
            if (param == null)
            {
                param = new Confirm();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

            if (param.MailId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var mail = (from a in _context.Mails
                            where a.MailId.Equals(param.MailId)
                            select a).FirstOrDefault();

                if (mail == null)
                {
                    param._message = "メールが存在しません。";
                    return BadRequest(param);
                }

                ReflectionUtility.Model2Model(mail, param);

                var selectUser = (from a in _context.MailTusers
                             join t in _context.Tusers
                             on a.TuserId equals t.TuserId
                             where a.MailId.Equals(param.MailId)
                             orderby t.TuserKana ascending
                             select new ConfirmUserList
                             {
                                 IsSelect = true,
                                 MailId = a.MailId,
                                 TuserId = a.TuserId,
                                 ToEmail = t.ToEmail,
                                 ToEmail2 = t.ToEmail2,
                                 ToEmail3 = t.ToEmail3,
                                 TuserName = t.TuserName,
                                 TuserKana = t.TuserKana,
                                 ErrorMessage = a.ErrorMessage,
                                 IsError = a.IsError ? "エラー" : "",
                                 IsSent = a.IsSent ? "済" : "",
                                 Status = a.IsRead ? "既読" : "未読"

                             }).ToList();



                //確認画面用に編集
                param.TerminalBody = BuildBodyStr(param, null);
                param.WebPageBody = BuildWebPageBodyStr(param, null);
                param.SelectedList = selectUser;
                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["システムエラー" + ex.Message];
                return BadRequest(param);
            }

        }

        //本文を組み立てる 本体　リンク　署名
        private string BuildBodyStr(Confirm vMail, ConfirmUserList vUserlist)
        {
            if (vUserlist == null)
            {
                vUserlist = new ConfirmUserList();
                vUserlist.TuserId = "ABCDEFG";
                vUserlist.TuserName = "〇〇　〇〇";

            }

            var req = HttpContext.Request;

            var body = vUserlist.TuserName + "様へ送信されたメールです。\n\n";
            var link = string.Format("添付ファイルはこちら: {0}://{1}{2}{3}", req.Scheme, req.Host, "/public/reference/", vMail.MailId + '=' + vUserlist.TuserId);
            if (vMail.Signature == null) vMail.Signature = "";

            body += "\n\n"
                   + link
                   + "\n"
                   + vMail.Signature;

            return body;
            
        }
        //本文を組み立てる 本体　リンク　署名
        private string BuildWebPageBodyStr(Confirm vMail, UserList vUserlist)
        {
            if (vUserlist == null)
            {
                vUserlist = new UserList();
                vUserlist.TuserId = "ABCDEFG";
                vUserlist.TuserName = "〇〇　〇〇";

            }
            if (vMail.Signature == null) vMail.Signature = "";

            var body = vUserlist.TuserName + "様へ送信されたメールです。全文\n";
            body += vMail.Body;
            body += vMail.Signature;

            return body;

        }
        // Post api/mail/confirm/send
        //[Authorize]
        [HttpPost(), Route("confirm/send")]
        public ActionResult<Confirm> Send([FromBody] Confirm param)
        {
            if (param == null)
            {
                param = new Confirm();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }

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
                    //Credentials = new System.Net.NetworkCredential(vmodel.FromEmail, "Tomoko3104_321"),
                    Credentials = new System.Net.NetworkCredential(param.FromEmail, "ewgya$0k"),
                    //Properties.Settings.Default.SMTPAuthUser, Properties.Settings.Default.SMTPAuthPass);
                    //or sc.Credentials = new System.Net.NetworkCredential("automail","pass123456");
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,

                    //現在は、EnableSslがtrueでは失敗する
                    EnableSsl = true
                };
                var okCount = 0;
                var ngCount = 0;
                foreach (var i in param.SelectedList)
                {
                    if (i.IsSelect)
                    {
                        //var enc = Encoding.GetEncoding(65001);
                        //var enc_2022 = Encoding.GetEncoding(50220);
                        //var from = myEncode(vmodel.FromEmail, enc_2022);
                        //var subject = myEncode(vmodel.Subject, enc_2022);
                        var msg = new System.Net.Mail.MailMessage();
                        msg.From = new System.Net.Mail.MailAddress(param.FromEmail, param.Sender);
                        msg.To.Add(new System.Net.Mail.MailAddress(i.ToEmail, i.TuserName));
                        if (i.ToEmail2 != null && i.ToEmail2 != "") msg.To.Add(new System.Net.Mail.MailAddress(i.ToEmail2, i.TuserName));
                        if (i.ToEmail3 != null && i.ToEmail3 != "") msg.To.Add(new System.Net.Mail.MailAddress(i.ToEmail3, i.TuserName));
                        msg.IsBodyHtml = false;
                        msg.Subject = param.Subject;
                        msg.Body = BuildBodyStr(param, i); 
                       
                        try
                        {
                            //メッセージを送信する
                            sc.Send(msg);
                            i.SentDate = DateTime.Now.ToLongDateString();
                            i.Status = "OK";
                            i.IsError = "false";
                            i.IsSent = "true";
                            i.ErrorMessage = null;
                            okCount += 1;

                            //後始末
                            msg.Dispose();
                        }
                        catch (Exception ex)
                        {
                            i.SentDate = DateTime.Now.ToLongDateString();
                            i.Status = "NG";
                            i.IsError = "true";
                            i.IsSent = "false";
                            i.ErrorMessage = ex.Message;
                            ngCount += 1;

                            //後始末
                            msg.Dispose();
                        }

                        //1件づつ登録
                        //更新前データを取得する
                        var storeModel = (from a in _context.MailTusers
                                          where a.MailId.Equals(i.MailId)
                                          where a.TuserId.Equals(i.TuserId)
                                          select a).FirstOrDefault();

                        if (storeModel == null)
                        {
                            param._message = _localizer["It has already been deleted."];
                            return BadRequest(param);
                        }

                        ReflectionUtility.Model2Model(param, storeModel);
                        storeModel.Updated = DateTime.Now;
                        storeModel.Version += 1;


                        _context.MailTusers.Update(storeModel);
                        _context.SaveChanges();

                    }
                }
                _context.SaveChanges();

                sc.Dispose();

                //メール登録
                var mail = new Mail();
                ReflectionUtility.Model2Model(param, mail);


                    try
                    {

                        mail.IsSent = true;
                        mail.IsDraft = false;
                        mail.ErrorCount = ngCount;
                        mail.SentCount = okCount;
                        mail.Updated = DateTime.Now;
                        mail.Registed = DateTime.Now;
                        mail.Version = 1;
                        _context.Mails.Update(mail);
                        _context.SaveChanges();

                    ReflectionUtility.Model2Model(mail, param);


                    return Ok(param);


                }
                catch (DbUpdateConcurrencyException ex)
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
                var model = (from a in _context.Mails
                             where a.MailId.Equals(param.MailId)
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

                _context.Mails.Remove(model);
                _context.SaveChanges();
                ReflectionUtility.Model2Model(model,param );
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
