using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using ferrilata_devilline.Models.DAOs;
using System.Collections.Generic;

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
            List<Badge> badges = _badgeService.GetAll();
            return View(badges);
        }
    }
}