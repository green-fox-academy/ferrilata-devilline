using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Controllers
{
    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    [Route("api")]
    public class PitchController : Controller
    {
        private readonly IPitchService _pitchService;

        public PitchController(IPitchService pitchService)
        {
            _pitchService = pitchService;
        }

        [HttpPost("post/pitch")]
        public IActionResult PostPitch([FromBody] Pitch NewPitch)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all fields" });
            }

            return Created("", new { message = "Created" });
        }

        [HttpGet("pitches")]
        public IActionResult ReturnPitches()
        {
            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Json(_pitchService.GetPitches());
            }

            return Unauthorized(new Error("Unauthorized"));
        }

        [HttpPut("pitch")]
        public IActionResult PutPitch([FromBody] Pitch pitchToUpdate)
        {

            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all fields" });
            }

            return Ok(new { message = "Success" });
        }
    }
}