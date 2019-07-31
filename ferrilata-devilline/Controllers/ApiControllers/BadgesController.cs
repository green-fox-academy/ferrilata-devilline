using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ferrilata_devilline.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
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
            return Ok(_badgeService.GetAllDTO());
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}")]
        public IActionResult GetBadgeById(long badgeId)
        {
            if (_badgeService.FindBadge(badgeId) == null)
            {
                return NotFound(new { error = "Please provide an existing Badge Id" });
            }
            return Ok(_badgeService.FindDTOById(badgeId));
        }

        [HttpDelete]
        [Route("/api/badges/{badgeId}")]
        public IActionResult DeleteBadge(long badgeId)
        {
            _badgeService.DeleteById(badgeId);
            return Ok("Deleted");
        }

       [HttpPost]
        [Route("/api/badges/{badgeId}/levels")]
        public IActionResult PostLevelByBadgeId([FromBody] LevelInDTO newLevel, long badgeId)
        {
            if (_badgeService.FindBadge(badgeId) == null)
            {
                return BadRequest(new { error = "Please provide an existing Badge Id" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Please provide all fields" });
            }

            bool isLevelNumberNew = _badgeService.FindBadge(badgeId).Levels.FirstOrDefault(l => l.LevelNumber == newLevel.LevelNumber) == null;

            if (!isLevelNumberNew)
            {
                return BadRequest(new { error = "This badge already has a level of this number" });
            }
            _levelService.AddLevel(badgeId, newLevel);
            return Created("", new { message = "Created" });
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}/levels/{levelId}")]
        public IActionResult GetLevelByIds(long badgeId, long levelId)
        {
            if (_badgeService.FindBadge(badgeId).Levels.Where(l => l.LevelId == levelId) == null)
            {
                return BadRequest(new { error = "Please provide an existing Id pair!" });
            }

            return Ok(_badgeService.FindBadge(badgeId).Levels.FirstOrDefault(l => l.LevelId == levelId));

        }
    }
}
