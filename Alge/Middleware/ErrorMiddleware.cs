using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Alge.Middleware
{
    public class ErrorMiddleware
    {
        readonly RequestDelegate _next;
        public readonly ILogger<ErrorMiddleware> Logger;


        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);;
            }
            catch (Exception ex) {
                LoggerExtensions.LogError(Logger, ex, "");
            }
        }
    }
}
