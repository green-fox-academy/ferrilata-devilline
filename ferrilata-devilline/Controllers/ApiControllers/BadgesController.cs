using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
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
             return Ok(_badgeService.GetAll());
        }

        [HttpGet]
        [Route("/api/badges/{badgeId}")]
        public IActionResult GetBadgeById(long badgeId)
        {
            if (_badgeService.FindById(badgeId) == null)
            {
                return NotFound(new { error = "Please provide an existing Badge Id" });
            }
            return Ok(_badgeService.FinDTOById(badgeId));
        }

        [HttpPost]
        [Route("/api/badges/{badgeId}/levels")]
        public IActionResult PostLevelByBadgeId([FromBody] LevelInDTO newLevel, long badgeId)
        {
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
