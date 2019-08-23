using ferrilata_devilline.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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

        [HttpPost("/badgelibrary/delete/{badgeId}")]
        public IActionResult DeleteBadgeFromLibrary(long badgeId)
        {
            _badgeService.DeleteById(badgeId);
            return Redirect("/badgelibrary");
        }

        [HttpPost("/badgelibrary/add")]
        public IActionResult CreateAndAddBadge(BadgeInDTO newBadge)
        {
            _badgeService.AddBadge(newBadge);
            return Redirect("/badgelibrary");
        }

        [HttpPost("/UpdateBadge")]
        public IActionResult UpdateBadge(BadgeDTO badge)
        {
            _badgeService.UpdateBadgeFromForm(badge);
            return Redirect("/badgeLibrary");
        }
    }
}