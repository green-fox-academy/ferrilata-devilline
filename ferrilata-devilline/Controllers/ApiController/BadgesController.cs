using ferrilata_devilline.Models;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ferrilata_devilline.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BadgesController : Controller
    {
        private readonly IBadgeService _badgeService;

        public BadgesController(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        [Route("/api/badges")]
        public IActionResult getGadgets()
        {
            return Ok(_badgeService.GetAll());
        }

        
    }
}
