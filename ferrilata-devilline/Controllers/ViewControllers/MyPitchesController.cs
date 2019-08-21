using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public class MyPitchesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBadgeService _badgeService;

        public MyPitchesController(IUserService userService, IBadgeService badgeService)
        {
            _userService = userService;
            _badgeService = badgeService;
        }

        [HttpGet("/mypitches")]
        public IActionResult getMyPitches()
        {
            return View();
        }

        [HttpPost("/createpitch")]
        public IActionResult createpitch()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            if (_userService.IsNewUser(email))
            {
                _userService.Add(new User { Name = User.Identity.Name, Email = email });
            }
            return Redirect("/badgelibrary");
        }

        [HttpPost("/UpdateBadge")]
        public IActionResult UpdateBadge(BadgeDTO badge)
        {
            
            return Redirect("/badgeLibrary");
        }
    }
}
