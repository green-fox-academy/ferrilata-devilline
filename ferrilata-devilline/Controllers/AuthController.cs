using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            ////security key

            ////symmetric sec kex
            ////signing credentials
            ////return token

            //var token = new JwtSecurityToken

            return Ok("Hello");
        }
    }
}
