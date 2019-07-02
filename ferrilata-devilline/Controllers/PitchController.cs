﻿using ferrilata_devilline.Models;
using ferrilata_devilline.Services.Interfaces;
using ferrilata_devilline.HelperMethods;
using Microsoft.AspNetCore.Mvc;

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
                return Unauthorized(new { message = "Unauthorized" });
            }
            if (HelperMethods.HelperMethods.checkMissingPostedPitchFields(NewPitch))
            {
                return NotFound(new { error = "Please provide all fields" });
            }

            return Created("", new { message = "Created" });
        }

        [HttpGet("pitches")]
        public IActionResult Return_Pitches()
        {
            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Json(_pitchService.GetPitches());
            }

            return Unauthorized(new Error("Unauthorized"));
        }
    }
}