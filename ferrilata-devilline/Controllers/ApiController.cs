using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        [HttpPost("/api/add-assignee")]
        public IActionResult AddAssignee([FromHeader] string Authorization, [FromBody]JObject data)
        {
            if (Authorization != null && Authorization != "")
            { 
                var messageObject = new { message = "Created" };
                List<object> responseList = new List<object>();
                responseList.Add(messageObject);

                var result = new ObjectResult(responseList);
                result.StatusCode = 201;
                return result;
            }
            else
            {
                return Unauthorized( Json( new { error = "Unauthorized" }));
            }
        }
    }
}