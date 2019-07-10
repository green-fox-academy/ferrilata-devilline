using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Controllers
{
    public class TokenController : Controller
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService TokenService)
        {
            _tokenService = TokenService;
        }

        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        [HttpGet("/token")]
        public IActionResult PostPitch()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(_tokenService.GenerateToken(email));
        }
    }
}
