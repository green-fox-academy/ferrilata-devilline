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
                return Ok(_badgeService.GetAllDTO());
            }

            return Unauthorized(new {error = "Unauthorized"});
        }

        [HttpDelete]
        [Route("/api/badges/{badgeid}")]
        public IActionResult DeleteBadge(long badgeid)
        {
            var request = Request;

            if (request.Headers.ContainsKey("Authorization") &&
                request.Headers["Authorization"].ToString() != "")
            {
                _badgeService.DeleteById(badgeid);
                return Ok("Deleted");
            }

            return Unauthorized(new { error = "Unauthorized" });
        }
    }
}