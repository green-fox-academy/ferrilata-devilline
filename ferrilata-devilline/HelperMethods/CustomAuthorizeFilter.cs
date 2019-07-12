using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
{
    public AuthorizationPolicy Policy { get; }

    public CustomAuthorizeFilter(AuthorizationPolicy policy)
    {
        Policy = policy ?? throw new ArgumentNullException(nameof(policy));
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Allow Anonymous skips all authorization
        if (context.Filters.Any(Item = &amp; gt; item is IAllowAnonymousFilter))
        {
            return;
        }

        var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService & amp; lt; IPolicyEvaluator & amp; gt; ();
        var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
        var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

        if (authorizeResult.Challenged)
        {
            // Return custom 401 result
            context.Result = new CustomUnauthorizedResult("Authorization failed.");
        }
        else if (authorizeResult.Forbidden)
        {
            // Return default 403 result
            context.Result = new ForbidResult(Policy.AuthenticationSchemes.ToArray());
        }
    }
}
public class CustomUnauthorizedResult : JsonResult
{
    public CustomUnauthorizedResult(string message)
        : base(new CustomError(message))
    {
        StatusCode = StatusCodes.Status401Unauthorized;
    }
}
public class CustomError
{
    public string Error { get; }

    public CustomError(string message)
    {
        Error = message;
    }
}