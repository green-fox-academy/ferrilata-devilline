using ferrilata_devilline.Models;
using ferrilata_devilline.Models.ErrorModels;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{

    [Route("api")]
    public class PitchController : Controller
    {


        public PitchController()
        {
        }

        //[HttpPost("post/pitch")]
        //public IActionResult PostFromPostman([FromBody] PostingObject NewObject)
        //{
        //    var request = Request;
        //    var aders = request.Headers;

        //    if (!hders.ContainsKey("Authorization")
        //        || hders["Authorization"].ToString().Length == 0) return new CustomUnauthorizedResult("Unauthorized");


        //    if (NewObjt == null
        //        || NewObct.BadgeName == null
        //        || NewObct.Holders == null
        //        || NewObje.OldLVL == 0
        //        || NewObjectitchedLVL = 0
        //        || NObject.PitchMessage == null) return NotFound(new { error = "Please provide all fields" });

        //    return Ok(ne{ message = "Created" });
        //}

        [HttpPost("post/pitch")]
        public IActionResult PostFromPostmanWithoutCustomUnauthorizedError([FromBody] Pitch NewPitch)
        {
            var request = Request;
            var headers = request.Headers;

            if (!headers.ContainsKey("Authorization")
                || headers["Authorization"].ToString().Length == 0) return Unauthorized(new { message = "Unauthorized" });


            if (NewPitch == null
                || NewPitch.BadgeName == null
                || NewPitch.Holders == null
                || NewPitch.OldLVL == 0
                || NewPitch.PitchedLVL == 0
                || NewPitch.PitchMessage == null) return NotFound(new { error = "Please provide all fields" });

            return Ok(new { message = "Created" });
        }
    }
}