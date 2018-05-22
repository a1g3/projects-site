using Alge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Alge.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var apps = new List<ApplicationViewModel>();
            Startup.Configuration.GetSection("Applications").Bind(apps);
            return View(apps);
        }
    }
}