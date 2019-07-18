using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    public class ViewBadgesController : Controller
    {
        private readonly IBadgeService _badgeService;

        public ViewBadgesController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet("/badgelibrary")]
        public IActionResult GetBadgeLibrary()
        {
            return View(_badgeService.GetAll());
        }
    }
}