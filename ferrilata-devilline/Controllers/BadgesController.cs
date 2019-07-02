﻿using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers
{
    public class BadgesController : Controller
    {
        [HttpGet]
        [Route("/api/badges")]
        public IActionResult getGadgets()
        {
            var request = Request;

            if ((request.Headers.ContainsKey("Authorization")) &&
                (request.Headers["Authorization"].ToString() != ""))
            { 
                return Ok(new BadgesBase());
            }
            return Unauthorized(new { error = "Unauthorized" });
        }
    }
}