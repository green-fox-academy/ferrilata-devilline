using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
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
            var listsDTOs = _badgeService.GetAllDTO();
            return View(listsDTOs);
        }

        [HttpPost("/UpdateBadge")]
        public IActionResult UpdateBadge(BadgeDTO badge)
        {
            
            _badgeService.UpdateBadge(badge);
            return Redirect("/badgeLibrary");
        }
    }
}//long BadgeId, double Version, string Name, string Tag