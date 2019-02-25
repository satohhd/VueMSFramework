using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using VueMSFramework.Core.Utils;
using VueMSFramework.Data;
using VueMSFramework.Models.Kintai;
using VueMSFramework.ViewModels.Kintai;

namespace Vue2Spa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class KintaiController : PageController
    {
        public KintaiController(ApplicationDbContext context, IStringLocalizer<KintaiController> localizer) : base(context, localizer)
        {
        }

        // Post api/kintai/select
        [HttpPost(), Route("index")]
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


        // Post api/kintai/select
        [HttpPost(), Route("select/load")]
        public ActionResult<Select> Load([FromBody]Select param)
        {
            if (param == null)
            {
                param = new Select();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {

                var list = (from a in _context.Shains

                             orderby a.Ymgn ascending
                             select new ShainList
                             {
                                 YmgnIchimoji = a.Ymgn==null?"":a.Ymgn.Substring(0,1),
                                 ShainId = a.ShainId,
                                 Shinbng = a.Shinbng,
                                 Shmi = a.Shmi,
                             }).ToList();


                param.ShainList = list;

                System.Threading.Thread.Sleep(500);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }
        // Post api/kintai/search/next
        [HttpPost(), Route("search/next")]
        public ActionResult<Search> Next([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
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
        // Post api/kintai/search/next
        [HttpPost(), Route("search/prev")]
        public ActionResult<Search> Prev([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
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

   

        // Post api/kintai/search/approval
        [HttpPost(), Route("search/approval")]
        public ActionResult<Search> Approval([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
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

        // Post api/kintai/search/pdf
        [HttpPost(), Route("search/pdf")]
        public ActionResult<Search> Pdf([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
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



        // Post api/kintai/search/output
        [HttpPost(), Route("search/output")]
        public ActionResult<Search> Output([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
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




        // Post api/kintai/search
        [HttpPost(), Route("search/load")]
        public ActionResult<Search> Load([FromBody]Search param)
        {
            if (param == null)
            {
                param = new Search();
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
                var culture = System.Globalization.CultureInfo.GetCultureInfo("ja-JP");

                //社員情報取得
                var shaininfo = (from a in _context.Shains
                                 where a.ShainId.Equals(param.ShainId)
                                 orderby a.Ymgn ascending
                                 select new ShainInfo
                                 {
                                     ShainId = a.ShainId,
                                     Shinbng = a.Shinbng,
                                     Shmi = a.Shmi,
                                     Bmnmi = a.Bmnmi,
                                     Kykti = a.Kykti,
                                     Kytnmi = a.Kytnmi,

                                 }).ToList();

                param.ShainInfo = shaininfo;

                //未設定の場合
                if (param.YmPanel.YearMonth == null)
                {
                    param.YmPanel.YearMonth = DateTime.Now.ToString("yyyy-MM");
                }

                //選択リスト

                string[] RowColor = { "Su", "M", "Tu", "W", "Th", "F", "Sa", };
                var list = new List<KintaiList>();
                //31日ループ
                for (var i = 1; i <= 31; i++)
                {

                    var dayStr = param.YmPanel.YearMonth.Substring(0,4) + "-" + param.YmPanel.YearMonth.Substring(5, 2) + "-" + i.ToString("00");
                    //月が替わった場合
                    if (!DateTime.TryParse(dayStr, out DateTime d)) break;

                    var day = (from a in _context.Kintais
                               join b in _context.Shains
                               on a.ShainId equals b.ShainId into ab
                               from shain in ab.DefaultIfEmpty()
                               join c in _context.Contents
                               on a.Knmkbn equals c.ContentId into ac
                               from knmkbn in ac.DefaultIfEmpty()
  
                               where a.ShainId.Equals(param.ShainId)
                               where a.Hdk.Equals(DateTime.Parse(dayStr))
                               select new KintaiList
                               {
                                   KintaiId = a.KintaiId,
                                   ShainId = a.ShainId,
                                   Shinbng = shain.Shinbng,
                                   Shmi = shain.Shmi,
                                   Dow = RowColor[(int)a.Hdk.DayOfWeek],
                                   Hdk = dayStr,
                                   Hdk_Yb = a.Hdk.ToString("dd") + "  " + a.Hdk.ToString("ddd", culture),
                                   Knmkbn_KnmkbnJsk = (knmkbn.ContentName?? "休暇") + "<br>",
                                   Sykjk_SykjkDkk = (a.Sykjk??"--:--") + "<br>" + (a.SykjkDkk??"--:--"),
                                   Sykjk = a.Sykjk,
                                   SykjkDkk = a.SykjkDkk,
                                   Tkjk_TkjkDkk = (a.Tkjk??"--:--") + "<br>" + (a.TkjkDkk??"--:--"),
                                   Tkjk = a.Tkjk,
                                   TkjkDkk = a.TkjkDkk,
                                   KykJkn = a.KykJkn??"--:--",
                                   Zgyjkn = a.Zgyjkn ?? "--:--",
                                   Zgyjkn36 = a.Zgyjkn36 ?? "--:--",
                                   KntiChart = "",
                                   Biko = a.Biko,
                                   _rowVariant = RowColor[(int)a.Hdk.DayOfWeek],
                               }).FirstOrDefault();

                    if ( day != null)
                    {
                        list.Add(day);

                    } else{

                        list.Add(new KintaiList() {
                            ShainId = param.ShainId
                           , Shinbng = param.ShainInfo[0].Shinbng
                           , Shmi = param.ShainInfo[0].Shmi
                           , Dow = DateTime.Parse(dayStr).ToString("ddd")
                           , Hdk = dayStr
                           , Hdk_Yb = i.ToString("00") + "  " + DateTime.Parse(dayStr).ToString("ddd", culture)
                           , Knmkbn_KnmkbnJsk = "-"
                           , Sykjk_SykjkDkk = "--:--"
                           , Tkjk_TkjkDkk = "--:--"
                           , KykJkn = "--:--"
                           , Zgyjkn = "--:--"
                           , Zgyjkn36 = "--:--"
                           , KntiChart = ""
                           ,Biko = ""
                           ,_rowVariant = RowColor[(int)DateTime.Parse(dayStr).DayOfWeek],
                        });

                    }
                }


                param.Tabs.Tab1.KintaiList = list;
                //model.Tabs.Tab1.KintaiList = GetKintaiList(model);
                //System.Threading.Thread.Sleep(500);

                return Ok(param);
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        //// Post api/kintai/create/load
        //[HttpPost(), Route("create/load")]
        //public ActionResult<Create> Load([FromBody]Create param)
        //{
        //    if (param == null)
        //    {
        //        param = new Create();
        //        param._message = _localizer["No parameters."];
        //        return BadRequest(param);
        //    }
        //    return Ok(param);

        //}
        // Post api/kintai/edit
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

                if (param.KintaiId == null)
                {
                    //var model = new Kintai();
                    //ReflectionUtility.Model2Model(model, param);
                    return Ok(param);

                }
                else
                {
                    var model = (from a in _context.Kintais
                                 where a.KintaiId.Equals(param.KintaiId)
                                 select a).FirstOrDefault();

                    if (model == null)
                    {
                        param._message = _localizer["It has already been deleted."];
                        return BadRequest(param);
                    }

                    ReflectionUtility.Model2Model(model, param);

                }

                System.Threading.Thread.Sleep(500);
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }



        }
        // Post api/kintai/remove/load
        [HttpPost(), Route("remove/load")]
        public ActionResult<Remove> Load([FromBody]Remove param)
        {

            if (param == null)
            {
                param = new Remove();
                param._message = _localizer["No parameters."];
                return BadRequest(param);

            }
            if (param.KintaiId == null)
            {
                param._message = _localizer["The key parameter has not been set yet."];
                return BadRequest(param);
            }
            try
            {
                var model = (from a in _context.Kintais
                             where a.KintaiId.Equals(param.KintaiId)
                             select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                ReflectionUtility.Model2Model(model, param);

                System.Threading.Thread.Sleep(500);

                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }

        }

 
        private string Hhmmformater(string str)
        {

            if (str == null || str.Length == 0)
            {
                return null;
            }
            else
            {
                return str.Split(':')[0].PadLeft(2, '0') + ":" + str.Split(':')[1].PadLeft(2, '0');

            }
        }


        //// POST api/kintai/create/insert
        //[HttpPost(), Route("create/insert")]
        //public ActionResult<Create> Insert([FromBody] Create param)
        //{

        //    if (param == null)
        //    {
        //        param = new Create();
        //        param._message = _localizer["No parameters."];
        //        return BadRequest(param);
        //    }

        //    if (!TryValidateModel(param))
        //    {
        //        param._message = _localizer["The input is incorrect."];

        //        return BadRequest(param);
        //    }


        //    try
        //    {
        //        var storeModel = new Kintai();
        //        ReflectionUtility.Model2Model(param, storeModel);

        //        //時間の書式統一
        //        storeModel.Sykjk = Hhmmformater(storeModel.Sykjk);
        //        storeModel.Tkjk = Hhmmformater(storeModel.Tkjk);
        //        storeModel.SykjkDkk = Hhmmformater(storeModel.SykjkDkk);
        //        storeModel.TkjkDkk = Hhmmformater(storeModel.TkjkDkk);

        //        storeModel.KykJkn = Hhmmformater(storeModel.KykJkn);
        //        storeModel.Zgyjkn = Hhmmformater(storeModel.Zgyjkn);
        //        storeModel.Zgyjkn36 = Hhmmformater(storeModel.Zgyjkn36);

        //        storeModel.KintaiId = (Guid.NewGuid()).ToString();
        //        storeModel.Owner = HttpContext.User.Identity.Name;
        //        storeModel.Registed = DateTime.Now;
        //        storeModel.Updated = DateTime.Now;
        //        storeModel.Version = 1;

        //        //登録
        //        _context.Kintais.Add(storeModel);
        //        _context.SaveChanges();
        //        ReflectionUtility.Model2Model(storeModel, param);
        //        return Ok(param);
        //    }
        //    catch (Exception ex)
        //    {
        //        param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
        //        return BadRequest(param);
        //    }

        //}

        // PUT api/kintai/edit/store
        [HttpPost(), Route("edit/store")]
        public ActionResult<Edit> Store([FromBody] Edit param)
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

            //新規の場合
            if (param.KintaiId == null)
            {

                try
                {
                    var storeModel = new Kintai();
                    ReflectionUtility.Model2Model(param, storeModel);

                    //時間の書式統一
                    storeModel.Sykjk = Hhmmformater(storeModel.Sykjk);
                    storeModel.Tkjk = Hhmmformater(storeModel.Tkjk);
                    storeModel.SykjkDkk = Hhmmformater(storeModel.SykjkDkk);
                    storeModel.TkjkDkk = Hhmmformater(storeModel.TkjkDkk);

                    storeModel.KykJkn = Hhmmformater(storeModel.KykJkn);
                    storeModel.Zgyjkn = Hhmmformater(storeModel.Zgyjkn);
                    storeModel.Zgyjkn36 = Hhmmformater(storeModel.Zgyjkn36);

                    storeModel.KintaiId = (Guid.NewGuid()).ToString();
                    storeModel.Owner = HttpContext.User.Identity.Name;
                    storeModel.Registed = DateTime.Now;
                    storeModel.Updated = DateTime.Now;
                    storeModel.Version = 1;

                    //登録
                    _context.Kintais.Add(storeModel);
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
            else
            {
                //更新の場合

 
                try
                {
                    //更新前データを取得する
                    var storeModel = (from a in _context.Kintais
                                      where a.KintaiId.Equals(param.KintaiId)
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

                    //時間の書式統一
                    storeModel.Sykjk = Hhmmformater(storeModel.Sykjk);
                    storeModel.Tkjk = Hhmmformater(storeModel.Tkjk);
                    storeModel.SykjkDkk = Hhmmformater(storeModel.SykjkDkk);
                    storeModel.TkjkDkk = Hhmmformater(storeModel.TkjkDkk);

                    storeModel.KykJkn = Hhmmformater(storeModel.KykJkn);
                    storeModel.Zgyjkn = Hhmmformater(storeModel.Zgyjkn);
                    storeModel.Zgyjkn36 = Hhmmformater(storeModel.Zgyjkn36);


                    storeModel.Updated = DateTime.Now;
                    storeModel.Version += 1;


                    //更新
                    _context.Kintais.Update(storeModel);
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
        }

        // DELETE api/kintai/id
        [HttpPost(), Route("remove/delete")]
        public ActionResult<Remove> Delete(Remove param)
        {

            if (param == null)
            {
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }



            try
            {
                var model = new Kintai();

                model = (from a in _context.Kintais
                         where a.KintaiId.Equals(param.KintaiId)
                         select a).FirstOrDefault();

                if (model == null)
                {
                    param._message = _localizer["It has already been deleted."];
                    return BadRequest(param);
                }
                //削除
                _context.Kintais.Remove(model);
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
