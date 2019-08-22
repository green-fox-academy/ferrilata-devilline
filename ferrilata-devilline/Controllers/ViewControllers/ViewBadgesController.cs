using ferrilata_devilline.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.ViewModels;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public class ViewBadgesController : Controller
    {
        private readonly IBadgeService _badgeService;
        private readonly IUserService _userService;

        public ViewBadgesController(IBadgeService badgeService, IUserService userService)
        {
            _badgeService = badgeService;
            _userService = userService;
        }

        [HttpGet("/badgelibrary")]
        public IActionResult GetBadgeLibrary()
        {
            List<BadgeDTO> badges = _badgeService.GetAllDTO();
            List<User> users = _userService.GetAll();
            var ViewModel = new BadgeLibraryViewModel { Badges = badges, Users = users };

            return View(badges);
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
    }
}