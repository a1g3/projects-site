using Microsoft.AspNetCore.Mvc;

namespace Alge.Controllers
{
    [Route("tls-client")]
    public class TlsClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
