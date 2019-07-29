using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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

            return Unauthorized(new { error = "Unauthorized" });
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}")]
        public IActionResult GetBadgeById(long badgeId)
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            {
                return Ok(_badgeService.FindBadgeOutDTOById(badgeId));
            }

            return Unauthorized(new { error = "Unauthorized" });
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}/levels")]
        public IActionResult GetBadgeLevelsByBadgeId(long badgeId)
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            {
                return Ok(_badgeService.FindBadgeLevelsByBadgeId(badgeId));
            }

            return Unauthorized(new { error = "Unauthorized" });
        }

        [HttpDelete]
        [Route("/api/badges/{badgeId}/levels/{levelId}")]
        public IActionResult DeleteBadgeLevelById(long badgeId, long levelId)
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            {
                _badgeService.DeleteLevelById(levelId);
                return Ok(new { message = "Deleted" });
            }

            return Unauthorized(new { error = "Unauthorized" });
        }

        [HttpPost]
        [Route("/api/post/badges")]
        public IActionResult PostBadge([FromBody] BadgeInDTO IncomingBadge)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                   Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            _badgeService.AddBadge(IncomingBadge);

            return Created("", new { message = "Created" });
        }
    }
}