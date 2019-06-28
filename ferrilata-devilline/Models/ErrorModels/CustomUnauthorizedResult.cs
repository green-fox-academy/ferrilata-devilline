using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ferrilata_devilline.Models.ErrorModels
{
    public class CustomUnauthorizedResult : JsonResult
    {
        public CustomUnauthorizedResult(string message)
            : base(new CustomError(message)) // I am calling constructor of base JsonReuslt class that takes object and has StatusCode property
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
