using Microsoft.AspNetCore.Http;

namespace ferrilata_devilline.Services.Helpers
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
