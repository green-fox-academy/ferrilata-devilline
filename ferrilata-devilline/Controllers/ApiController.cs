using System.Linq;
using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class RestController : Controller
    {
        [Route("api/pitches")]
        public JsonResult Return_Pitches()
        {
            var re = Request;

            var headers = re.Headers;
            if (headers.ContainsKey("Authorization") && (headers.First(h => h.Key == "Authorization").Value != "" ||
                                                         !(headers.First(h => h.Key == "Authorization").Value
                                                             .ToString() is null)))
                return Json(new Pitches());
            else
            {
                Response.StatusCode = 401;
                return Json(new Error("Unauthorizied"));
            }
        }
    }
}