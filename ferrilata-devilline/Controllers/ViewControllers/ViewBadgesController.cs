using System.Linq;
using ferrilata_devilline.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public class ViewBadgesController : Controller
    {
        private readonly IBadgeService _badgeService;
        private readonly ILevelService _levelService;
        private readonly ISlackMessagingService _slackMessagingService;

        public ViewBadgesController(IBadgeService badgeService, ISlackMessagingService slackMessagingService,
            ILevelService levelService)
        {
            _badgeService = badgeService;
            _slackMessagingService = slackMessagingService;
            _levelService = levelService;
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
            string message = _slackMessagingService.BuildMessage("A new badge has been added by ", User.Identity.Name);
            _slackMessagingService.SendMessage(message);
            return Redirect("/badgelibrary");
        }

        [HttpPost("/badgelibrary/{badgeId}/levels/add")]
        public IActionResult CreateAndAddLevel(long badgeId, LevelInDTO newLevel)
        {
            bool isLevelNumberNew = _badgeService.FindBadgeById(badgeId).Levels
                                        .FirstOrDefault(l => l.LevelNumber == newLevel.LevelNumber) == null;

            if (isLevelNumberNew)
            {
                _levelService.AddLevel(badgeId, newLevel);
                string message =
                    _slackMessagingService.BuildMessage("A new level has been added by ", User.Identity.Name);
                _slackMessagingService.SendMessage(message);
            }

            return Redirect("/badgelibrary");
        }
    }
}