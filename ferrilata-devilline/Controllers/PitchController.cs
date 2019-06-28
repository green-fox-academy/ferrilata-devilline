using ferrilata_devilline.Models;
using ferrilata_devilline.Models.ErrorModels;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{

    [Route("api")]
    public class ApiController : Controller
    {


        public ApiController()
        {
        }

        //[HttpPost("post/pitch")]
        //public IActionResult PostFromPostman([FromBody] PostingObject NewObject)
        //{
        //    var request = Request;
        //    var headers = request.Headers;

        //    if (!headers.ContainsKey("Authorization")
        //        || headers["Authorization"].ToString().Length == 0) return new CustomUnauthorizedResult("Unauthorized");


        //    if (NewObject == null
        //        || NewObject.BadgeName == null
        //        || NewObject.Holders == null
        //        || NewObject.OldLVL == 0
        //        || NewObject.PitchedLVL == 0
        //        || NewObject.PitchMessage == null) return NotFound(new { error = "Please provide all fields" });

        //    return Ok(new { message = "Created" });
        //}

        [HttpPost("post/pitch")]
        public IActionResult PostFromPostmanWithoutCustomUnauthorizedError([FromBody] PostingObject NewObject)
        {
            var request = Request;
            var headers = request.Headers;

            if (!headers.ContainsKey("Authorization")
                || headers["Authorization"].ToString().Length == 0) return Unauthorized(new { message = "Unauthorized" });


            if (NewObject == null
                || NewObject.BadgeName == null
                || NewObject.Holders == null
                || NewObject.OldLVL == 0
                || NewObject.PitchedLVL == 0
                || NewObject.PitchMessage == null) return NotFound(new { error = "Please provide all fields" });

            return Ok(new { message = "Created" });
        }
    }
}