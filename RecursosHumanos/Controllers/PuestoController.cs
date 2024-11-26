using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Models;

public class PuestoController : Controller
{
    private readonly IPuestoRepositorio _puestoRepo;

    public PuestoController(IPuestoRepositorio puestoRepo)
    {
        _puestoRepo = puestoRepo;
    }

    // GET: Index (Lista de instituciones no eliminadas)
    public IActionResult Index()
    {
        var lista = _puestoRepo.ObtenerTodos()
            .Where(i => !i.Eliminada); // Filtrar instituciones no eliminadas
        PuestoVM model = new PuestoVM
        {
            Puesto = lista.ToList() // Pasar la lista filtrada
        };

        return View(model); // Pasar el modelo a la vista
    }

    // Acción GET para ver la Papelera de Instituciones (instituciones eliminadas)
    public IActionResult Papelera()
    {
        var listaEliminada = _puestoRepo.ObtenerPuestosEliminados();  // Aquí debes obtener las instituciones eliminadas
        PuestoVM model = new PuestoVM
        {
            Puesto = listaEliminada.ToList()  // Pasar la lista de instituciones eliminadas a la vista
        };

        return View(model); // Pasar el modelo a la vista de la papelera
    }

    public IActionResult Upsert(int? id)
    {
        Puesto model = new Puesto();

        if (id == null || id == 0)
        {
            // Crear una nueva institución
            return View(model);
        }
        else
        {
            // Editar una institución existente
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
                // Crear una nueva institución
                _puestoRepo.Agregar(model);
            }
            else
            {
                // Actualizar una institución existente
                _puestoRepo.Actualizar(model);
            }

            _puestoRepo.Grabar();
            TempData["Exitosa"] = "Puesto guardada exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }

    // Acción GET para restaurar una institución eliminada
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

        // Restaurar la institución cambiando su estado de eliminada a falso
        puesto.Eliminada = false;
        _puestoRepo.Actualizar(puesto);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto restaurada exitosamente";
        return RedirectToAction(nameof(Papelera));
    }

    // Acción GET para eliminar permanentemente una institución
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

        // Eliminar permanentemente la institución de la base de datos
        _puestoRepo.Remover(puesto);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto eliminada permanentemente";
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

        // Cambiar el estado de Eliminada a true (mover a la papelera)
        obj.Eliminada = true;
        _puestoRepo.Actualizar(obj);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto movida a la papelera";
        return RedirectToAction(nameof(Index));
    }
}
