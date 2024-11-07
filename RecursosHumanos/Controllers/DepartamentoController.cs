using RecursosHumanos_AccesoDatos;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecursosHumanos.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class DepartamentoController : Controller
    {

        //private readonly ApplicationDbContext _db;


        //public CategoriaController(ApplicationDbContext db)
        //{
        //    _db = db;  
        //}


        private readonly IDepartamentoRepositorio _departametoRepo;

        public DepartamentoController(IDepartamentoRepositorio departamentoRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _departametoRepo = departamentoRepo;

        }


        public IActionResult Index()
        {
            IEnumerable<Departamento> lista = _departametoRepo.ObtenerTodos();

            return View(lista);
        }

        //Get
        public IActionResult Crear()
        {


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Departamento departamento)
        {

            if (ModelState.IsValid)
            {
                _departametoRepo.Agregar(departamento);
                _departametoRepo.Grabar();
                TempData[WC.Exitosa] = "Departamento creado exitosamente";
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            TempData[WC.Error] = "Error al crear un nuevo departamento";
            return View(departamento);
        }


        //GET EDITAR QUE RECIBE DE LA VISTA EL ID DE LA CAT A EDITAR
        public IActionResult Editar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var objDep = _departametoRepo.Obtener(Id.GetValueOrDefault());

            if (objDep == null)
            {
                return NotFound();
            }
            return View(objDep);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Departamento departamento)
        {

            if (ModelState.IsValid)
            {
                _departametoRepo.Actualizar(departamento);
                _departametoRepo.Grabar();
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            return View(departamento);
        }



        //GET ELIMINAR
        public IActionResult Eliminar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var objDep = _departametoRepo.Obtener(Id.GetValueOrDefault());

            if (objDep == null)
            {
                return NotFound();
            }
            return View(objDep);
        }

        //POST ELIMINAR


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Departamento departamento)
        {

            if (departamento == null)
            {
                return NotFound();
            }
            _departametoRepo.Remover(departamento);
            _departametoRepo.Grabar();
            return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index

        }



    }
}
