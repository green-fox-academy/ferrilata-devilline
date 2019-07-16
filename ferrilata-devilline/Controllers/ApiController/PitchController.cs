using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all fields" });
            }
            return Created("", new {message = "Created"});
        }

        [Route("pitches")]
        public IActionResult Return_Pitches()
        {  
            return Json(_pitchService.GetPitches());
        }

        [HttpPut("pitch")]
        public IActionResult PutPitch([FromBody] Pitch pitchToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all fields" });
            }
            return Created("", new { message = "Success" });
        }   
    }
}