using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetBadges()
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            {
                return Ok(_badgeService.GetAll());
            }

            return Unauthorized(new {error = "Unauthorized"});
        }

        [HttpPut("/api/badges/{badgeId}")]
        public IActionResult PutBadge([FromBody]BadgeDTO badge, long badgeId)
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            { 
                if (badgeId != badge.BadgeId)
                {
                    return BadRequest(new { message = "Please provide a single Badge ID" });
                }

                if (_badgeService.BadgeExists(badgeId))
                {
                    _badgeService.TranslateAndUpdateBadgeFrom(badge);
                    return Ok(new { message = "Updated" });
                }

                return NotFound(new { message = "Please provide an existing Badge ID" });
            }

            return Unauthorized(new { error = "Unauthorized" });
        }
    }
}