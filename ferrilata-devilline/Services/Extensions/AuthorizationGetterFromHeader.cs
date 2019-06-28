using ferrilata_devilline.Services.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Extensions
{
    public static class AuthorizationGetterFromHeader
    {
        public static string GetAuthorization(this IEnumerator<KeyValuePair<string, StringValues>> headers)
        {
            string authorization = null;
            while (headers.MoveNext())
            {
                if (headers.Current.Key == "Authorization")
                {
                    authorization = headers.Current.Value;
                }
            }
            return authorization;
        }
    }
}
