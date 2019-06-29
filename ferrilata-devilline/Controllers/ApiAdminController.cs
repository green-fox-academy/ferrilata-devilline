using ferrilata_devilline.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers
{
    [ApiController]
    public class ApiAdminController : Controller
    {
        [HttpPost("api/admin/add")]
        public IActionResult AddAdmin([FromBody]JObject data)
        {
            var header = Request.Headers;
            string authorization = header.ContainsKey("Authorization") ? (string)header["Authorization"] : null;

            if (authorization != null && authorization != "")
            {
                if (data == null || data.HasMissingFieldsAsAdmin() || data.HasNullValuesAsAdmin()) 
                {
                    var missingFieldResult = new ObjectResult(new { error = "Please provide all fields" });
                    missingFieldResult.StatusCode = 400; 
                    return missingFieldResult;
                }

                var createdResult = new { message = "Created" };
                List<object> responseList = new List<object>() { createdResult };
                var result = new ObjectResult(responseList);
                result.StatusCode = 201;
                return result;
            }

            return Unauthorized(Json(new { error = "Unauthorized" }));
        }
    }
}