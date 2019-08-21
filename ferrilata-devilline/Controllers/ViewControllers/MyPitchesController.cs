using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers.ViewControllers
{
    [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
    public class MyPitchesController : Controller
    {
        [HttpGet("/mypitches")]
        public IActionResult getMyPitches()
        {
            return View();
        }

        [HttpPost("/mypitches/newpitch")]
        public IActionResult createPitch()
        {
            
            return Redirect("/badgelibrary");
        }
    }
}
