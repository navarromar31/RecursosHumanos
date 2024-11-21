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
        // private readonly ApplicationDbContext _db;

        private readonly ICapacitacionRepositorio _capacitacionRepo;// Capacitacion capacitacion
        private readonly IColaboradorRepositorio _colaboradorRepo;//Colaborador colaborador
        private readonly IEvaluacionRepositorio _evaluacionRepo;// Evaluacion evaluacion

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