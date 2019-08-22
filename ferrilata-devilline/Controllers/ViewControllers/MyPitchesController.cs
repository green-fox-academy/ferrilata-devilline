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
            return View();
        }

        [HttpPost("/createpitch/{levelId}")]
        public IActionResult createpitch(long levelId, PitchInDTO incomingPitch)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            if (_userService.IsNewUser(email))
            {
                _userService.Add(new User { Name = User.Identity.Name, Email = email });
            }
 
            var user = _userService.FindByEmail(email);
            _pitchService.SavePitchFromPitchInDTO(levelId, user, incomingPitch);

            return Redirect("/badgelibrary");
        }
    }
}
