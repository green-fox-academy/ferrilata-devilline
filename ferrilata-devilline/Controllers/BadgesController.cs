using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Extensions;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Controllers
{
    public class BadgesController : Controller
    {
        JTokenAnalyzer _jTokenAnalyzer;
        private readonly IBadgeAndLevelService _badgeService;

        public BadgesController(JsonSchemaService SchemaService, IBadgeAndLevelService badgeService)
        {
            _jTokenAnalyzer = new JTokenAnalyzer(SchemaService);
            _badgeService = badgeService;
        }

        [HttpGet]
        [Route("/api/badges")]
        public IActionResult getBadges()
        {
            var request = Request;

            if ((request.Headers.ContainsKey("Authorization")) &&
                (request.Headers["Authorization"].ToString() != ""))
            { 
                return Ok(_badgeService.GetAndTranslateToBadgeDTOAll());
            }
            return Unauthorized(new { error = "Unauthorized" });
        }

        [HttpPost("api/badges")]
        public IActionResult AddAdmin([FromBody]JToken requestBody)
        {
            string authorization = Request.GetAuthorization();
            string expectedType = typeof(BadgeInDTO).ToString();

            if (authorization != null && authorization != "")
            {
                if (requestBody == null || _jTokenAnalyzer.FindsMissingFieldsOrValuesIn(requestBody, expectedType))
                {
                    return BadRequest(new { error = "Please provide all fields" });
                }

                _badgeService.TranslateAndSave(requestBody);
                return Created("/api/badges/1", new List<object> { new { message = "Created" } });
            }

            return Unauthorized(Json(new { error = "Unauthorized" }));
        }
    }
}
