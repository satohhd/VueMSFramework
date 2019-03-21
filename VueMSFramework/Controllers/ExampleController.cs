using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using VueMSFramework.Data;
using VueMSFramework.ViewModels.Example;

namespace VueMSFramework.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : PageController<ExampleController>
    {
        public ExampleController(ApplicationDbContext context, IStringLocalizer<ExampleController> localizer) : base(context, localizer)
        {
        }

        // Post api/shain/search/load
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

                return Ok(param);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(param);
            }
        }


        // Post api/shain/create/load
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

        // Post api/shain/edit
        [HttpPost(), Route("edit/load")]
        public ActionResult<Edit> Load([FromBody]Edit param)
        {
            if (param == null)
            {
                param = new Edit();
                param._message = _localizer["No parameters."];
                return BadRequest(param);
            }
            return Ok(param);

        }

    }
}
