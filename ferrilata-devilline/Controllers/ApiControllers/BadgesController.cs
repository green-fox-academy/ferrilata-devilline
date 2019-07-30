using ferrilata_devilline.Repositories;
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
        private readonly IBadgeRepository _badgeRepository;

        public BadgesController(IBadgeService badgeService, IBadgeRepository badgeRepository)
        {
            _badgeService = badgeService;
            _badgeRepository = badgeRepository;
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

        [HttpGet]
        [Route("/api/badges/{badgeId}")]
        public IActionResult GetBadgeById(long badgeId)
        {
            if (_badgeService.FindBadge(badgeId) == null)
            {
                return NotFound(new { error = "Please provide an existing Badge Id" });
            }
            return Ok(_badgeService.FinDTOById(badgeId));
        }
    }
}
