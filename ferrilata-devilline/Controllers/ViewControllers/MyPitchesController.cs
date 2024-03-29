﻿using ferrilata_devilline.Models.DAOs;
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

        public MyPitchesController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpGet("/mypitches")]
        public IActionResult getMyPitches()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var user = _userService.FindByEmail(email);

            return View(user);
        }

        [HttpPost("/createpitch/{levelId}")]
        public IActionResult createpitch(long levelId, PitchInDTO pitchInDTO)
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            if (_userService.IsNewUser(email))
            {
                _userService.Add(new User { Name = User.Identity.Name, Email = email });
            }
            return Redirect("/badgelibrary");
        }
    }
}
