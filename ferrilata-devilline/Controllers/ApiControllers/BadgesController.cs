using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
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
                return Ok(_badgeService.GetAll());
            }

            return Unauthorized(new {error = "Unauthorized"});
        }
    }
}


//[
//{
//    "id": 123,
//    "version": "2.3",
//    "name": "Process improver/initator",
//    "tag": "general",
//    "levels": [
//    {
//        "id": 12,
//        "level": 1,
//        "weight": 2,
//        "description": "I can see through processes and propose relevant and doable ideas for improvement. I can create improved definition / accountibility / documentation and communicate it to the team",
//        "holders": [
//        {
//            "id": 45,
//            "name": "balazs.barna"
//        },
//        ...
//            ]
//    },
//    ...
//        ]