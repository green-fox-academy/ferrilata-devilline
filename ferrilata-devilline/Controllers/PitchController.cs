using System.Linq;
using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class PitchController : Controller
    {
        [Route("api/pitches")]
        public IActionResult Return_Pitches()
        {
            var re = Request;

            var headers = re.Headers;
            if (headers.ContainsKey("Authorization") && headers["Authorization"].ToString().Length != 0)
            {
                return Json(new Pitches());
            }

            Response.StatusCode = 401;
            return Json(new Error("Unauthorized"));
        }
    }
}