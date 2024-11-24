using Microsoft.AspNetCore.Mvc;

namespace RecursosHumanos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
