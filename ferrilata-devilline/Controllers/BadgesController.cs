using ferrilata_devilline.Models;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers
{
    public class BadgesController : Controller
    {
        private readonly IBadgeService _badgeService;

        public BadgesController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        [Route("/api/badges")]
        public IActionResult getGadgets()
        {
            var request = Request;

            if ((request.Headers.ContainsKey("Authorization")) &&
                (request.Headers["Authorization"].ToString() != ""))
            { 
                return Ok(_badgeService.GetAndTranslateAll());
            }
            return Unauthorized(new { error = "Unauthorized" });
        }
    }
}
