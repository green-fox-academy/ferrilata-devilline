using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class PitchController : Controller
    {
        [Route("api/pitches")]
        public IActionResult Return_Pitches()
        {
            if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].ToString().Length != 0)
            {
                return Json(new Pitches());
            }

            return Unauthorized(new Error("Unauthorized"));
        }
    }
}