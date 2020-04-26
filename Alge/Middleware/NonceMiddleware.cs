using Alge.Domain.Interfaces.Infastructure;
using Alge.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Alge.Middleware
{
    public class NonceMiddleware
    {
        private readonly RequestDelegate _next;
        public string Nonce { get; set; }

        public NonceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment hostingEnvironment)
        {
            var settings = (ISettings)context.RequestServices.GetService(typeof(ISettings));
            var nonceService = (INonceService)context.RequestServices.GetService(typeof(INonceService));
            var nonceString = string.Empty;
            Nonce = nonceService.GetNonce();
            nonceString = string.Format(settings.CSP, Nonce);

            context.Response.OnStarting((state) => {
                if (context.Response.ContentType != null && context.Response.ContentType.Contains("text/html;"))
                {
                    context.Response.Headers.Add("Content-Security-Policy", nonceString);
                }
                return Task.FromResult(0);
            }, null);

            await _next.Invoke(context);
        }
    }
}
