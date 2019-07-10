﻿using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            if ((Request.Headers.ContainsKey("Authorization")) && (Request.Headers["Authorization"].ToString() != "") && (HelperMethods.HelperMethods.checkIAllFieldsArePresent(pitchToUpdate)))
            {
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