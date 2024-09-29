using Microsoft.AspNetCore.Mvc;

namespace RecursosHumanos.Controllers
{
    public class RespuestaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
