using ferrilata_devilline.Models;
using ferrilata_devilline.HelperMethods;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{

    [Route("api")]
    public class PitchController : Controller
    {
        [HttpPost("post/pitch")]
        public IActionResult PostPitch([FromBody] Pitch NewPitch)
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
    }
}