using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers.ViewControllers
{

    public class ViewBadgesController : Controller
    {
        private readonly IBadgeAndLevelService _badgeService;

        public ViewBadgesController(IBadgeAndLevelService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet("/badgelibrary")]
        public IActionResult GetBadgeLibrary()
        {
            return View(_badgeService.GetAndTranslateToBadgeDTOAll());
        }
    }
}