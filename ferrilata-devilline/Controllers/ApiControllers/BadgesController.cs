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

        public BadgesController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
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

        [HttpPut]
        [Route("/api/badges/{badgeId}/levels/{levelId}")]
        public IActionResult UpdateBadgeLevel([FromBody] BadgeDTO badgeDTO, long badgeId, long levelId)
        {
            return Ok("Updated");
        }
    }
}