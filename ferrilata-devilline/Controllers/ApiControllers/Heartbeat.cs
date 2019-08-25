using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ferrilata_devilline.Controllers.ApiControllers
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