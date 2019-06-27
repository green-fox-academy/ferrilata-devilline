using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class ApiController : Controller
    {
        [Route("api/pitches")]
        public JsonResult Return_Pitches()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.ContainsKey("Authorization"))
            {
                return Json(new Pitches());
            }
            else
            {
                Response.StatusCode = 401;
                return Json(new Error("Unauthorizied"));
            }
        }
    }
}