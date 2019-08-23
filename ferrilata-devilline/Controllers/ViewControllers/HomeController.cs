using System.Threading.Tasks;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    public class HomeController : Controller
    {
        private readonly ISlackMessagingService _slackMessagingService;

        public HomeController(ISlackMessagingService slackMessagingService)
        {
            _slackMessagingService = slackMessagingService;
        }

        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        [HttpGet("/index")]
        public IActionResult Index()
        {
            string testMessage = _slackMessagingService.BuildMessage();
            _slackMessagingService.SendMessage(testMessage);
            return View();
        }

        [HttpPost("/signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            return Redirect(
                "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:5001/index");
        }
    }
}