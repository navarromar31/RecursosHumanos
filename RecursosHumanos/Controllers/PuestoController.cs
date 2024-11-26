using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;

namespace RecursosHumanos.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class PuestoController : Controller
    {

        //private readonly ApplicationDbContext _db;


        //public CategoriaController(ApplicationDbContext db)
        //{
        //    _db = db;  
        //}


        private readonly IPuestoRepositorio _puestoRepo;

        public PuestoController(IPuestoRepositorio puestoRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _puestoRepo = puestoRepo;

        }


        public IActionResult Index()
        {
            IEnumerable<Puesto> lista = _puestoRepo.ObtenerTodos();

            return View(lista);
        }

        //Get
        public IActionResult Crear()
        {


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Puesto puesto)
        {

            if (ModelState.IsValid)
            {
                _puestoRepo.Agregar(puesto);
                _puestoRepo.Grabar();
                TempData[WC.Exitosa] = "Puesto creado exitosamente";
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            TempData[WC.Error] = "Error al crear un nuevo puesto";
            return View(puesto);
        }


        //GET EDITAR QUE RECIBE DE LA VISTA EL ID DE LA CAT A EDITAR
        public IActionResult Editar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var objDep = _puestoRepo.Obtener(Id.GetValueOrDefault());

            if (objDep == null)
            {
                return NotFound();
            }
            return View(objDep);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Puesto puesto)
        {

            if (ModelState.IsValid)
            {
                _puestoRepo.Actualizar(puesto);
                _puestoRepo.Grabar();
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            return View(puesto);
        }



        //GET ELIMINAR
        public IActionResult Eliminar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var objDep = _puestoRepo.Obtener(Id.GetValueOrDefault());

            if (objDep == null)
            {
                return NotFound();
            }
            return View(objDep);
        }

        //POST ELIMINAR


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Puesto puesto)
        {

            if (puesto == null)
            {
                return NotFound();
            }
            _puestoRepo.Remover(puesto);
            _puestoRepo.Grabar();
            return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index

        }



    }
}
