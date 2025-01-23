using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MusicFestivalManagementSystem.Middleware
{
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Add security headers
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
