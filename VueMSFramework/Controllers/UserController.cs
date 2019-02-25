using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vue2Spa.Controllers;
using Vue2SpaSignalR.Models.Auth;
using Vue2SpaSignalR.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vue2SpaSignalR.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginUser userParam)
        {
            var user = _userService.Authenticate(userParam.UserName, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
