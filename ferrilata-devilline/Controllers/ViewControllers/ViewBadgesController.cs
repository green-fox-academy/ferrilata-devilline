using ferrilata_devilline.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.ViewModels;
using System.Security.Claims;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
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
            string email = User.FindFirstValue(ClaimTypes.Email);
            User user = _userService.FindByEmail(email);
            List<BadgeDTO> badges = _badgeService.GetAllDTO();
            List<User> users = _userService.GetAllExceptFor(user);

            long userId = _userService.FindByEmail(email).UserId;

            var ViewModel = new BadgeLibraryViewModel { Badges = badges, Users = users, User = user};

            return View(ViewModel);
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
            if (ModelState.IsValid)
            {
                _badgeService.AddBadge(newBadge);
            }
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