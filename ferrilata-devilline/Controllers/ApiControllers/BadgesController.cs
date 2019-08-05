using ferrilata_devilline.Models.DTOs;
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

        [HttpDelete]
        [Route("/api/badges/{badgeId}")]
        public IActionResult DeleteBadge(long badgeId)
        {
            _badgeService.DeleteById(badgeId);
            return Ok("Deleted");
        }

        [HttpDelete]
        [Route("/api/badges/{badgeId}/levels/{levelId}")]
        public IActionResult DeleteBadge(long badgeId, long levelId)
        {
            var requestedBadge = _badgeService.FindBadgeById(badgeId);
            var requestedLevel = _levelService.FindLevelById(levelId);

            if (!requestedBadge.Levels.Contains(requestedLevel))
            {
                return BadRequest(new { error = "Requested Badge does not contain requested Level" });
            }

            _levelService.DeleteById(levelId);
            return Ok("Deleted");
        }

        [HttpPost]
        [Route("/api/badges/{badgeId}/levels")]
        public IActionResult PostLevelByBadgeId([FromBody] LevelInDTO newLevel, long badgeId)
        {
            if (_badgeService.FindBadgeById(badgeId) == null)
            {
                return BadRequest(new { error = "Please provide an existing Badge Id" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Please provide all fields" });
            }

            bool isLevelNumberNew = _badgeService.FindBadgeById(badgeId).Levels.FirstOrDefault(l => l.LevelNumber == newLevel.LevelNumber) == null;

            if (!isLevelNumberNew)
            {
                return BadRequest(new { error = "This badge already has a level of this number" });
            }
            _levelService.AddLevel(badgeId, newLevel);
            return Created("", new { message = "Created" });
        }

        [Route("/api/post/badges")]
        public IActionResult PostBadge([FromBody] BadgeInDTO IncomingBadge)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all files" });
            }
            _badgeService.AddBadge(IncomingBadge);

            return Created("", new { message = "Created" });
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}/levels")]
        public IActionResult GetLevelsBadgeById(long badgeId)
        {
            if (_badgeService.FindBadgeById(badgeId) == null)
            {
                return NotFound(new { error = "Please provide an existing Badge Id" });
            }
            return Ok(_badgeService.FinLevelsDTOByBadgeId(badgeId));
        }

        [HttpPut]
        [Route("/api/badges/{badgeId}/levels/{levelId}")]
        public IActionResult UpdateBadgeLevel([FromBody] LevelInDTO levelInDTO, long badgeId, long levelId)
        {
            if (!_badgeService.FindBadgeById(badgeId).Levels.Contains(_levelService.FindLevelById(levelId)))
            {
                return NotFound(new { error = "No such level found for the selected badge" });
            }

            return Ok(new { message = "Updated" });
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}")]
        public IActionResult GetBadgeById(long badgeId)
        {
            if (_badgeService.FindBadgeById(badgeId) == null)
            {
                return NotFound(new { error = "Please provide an existing Badge Id" });
            }
            return Ok(_badgeService.FindDTOById(badgeId));
        }
    }
}
