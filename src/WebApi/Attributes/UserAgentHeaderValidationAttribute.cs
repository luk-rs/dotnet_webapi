using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Yld.GamingApi.WebApi.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class UserAgentHeaderValidationAttribute : ActionFilterAttribute
{

    // ! I'm going with an attribute approach here so I don't add an additional call stack entry to the request
    // ! I'm assuming that only selected methods should validate this behavior
    // ! If, in fact, this is a cross api concern then should be moved into a middleware to make sure we never forget to enforce it
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("User-Agent"))
        {
            context.Result = new BadRequestObjectResult("Missing 'User-Agent' header");
        }

        base.OnActionExecuting(context);
    }
}
