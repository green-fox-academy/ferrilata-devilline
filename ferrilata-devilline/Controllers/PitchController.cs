using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
﻿using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Helpers.ObjectTypeCheckers;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Controllers
{
    [Route("api")]
    public class PitchController : Controller
    {
        private readonly JTokenAnalyzer _jTokenAnalyzer;
        private readonly IPitchService _pitchService;

        public PitchController(IPitchService pitchService, JsonSchemaService service)
        {
            _jTokenAnalyzer = new JTokenAnalyzer(service);
            _pitchService = pitchService;
        }

        [HttpPost("post/pitch")]
        public IActionResult PostPitch([FromBody] JToken requestBody)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (_jTokenAnalyzer.FindsMissingFieldsOrValuesIn(requestBody, typeof(PitchInDTO).ToString()))
            {
                return NotFound(new { error = "Please provide all fields" });
            }

            _pitchService.TranslateAndSave(requestBody);
            return Created("", new { message = "Created" });
        }

        [Route("pitches")]
        public IActionResult Return_Pitches()
        {
            string userEmail = "user1 email";

            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Json(_pitchService.GetPitches(userEmail));
            }

            return Unauthorized(new Error("Unauthorized"));
        }

        [HttpPut("pitch")]
        public IActionResult PutPitch([FromBody] JToken requestBody)
        {
            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString() != "" &&
                _jTokenAnalyzer.ConsidersValid(requestBody, typeof(PitchDTO).ToString()))
            {
                _pitchService.TranslateAndUpdate(requestBody);
                return Ok(new { message = "Success" });
            }

            if (_jTokenAnalyzer.ConsidersValid(requestBody, typeof(PitchDTO).ToString()))
            {
                return Unauthorized(new { error = "Unauthorized" });
            }

            return NotFound(new { error = "Please provide all fields" });
        }
    }
}