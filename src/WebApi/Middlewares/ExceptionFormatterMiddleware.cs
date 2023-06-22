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
        static HttpStatusCode readStatusCodeFromValidationException(ValidationException ex)
        {
            const string key = nameof(HttpStatusCode);
            if (!ex.Data.Contains(key))
                return HttpStatusCode.InternalServerError;

            var value = ex.Data[key];

            if (value == default)
                return HttpStatusCode.InternalServerError;

            return value is HttpStatusCode code ? code : HttpStatusCode.InternalServerError;
        }

        var statusCode = ex switch
        {
            ValidationException e => readStatusCodeFromValidationException(e),
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

