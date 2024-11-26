using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_ViewModels.ViewModels.RecursosHumanos_Models.ViewModels;
using RecursosHumanos_ViewModels.ViewModels.RecursosHumanos_Models;
using RecursosHumanos_Utilidades;
using RecursosHumanos_ViewModels.ViewModels;
using RecursosHumanos_ViewModels;

public class PuestoController : Controller
{
    private readonly IPuestoRepositorio _puestoRepo;
    private readonly IWebHostEnvironment _hostEnvironment;

    public PuestoController(IPuestoRepositorio puestoRepo, IWebHostEnvironment hostEnvironment)
    {
        _puestoRepo = puestoRepo;
        _hostEnvironment = hostEnvironment;
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
            string rutaPrincipal = _hostEnvironment.WebRootPath;
            

     

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


