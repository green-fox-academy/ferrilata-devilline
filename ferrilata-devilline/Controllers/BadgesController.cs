using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Helpers;
using ferrilata_devilline.Services.Helpers.ObjectTypeCheckers;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Controllers
{
    public class BadgesController : Controller
    {
        private readonly IBadgeAndLevelService _badgeService;

        public BadgesController(IBadgeAndLevelService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        [Route("/api/badges")]
        public IActionResult getBadges()
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            {
                return Ok(_badgeService.GetAndTranslateToBadgeDTOAll());
            }
            return Unauthorized(new { error = "Unauthorized" });
        }

        [HttpPost("api/badges")]
        public IActionResult AddBadge([FromBody]BadgeInDTO requestBody)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Please provide all fields" });
            }

            JObject toSave = (JObject)JToken.FromObject(requestBody);
            _badgeService.TranslateAndSave(toSave);
            return Created("/api/badges/1", new List<object> { new { message = "Created" } });
        }
    }
}
