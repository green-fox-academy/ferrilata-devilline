
using ferrilata_devilline.Services.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using ferrilata_devilline.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ferrilata_devilline.Controllers
{

    [Authorize(AuthenticationSchemes =
    JwtBearerDefaults.AuthenticationScheme)]
    public class ApiAdminController : Controller
    {
        [HttpPost("api/admin/add")]
        public IActionResult AddAdmin([FromBody]BadgeInDTO requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Please provide all fields" });
            }

            return Created("/api/badges/1", new List<object> { new { message = "Created" } });
        }
    }
}