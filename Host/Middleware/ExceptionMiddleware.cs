using Application.Common.Exceptions;

namespace WebApi.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthenticatedException => StatusCodes.Status401Unauthorized,
            UnauthorizedAccessException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        var response = new
        {
            error = context.Response.StatusCode == StatusCodes.Status500InternalServerError  
                ? "Internal server error" : exception.Message,
            statusCode = context.Response.StatusCode,
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}