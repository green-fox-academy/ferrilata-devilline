using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers
{
    public class TokenController : Controller
    {
        [HttpGet("/token")]
        public IActionResult GenerateToken()
        {

        }
    }
}
