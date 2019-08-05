using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using ferrilata_devilline.Models.DAOs;

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

        //[HttpPost("/UpdateBadge")]
        //public IActionResult UpdateBadge(long BadgeId)
        //{
        //    Badge badge = 
        //    return View()
        //}
    }
}