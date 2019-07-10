using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Controllers
{
    [Route("api")]
    public class PitchController : Controller
    {
        private readonly IPitchService _pitchService;

        public PitchController(IPitchService pitchService)
        {
            _pitchService = pitchService;
        }

        [HttpPost("post/pitch")]
        public IActionResult PostPitch([FromBody] AuxPitch NewPitch)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new {message = "Unauthorized"});
            }

            if (HelperMethods.HelperMethods.checkMissingPostedPitchFields(NewPitch))
            {
                return NotFound(new {error = "Please provide all fields"});
            }

            return Created("", new {message = "Created"});
        }

        [Route("pitches")]
        public IActionResult Return_Pitches()
        {
            string userEmail = "mockUserEmail";

            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Json(_pitchService.GetPitches(userEmail));
            }

            return Unauthorized(new Error("Unauthorized"));
        }

        [HttpPut("pitch")]
        public IActionResult PutPitch([FromBody] Pitch pitchToUpdate)
        {           
            if ((Request.Headers.ContainsKey("Authorization")) && (Request.Headers["Authorization"].ToString() != "") && (HelperMethods.HelperMethods.checkIAllFieldsArePresent(pitchToUpdate)))
            {
                _pitchService.Save(pitchToUpdate);
                return Ok(new { message = "Success" });
            }
            if (HelperMethods.HelperMethods.checkIAllFieldsArePresent(pitchToUpdate))
            {
                return Unauthorized(new { error = "Unauthorized" });
            }
            return NotFound(new { error = "Please provide all fields" });
        }

        
    }
}