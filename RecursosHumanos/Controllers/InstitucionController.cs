using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;

public class InstitucionController : Controller
{
    private readonly IInstitucionRepositorio _institucionRepo;
    private readonly IWebHostEnvironment _hostEnvironment;

    public InstitucionController(IInstitucionRepositorio institucionRepo, IWebHostEnvironment hostEnvironment)
    {
        _institucionRepo = institucionRepo;
        _hostEnvironment = hostEnvironment;
    }

    // GET: Index (Lista de instituciones no eliminadas)
    public IActionResult Index()
    {
        var lista = _institucionRepo.ObtenerTodos()
            .Where(i => !i.Eliminada); // Filtrar instituciones no eliminadas
        InstitucionVM model = new InstitucionVM
        {
            Institucion = lista.ToList() // Pasar la lista filtrada
        };

        return View(model); // Pasar el modelo a la vista
    }

    private string GuardarImagen(IFormFile imagenInstitucion)
    {
        string rutaPrincipal = _hostEnvironment.WebRootPath;
        string subCarpeta = @"imagenes\instituciones";
        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagenInstitucion.FileName);
        string rutaCompleta = Path.Combine(rutaPrincipal, subCarpeta);

        if (!Directory.Exists(rutaCompleta))
        {
            Directory.CreateDirectory(rutaCompleta);
        }

        string rutaArchivo = Path.Combine(rutaCompleta, nombreArchivo);

        using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
        {
            imagenInstitucion.CopyTo(fileStream);
        }

        // Devolver la ruta relativa para almacenarla en la base de datos
        return Path.Combine(subCarpeta, nombreArchivo).Replace("\\", "/");
    }

    // Acción GET para ver la Papelera de Instituciones (instituciones eliminadas)
    public IActionResult Papelera()
    {
        var listaEliminada = _institucionRepo.ObtenerInstitucionesEliminadas();  // Aquí debes obtener las instituciones eliminadas
        InstitucionVM model = new InstitucionVM
        {
            Institucion = listaEliminada.ToList()  // Pasar la lista de instituciones eliminadas a la vista
        };

        return View(model); // Pasar el modelo a la vista de la papelera
    }


    public IActionResult Upsert(int? id)
    {
        Institucion model = new Institucion();

        if (id == null || id == 0)
        {
            // Crear una nueva institución
            return View(model);
        }
        else
        {
            // Editar una institución existente
            model = _institucionRepo.Obtener(id.GetValueOrDefault());
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
    public IActionResult Upsert(Institucion model, IFormFile imagenInstitucion)
    {
        if (ModelState.IsValid)
        {
            if (imagenInstitucion != null)
            {
                // Usar el método GuardarImagen para almacenar la nueva imagen
                model.ImagenUrlInstitucion = GuardarImagen(imagenInstitucion);
            }
            else if (model.Id != 0)
            {
                // Mantener la imagen existente si no se subió una nueva
                var institucionExistente = _institucionRepo.Obtener(model.Id);
                if (institucionExistente != null)
                {
                    model.ImagenUrlInstitucion = institucionExistente.ImagenUrlInstitucion;
                }
            }

            if (model.Id == 0)
            {
                // Crear una nueva institución
                _institucionRepo.Agregar(model);
            }
            else
            {
                // Actualizar una institución existente
                _institucionRepo.Actualizar(model);
            }

            _institucionRepo.Grabar();
            TempData["Exitosa"] = "Institución guardada exitosamente.";
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

        var institucion = _institucionRepo.Obtener(id.GetValueOrDefault());
        if (institucion == null)
        {
            return NotFound();
        }

        // Restaurar la institución cambiando su estado de eliminada a falso
        institucion.Eliminada = false;
        _institucionRepo.Actualizar(institucion);
        _institucionRepo.Grabar();

        TempData["Exitosa"] = "Institución restaurada exitosamente";
        return RedirectToAction(nameof(Papelera));
    }

    // Acción GET para eliminar permanentemente una institución
    public IActionResult EliminarPermanente(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var institucion = _institucionRepo.Obtener(id.GetValueOrDefault());
        if (institucion == null)
        {
            return NotFound();
        }

        // Eliminar permanentemente la institución de la base de datos
        _institucionRepo.Remover(institucion);
        _institucionRepo.Grabar();

        TempData["Exitosa"] = "Institución eliminada permanentemente";
        return RedirectToAction(nameof(Papelera));
    }

    // GET: Eliminar (Mover a papelera)
    public IActionResult Eliminar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var obj = _institucionRepo.Obtener(id.GetValueOrDefault());
        if (obj == null)
        {
            return NotFound();
        }

        // Cambiar el estado de Eliminada a true (mover a la papelera)
        obj.Eliminada = true;
        _institucionRepo.Actualizar(obj);
        _institucionRepo.Grabar();

        TempData["Exitosa"] = "Institución movida a la papelera";
        return RedirectToAction(nameof(Index));
    }
}
