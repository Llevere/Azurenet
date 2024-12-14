using Microsoft.AspNetCore.Mvc;

namespace Azurenet.Controllers
{
    public class JWT : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
