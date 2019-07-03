using ferrilata_devilline.Models;
using ferrilata_devilline.HelperMethods;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{

    [Route("api")]
    public class PitchController : Controller
    {
        [HttpPost("post/pitch")]
        public IActionResult PostPitch([FromBody] AuxPitch NewPitch)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            if (HelperMethods.HelperMethods.checkMissingPostedPitchFields(NewPitch))
            {
                return NotFound(new { error = "Please provide all fields" });
            }
            return Created("", new { message = "Created" });

        }

        [Route("pitches")]
        public IActionResult Return_Pitches()
        {
            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Json(new Pitches());
            }

            return Unauthorized(new Error("Unauthorized"));
        }

        [HttpPut]
        [Route("/api/pitch")]
        public IActionResult PutPitch([FromBody] Pitch pitchToUpdate)
        {
            var request = Request;
           
            if ((request.Headers.ContainsKey("Authorization")) && (request.Headers["Authorization"].ToString() != "") && (verifyPitch(pitchToUpdate)))
            {
                //update pitch
                return Ok(new { message = "Success" });
            }
            else if (verifyPitch(pitchToUpdate))
            {
                return Unauthorized(new { error = "Unauthorized" });
            }
            return NotFound(new { error = "Please provide all fields" });
        }

        public bool verifyPitch(Pitch pitch)
        {
            bool result = true;
            if (pitch.Username == null ||
                pitch.BadgeName == null ||
                pitch.Status == null ||
                pitch.PitchMessage == null)
            {
                result = false;
            }
            return result;
        }
    }
}