using Microsoft.AspNetCore.Mvc;

namespace Azurenet.Login
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
