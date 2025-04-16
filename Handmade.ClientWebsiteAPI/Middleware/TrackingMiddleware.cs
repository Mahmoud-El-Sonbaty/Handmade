using Serilog;

namespace Handmade.ClientWebsiteAPI.Middleware
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;


        public TrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var Page = context.Request.Path;
            var IP = context.Connection.RemoteIpAddress?.ToString();
            var UserAgent = context.Request.Headers["User-Agent"];
            var AccessedAt = DateTime.UtcNow;

            // Logging useing serilog
            Log.Information($"Tracking Info => IP: {IP}, UserAgent: {UserAgent}, Page: {Page}, AccessedAt: {AccessedAt}");
            await _next(context);
        }
    }
}
