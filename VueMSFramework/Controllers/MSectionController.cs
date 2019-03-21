using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using VueMSFramework.Data;
using VueMSFramework.ViewModels.MSection;

namespace VueMSFramework.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MSectionController : PageController<MSectionController>
    {
        public MSectionController(ApplicationDbContext context, IStringLocalizer<MSectionController> localizer) : base(context, localizer)
        {
        }

        //[HttpPost(), Route("index/load")]
        //public ActionResult<Index> Load([FromBody]Index param)
        //{
        //    if (param == null)
        //    {
        //        param = new Index();
        //        param._message = _localizer["No parameters."];
        //        return BadRequest(param);
        //    }
        //    try
        //    {
        //        return Ok(param);
        //    }
        //    catch (Exception ex)
        //    {
        //        param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
        //        return BadRequest(param);
        //    }
        //}


        [HttpPost(), Route("section1/load")]
        public ActionResult<Section1> Load([FromBody]Section1 param)
        {
            if (param == null)
            {
                param = new Section1();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                param.Item1 = (Guid.NewGuid()).ToString();
                return Ok(param);
   
            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("section2/load")]
        public ActionResult<Section2> Load([FromBody]Section2 param)
        {
            if (param == null)
            {
                param = new Section2();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                param.Item1 = (Guid.NewGuid()).ToString();

                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("section3/load")]
        public ActionResult<Section3> Load([FromBody]Section3 param)
        {
            if (param == null)
            {
                param = new Section3();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                param.Item1 = (Guid.NewGuid()).ToString();
                return Ok(param);

            }
            catch (Exception ex)
            {
                param._message = _localizer["System error Please inform system personnel.({0})", ex.Message];
                return BadRequest(param);
            }
        }

        [HttpPost(), Route("section4/load")]
        public ActionResult<Section3> Load([FromBody]Section4 param)
        {
            if (param == null)
            {
                param = new Section4();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            try
            {
                param.Item1 = (Guid.NewGuid()).ToString();

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
