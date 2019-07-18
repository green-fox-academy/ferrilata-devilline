using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
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
                return Ok(_badgeService.FindById(badgeId));
            
        }
    }
}
