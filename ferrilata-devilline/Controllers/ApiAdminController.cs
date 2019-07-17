using ferrilata_devilline.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers
{
   
    public class ApiAdminController : Controller
    {
        [HttpPost("api/admin/add")]
        public IActionResult AddAdmin([FromBody]BadgeInDTO requestBody)
        {
            if (!Request.Headers.ContainsKey("Authorization") ||
                Request.Headers["Authorization"].ToString().Length == 0)
            {
                return Unauthorized(new {message = "Unauthorized"});
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new {error = "Please provide all fields"});
            }

            return Created("/api/badges/1", new List<object> {new {message = "Created"}});
        }
    }
}