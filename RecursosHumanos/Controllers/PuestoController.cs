@*using Microsoft.AspNetCore.Authorization;
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
*@
using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;

public class PuestoController : Controller
{
    private readonly IPuestoRepositorio _puestoRepo;

    public PuestoController(IPuestoRepositorio puestoRepo)
    {
        _puestoRepo = puestoRepo;
    }

    // GET: Index (Lista de puestos no eliminados)
    public IActionResult Index()
    {
        var lista = _puestoRepo.ObtenerTodos()
            .Where(p => !p.Eliminado); // Filtrar puestos no eliminados
        PuestoVM model = new PuestoVM
        {
            Puesto = lista.ToList() // Pasar la lista filtrada
        };

        return View(model); // Pasar el modelo a la vista
    }

    // Acción GET para ver la Papelera de Puestos (puestos eliminados)
    public IActionResult Papelera()
    {
        var puestosEliminados = _puestoRepo.ObtenerTodos()
            .Where(p => p.Eliminado); // Filtrar puestos eliminados
        return View(puestosEliminados); // Pasar los puestos eliminados a la vista
    }

    public IActionResult Upsert(int? id)
    {
        Puesto model = new Puesto();

        if (id == null || id == 0)
        {
            // Crear un nuevo puesto
            return View(model);
        }
        else
        {
            // Editar un puesto existente
            model = _puestoRepo.Obtener(id.GetValueOrDefault());
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }

    // Acción POST para guardar los cambios
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Puesto model)
    {
        if (ModelState.IsValid)
        {
            if (model.Id == 0)
            {
                // Crear un nuevo puesto
                _puestoRepo.Agregar(model);
            }
            else
            {
                // Actualizar un puesto existente
                _puestoRepo.Actualizar(model);
            }

            _puestoRepo.Grabar();
            TempData["Exitosa"] = "Puesto guardado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // Acción GET para restaurar un puesto eliminado
    public IActionResult Restaurar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var puesto = _puestoRepo.Obtener(id.GetValueOrDefault());
        if (puesto == null)
        {
            return NotFound();
        }

        // Restaurar el puesto cambiando su estado de eliminado a falso
        puesto.Eliminado = false;
        _puestoRepo.Actualizar(puesto);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto restaurado exitosamente";
        return RedirectToAction(nameof(Papelera));
    }

    // Acción GET para eliminar permanentemente un puesto
    public IActionResult EliminarPermanente(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var puesto = _puestoRepo.Obtener(id.GetValueOrDefault());
        if (puesto == null)
        {
            return NotFound();
        }

        // Eliminar permanentemente el puesto de la base de datos
        _puestoRepo.Remover(puesto);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto eliminado permanentemente";
        return RedirectToAction(nameof(Papelera));
    }

    // GET: Eliminar (Mover a papelera)
    public IActionResult Eliminar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var obj = _puestoRepo.Obtener(id.GetValueOrDefault());
        if (obj == null)
        {
            return NotFound();
        }

        // Cambiar el estado de Eliminado a true (mover a la papelera)
        obj.Eliminado = true;
        _puestoRepo.Actualizar(obj);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto movido a la papelera";
        return RedirectToAction(nameof(Index));
    }
}
