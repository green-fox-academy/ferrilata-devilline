using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ferrilata_devilline.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
    public class ApiHeartbeat : Controller
    {
        [HttpGet("/Heartbeat")]
        public IActionResult Heartbeat()
        {
            return Ok(new {status = "OK"});
        }
    }
}