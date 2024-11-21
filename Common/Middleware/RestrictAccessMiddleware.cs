using Microsoft.AspNetCore.Http;

namespace Common.Middleware
{
    /// <summary>
    /// Middleware for autorization process.
    /// </summary>
    public class RestrictAccessMiddleware
    {
        private RequestDelegate _next;
        public RestrictAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var referrer = context.Request.Headers["Referrer"].FirstOrDefault();

            if (string.IsNullOrEmpty(referrer))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Direct call of the service is forbidden.");
                return;
            }

            await _next(context);
        }
    }
}
