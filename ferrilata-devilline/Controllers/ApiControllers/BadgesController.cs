using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class BadgesController : Controller
    {
        private readonly IBadgeService _badgeService;
        private readonly ILevelService _levelService;

        public BadgesController(IBadgeService badgeService, ILevelService levelService)
        {
            _badgeService = badgeService;
            _levelService = levelService;
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

        [HttpGet]
        [Route("/api/badges/{badgeId}")]
        public IActionResult GetBadgeById(long badgeId)
        {
            var request = Request;

            if (!(request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != ""))
            {
                return Unauthorized(new { error = "Unauthorized" });
            }
            else if (_badgeService.FindById(badgeId) == null)
            {
                return NotFound(new { error = "Please provide an existing Badge Id" });
            }
            return Ok(_badgeService.FinDTOById(badgeId));
        }

        [HttpPost]
        [Route("/api/badges/{badgeId}/levels")]
        public IActionResult PostLevelByBadgeId([FromBody] LevelInDTO newLevel, long badgeId)
        {
            var request = Request;

            if (!(request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != ""))
            {
                return Unauthorized(new {error = "Unauthorized"});
            }

            if (_badgeService.FindById(badgeId) == null)
            {
                return NotFound(new {error = "Please provide an existing Badge Id"});
            }

            if (!ModelState.IsValid)
            {
                return NotFound(new {error = "Please provide all fields"});
            }

            _levelService.AddLevel(badgeId, newLevel);
            return Created("", new { message = "Created" });


        }
    }
}
