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
            return Ok(_badgeService.GetAllDTO());
        }

        [HttpDelete]
        [Route("/api/badges/{badgeid}")]
        public IActionResult DeleteBadge(long badgeid)
        {
            _badgeService.DeleteById(badgeid);
            return Ok("Deleted");
        }
    }
}