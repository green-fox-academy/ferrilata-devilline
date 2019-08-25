using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public class MyPitchesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBadgeService _badgeService;
        private readonly IPitchService _pitchService;

        public MyPitchesController(IUserService userService, IBadgeService badgeService, IPitchService pitchService)
        {
            _userService = userService;
            _badgeService = badgeService;
            _pitchService = pitchService;
        }

        [HttpGet("/mypitches")]
        public IActionResult getMyPitches()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = _userService.FindByEmail(email);

            return View(user);
        }

        [HttpPost("/createpitch/{levelId}")]
        public IActionResult createpitch(long levelId, long badgeId, long userId, PitchInDTO incomingPitch)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            if (_userService.IsNewUser(email))
            {
                _userService.Add(new User { Name = User.Identity.Name, Email = email });
            }
 
            var user = _userService.FindByEmail(email);
            var reviewer = _userService.FindById(userId);
            
            //if (_userService.IsThereLevelFromSameBadge(badgeId, user))
            //{
            //    Level pitchLevel = _userService.GetLevelFromSameBadge(badgeId, user);
            //    incomingPitch.Level = pitchLevel;
            //}
            _pitchService.SavePitchFromPitchInDTO(levelId, user, reviewer, incomingPitch);

            return Redirect("/badgelibrary");
        }
    }
}
