using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        [HttpGet("/index")]
        public IActionResult Index()
        {
            return View(User.Identity.IsAuthenticated ? "Index" : "Error");
        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult ReturnErrorPage()
//        {
//            return View("Error", new Error {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
//        }
    }
}