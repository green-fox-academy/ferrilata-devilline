using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Controllers
{
    [ApiController]
    public class ApiAdminController : Controller
    {
        JTokenAnalyzer _jTokenAnalyzer;

        public ApiAdminController(JsonSchemaService service)
        {
            _jTokenAnalyzer = new JTokenAnalyzer(service);
        }

        [HttpPost("api/admin/add")] // POST api badges
        public IActionResult AddAdmin([FromBody]JToken requestBody)
        {
            string authorization = Request.GetAuthorization();

            if (authorization != null && authorization != "")
            {
                if (requestBody == null || _jTokenAnalyzer.FindsMissingFieldsOrValuesIn(requestBody, typeof(BadgeInDTO).ToString()))
                {
                   return BadRequest(new { error = "Please provide all fields" });
                }

                return Created("/api/badges/1", new List<object> { new { message = "Created" } });
            }

            return Unauthorized(Json(new { error = "Unauthorized" }));
        }
    }
}