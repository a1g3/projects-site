using Microsoft.AspNetCore.Mvc;

namespace Alge.Controllers
{
    public class CryptoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}