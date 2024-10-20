using Microsoft.AspNetCore.Mvc;

namespace Net8Api.Middleware
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ProblemDetails problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = exception.Message
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Resource not found.";
                    problemDetails.Detail = notFoundException.Message;
                    break;

                case UnauthorizedException unauthorizedException:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Title = "Unauthorized access.";
                    problemDetails.Detail = unauthorizedException.Message;
                    break;
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}