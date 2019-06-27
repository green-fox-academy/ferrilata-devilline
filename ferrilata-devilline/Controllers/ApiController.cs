using ferrilata_devilline.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        [HttpPost("api/admin/add")]
        public IActionResult AddAdmin([FromHeader] string Authorization, [FromBody]JObject data)
        {
            if (Authorization != null && Authorization != "")
            {
                if (data == null)
                {
                    var missingBodyResult = new ObjectResult(new { error = "Missing body" });
                    missingBodyResult.StatusCode = 400; 
                    return missingBodyResult;
                }
                else if (data.HasMissingFields() || data.HasNullValues()) // what if it has too many fields?
                {
                    var missingFieldResult = new ObjectResult(new { error = "Please provide all fields" });
                    missingFieldResult.StatusCode = 422; //404 is recommended, but that means that 
                                                         //we don't have the resource, while it's the client's fault
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