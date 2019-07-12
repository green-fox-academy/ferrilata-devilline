using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers.ApiController
{
    public class ApiHeartbeat : Controller
    {
        [HttpGet("/Heartbeat")]
        public IActionResult Heartbeat()
        {
            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Ok(new { status = "OK" });
            }

            return Unauthorized(new { error = "Unauthorized" });
        }
    }
}
