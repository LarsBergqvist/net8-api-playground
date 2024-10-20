using Microsoft.AspNetCore.Mvc;

namespace Net8Api.Middleware
{
    public class StatusCodePagesMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);

            if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 600 && !context.Response.HasStarted)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = context.Response.StatusCode,
                    Title = "An error occurred.",
                    Instance = context.Request.Path
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}