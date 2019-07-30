using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

//        [HttpPost]
//        [Route("/api/badges/{badgeId}/levels")]
//        public IActionResult PostLevelByBadgeId([FromBody] LevelInDTO newLevel, long badgeId)
//        {
//            if (_badgeService.FindBadge(badgeId) == null)
//            {
//                return NotFound(new { error = "Please provide an existing Badge Id" });
//            }
//
//            if (!ModelState.IsValid)
//            {
//                return NotFound(new { error = "Please provide all fields" });
//            }
//
//            _levelService.AddLevel(badgeId, newLevel);
//            return Created("", new { message = "Created" });
//        }

        [HttpDelete]
        [Route("/api/badges/{badgeId}")]
        public IActionResult DeleteBadge(long badgeId)
        {
            _badgeService.DeleteById(badgeId);
            return Ok("Deleted");
        }

        [HttpPut]
        [Route("/api/badges/{badgeId}/levels/{levelId}")]
        public IActionResult UpdateBadgeLevel([FromBody] LevelInDTO levelInDTO, long badgeId, long levelId)
        {
            if (!_badgeService.FindBadge(badgeId).Levels.Contains(_levelService.FindById(levelId)))
            {
                return NotFound(new {error = "No such level found for the selected badge"});
            }
            
            return Ok("Updated");
        }
    }
}