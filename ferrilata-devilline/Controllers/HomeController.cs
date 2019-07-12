
using System.Threading.Tasks;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPitchService _pitchService;

        public HomeController(IPitchService pitchService)
        {
            _pitchService = pitchService;
        }

        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        [HttpGet("/index")]
        public IActionResult Index()
        {
            _pitchService.SendMessageToSlack("New pitch has been created by ", User.Identity.Name);
            return View(User.Identity.IsAuthenticated ? "Index" : "Error");
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