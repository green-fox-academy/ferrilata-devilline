using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Extensions
{
    public static class AuthorizationGetterFromHeader
    {
        public static string GetAuthorization(this HttpRequest request)
        {
            var headers = request.Headers;
            return headers.ContainsKey("Authorization") ? (string)headers["Authorization"] : null;
        }
    }
}
