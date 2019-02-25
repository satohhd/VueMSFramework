using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using VueMSFramework.Data;

namespace Vue2Spa.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext _context;
        protected IStringLocalizer _localizer;

        public HomeController(ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }


        public IActionResult Confirmed()
        {
            //ViewData["Message"] = String.Format("Claims available for the user {0}", (User.FindFirst("name")?.Value));
            //Console.WriteLine("Confirmed");
            return View();
        }
        public IActionResult Defect()
        {
            //ViewData["Message"] = String.Format("Claims available for the user {0}", (User.FindFirst("name")?.Value));
            //Console.WriteLine("Confirmed");
            return View();
        }

        //[Authorize]
        public IActionResult Index()
        {
            Console.WriteLine("run Index");
            return View();
        }
        //[Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = String.Format("Claims available for the user {0}", (User.FindFirst("name")?.Value));
            return View();
        }

        //[Authorize]
        public ActionResult Claims()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [Authorize]
        public IActionResult Api()
        {
            string responseString = "";
            try
            {
                //// Retrieve the token with the specified scopes
                //var scope = AzureAdB2COptions.ApiScopes.Split(' ');
                //string signedInUserID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //TokenCache userTokenCache = new MSALSessionCache(signedInUserID, this.HttpContext).GetMsalCacheInstance();
                //ConfidentialClientApplication cca = new ConfidentialClientApplication(AzureAdB2COptions.ClientId, AzureAdB2COptions.Authority, AzureAdB2COptions.RedirectUri, new ClientCredential(AzureAdB2COptions.ClientSecret), userTokenCache, null);

                //AuthenticationResult result = await cca.AcquireTokenSilentAsync(scope, cca.Users.FirstOrDefault(), AzureAdB2COptions.Authority, false);

                //HttpClient client = new HttpClient();
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, AzureAdB2COptions.ApiUrl);

                //// Add token to the Authorization header and make the request
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                //HttpResponseMessage response = await client.SendAsync(request);

                //// Handle the response
                //switch (response.StatusCode)
                //{
                //    case HttpStatusCode.OK:
                //        responseString = await response.Content.ReadAsStringAsync();
                //        break;
                //    case HttpStatusCode.Unauthorized:
                //        responseString = $"Please sign in again. {response.ReasonPhrase}";
                //        break;
                //    default:
                //        responseString = $"Error calling API. StatusCode=${response.StatusCode}";
                //        break;
                //}
            }
            //catch (MsalUiRequiredException ex)
            //{
            //    responseString = $"Session has expired. Please sign in again. {ex.Message}";
            //}
            catch (Exception ex)
            {
                responseString = $"Error calling API: {ex.Message}";
            }

            ViewData["Payload"] = $"{responseString}";
            return View();
        }


        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View("Error");
        }
    }
}
