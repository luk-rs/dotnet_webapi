using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Yld.GamingApi.WebApi.Middlewares;

public class ExceptionFormatterMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = ToStatusCode(ex);
            await response.WriteAsync(ToFormattedError(ex));
        }
    }

    private static int ToStatusCode(Exception ex)
    {
        var statusCode = ex switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        return (int)statusCode;
    }

    private static string ToFormattedError(Exception ex)
    {
        var errorObject = new
        {
            message = ex.Message,
            errors = ex switch
            {
                ValidationException e => e.Errors
                .Select(error => new
                {
                    error.PropertyName,
                    error.ErrorMessage
                })
                .ToArray(),
                _ => Array.Empty<dynamic>()
            }
        };

        var errorJson = JsonSerializer.Serialize(errorObject);
        return errorJson;
    }
}

