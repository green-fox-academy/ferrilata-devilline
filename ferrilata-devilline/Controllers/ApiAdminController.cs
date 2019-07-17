using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
                return Unauthorized(new { message = "Unauthorized" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Please provide all fields" });
            }

            return Created("/api/badges/1", new List<object> { new { message = "Created" } });


            //string authorization = Request.GetAuthorization();

            //if (authorization != null && authorization != "")
            //{
            //    if (requestBody == null || requestBody.HasMissingFieldsOrValuesAsAdmin())
            //    {
            //       return BadRequest(new { error = "Please provide all fields" });
            //    }

            //    return Created("/api/badges/1", new List<object> { new { message = "Created" } });
            //}

            //return Unauthorized(Json(new { error = "Unauthorized" }));
        }
    }
}