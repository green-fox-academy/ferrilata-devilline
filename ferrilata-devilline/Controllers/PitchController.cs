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
        private readonly IPitchService _pitchService;

        public PitchController(IPitchService pitchService)
        {
            _pitchService = pitchService;
        }

        [HttpPost("post/pitch")]
        public IActionResult PostPitch([FromBody] PitchInDTO requestBody)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all fields" });
            }

            JObject toSave = (JObject)JToken.FromObject(requestBody);
            _pitchService.TranslateAndSave(toSave);
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

        [HttpPut("pitch/{id}")]
        public IActionResult PutPitch([FromBody] PitchDTO requestBody, long id)
        {
            requestBody.PitchId = id;
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new { error = "Unauthorized" });
            }

            if (!ModelState.IsValid)
            {
                return NotFound(new { error = "Please provide all fields" });
            }

            JObject toSave = (JObject)JToken.FromObject(requestBody);
            _pitchService.TranslateAndUpdate(toSave);
            return Ok(new { message = "Success" });
        }
    }
}