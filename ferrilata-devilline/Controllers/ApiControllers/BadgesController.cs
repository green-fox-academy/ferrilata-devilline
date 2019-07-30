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

        [HttpPost]
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
    }
}