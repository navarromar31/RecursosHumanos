using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models;
using System.Diagnostics;
using RecursosHumanos_Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_Utilidades;
using RecursosHumanos_Models.ViewModels.RecursosHumanos_Models.ViewModels;

namespace RecursosHumanos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICapacitacionRepositorio _capacitacionRepo;
        private readonly IColaboradorRepositorio _colaboradorRepo;
        private readonly IEvaluacionRepositorio _evaluacionRepo;

        public HomeController(ILogger<HomeController> logger, ICapacitacionRepositorio capacitacionRepo,
            IColaboradorRepositorio colaboradorRepo, IEvaluacionRepositorio evaluacionRepo)
        {
            _logger = logger;
            _capacitacionRepo = capacitacionRepo;
            _colaboradorRepo = colaboradorRepo;
            _evaluacionRepo = evaluacionRepo;
        }

        public IActionResult Index()
        {
            // Verificar el rol del usuario y asignar el Layout correspondiente
            if (User.IsInRole("userManager"))
            {
                ViewData["Layout"] = "_LayoutUserManager"; // Layout para el rol userManager
            }
            else if (User.IsInRole("userStore"))
            {
                ViewData["Layout"] = "_LayoutUserStore"; // Layout para el rol userStore
            }
            else
            {
                ViewData["Layout"] = "_Layout"; // Layout predeterminado
            }

            // Crear el ViewModel y cargar los datos
            HomeVM homeVM = new HomeVM()
            {
                Capacitacion = _capacitacionRepo.ObtenerTodos(incluirPropiedades: "Colaborador"),
                Colaborador = _colaboradorRepo.ObtenerTodos(incluirPropiedades: "Puesto,Institucion,Departamento"),
                Evaluacion = _evaluacionRepo.ObtenerTodos()
            };

            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}