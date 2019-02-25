using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using VueMSFramework.Data;
using VueMSFramework.ViewModels.Fukuri;

namespace Vue2Spa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FukuriController : PageController
    {
        public FukuriController(ApplicationDbContext context, IStringLocalizer<FukuriController> localizer) : base(context, localizer)
        {
        }

        // Post api/fukuri/index
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


        // Post api/fukuri/calc
        [HttpPost(), Route("calc")]
        public ActionResult<Calc> Calc([FromBody]Calc param)
        {
            if (param == null)
            {
                param = new Calc();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                var answer = param.Gnkn;
                var rate = param.Knr;

                if (answer == 0 || rate == 0) {

                    param.Display = answer.ToString();
                    param.ResultList = null;
                    return Ok(param);
                }


                //福利計算
                for (var i = 0; i < 10; i++)
                {
                    answer = answer * (1 + rate / 100);
                    answer = Math.Round(answer);

                }
                param.Display = answer.ToString();
                param.ResultList = new List<ResultList>();
                answer = param.Gnkn;
 
                //年毎の金額明細
                for (var i = 0; i < 31; i++)
                {
                    answer = answer * (1 + rate / 100);
                    var result = new ResultList
                    {
                        Nnm = (i + 1).ToString() + "年目",
                        Kgk = Math.Floor(answer)
                    };
                    param.ResultList.Add(result);

                }

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
