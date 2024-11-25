using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;

/*
public class InstitucionController : Controller
{
    private readonly IInstitucionRepositorio _institucionRepo;
    private readonly IWebHostEnvironment _hostEnvironment;

    public InstitucionController(IInstitucionRepositorio institucionRepo, IWebHostEnvironment hostEnvironment)
    {
        _institucionRepo = institucionRepo;
        _hostEnvironment = hostEnvironment;
    }

    // GET: Upsert (Crear o Editar)
    public IActionResult Upsert(int? id)
    {
        Institucion institucion = new Institucion();

        if (id != null && id != 0)
        {
            institucion = _institucionRepo.Obtener(id.GetValueOrDefault());
            if (institucion == null)
            {
                return NotFound();
            }
        }

        return View(institucion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Institucion institucion, IFormFile imagenInstitucion)
    {
        if (ModelState.IsValid)
        {
            // Si se ha subido una imagen, guardarla en el servidor
            if (imagenInstitucion != null)
            {
                // Obtener la ruta de almacenamiento
                string folderPath = Path.Combine(_hostEnvironment.WebRootPath, "imagenes");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Crear la carpeta si no existe
                }

                // Generar un nombre único para la imagen
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenInstitucion.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                // Guardar la imagen en el directorio
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagenInstitucion.CopyTo(stream);
                }

                // Asignar la ruta de la imagen a la propiedad del modelo
                institucion.ImagenUrlInstitucion = "/imagenes/" + fileName;
            }

            // Si el Id es 0, estamos creando una nueva institución
            if (institucion.Id == 0)
            {
                _institucionRepo.Agregar(institucion);
            }
            else
            {
                // Si el Id no es 0, estamos editando una institución existente
                _institucionRepo.Actualizar(institucion);
            }

            // Guardar cambios en la base de datos
            _institucionRepo.Grabar();

            // Mostrar mensaje de éxito
            TempData["Exitosa"] = institucion.Id == 0 ? "Institución creada exitosamente" : "Institución actualizada exitosamente";
            return RedirectToAction(nameof(Index)); // Redirigir a la vista de listado
        }

        // Si ocurre algún error en el modelo
        TempData["Error"] = "Error al guardar la institución";
        return View(institucion);
    }

    // GET: Eliminar
    public IActionResult Eliminar(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        var obj = _institucionRepo.Obtener(Id.GetValueOrDefault());
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    // POST: Eliminar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(Institucion institucion)
    {
        if (institucion == null)
        {
            return NotFound();
        }

        _institucionRepo.Remover(institucion);
        _institucionRepo.Grabar();
        TempData[WC.Exitosa] = "Institución eliminada exitosamente";
        return RedirectToAction(nameof(Index)); // Redirige a la vista de listado
    }

    // GET: Index (Lista de instituciones)
    public IActionResult Index()
    {
        var lista = _institucionRepo.ObtenerTodos();
        InstitucionVM model = new InstitucionVM
        {
            Institucion = lista.ToList() // Pasar la lista de instituciones
        };

        return View(model); // Pasar el modelo a la vista
    }
}
*/

public class InstitucionController : Controller
{
    private readonly IInstitucionRepositorio _institucionRepo;
    private readonly IWebHostEnvironment _hostEnvironment;

    public InstitucionController(IInstitucionRepositorio institucionRepo, IWebHostEnvironment hostEnvironment)
    {
        _institucionRepo = institucionRepo;
        _hostEnvironment = hostEnvironment;
    }

    // Acción GET para Crear
    public IActionResult Crear()
    {
        return View(new Institucion());
    }

    // Acción POST para Crear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Institucion institucion, IFormFile imagenInstitucion)
    {
        if (ModelState.IsValid)
        {
            // Si se ha subido una imagen, guardarla en el servidor
            if (imagenInstitucion != null)
            {
                // Obtener la ruta de almacenamiento
                string folderPath = Path.Combine(_hostEnvironment.WebRootPath, "imagenes");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Crear la carpeta si no existe
                }

                // Generar un nombre único para la imagen
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenInstitucion.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                // Guardar la imagen en el directorio
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagenInstitucion.CopyTo(stream);
                }

                // Asignar la ruta de la imagen a la propiedad del modelo
                institucion.ImagenUrlInstitucion = "/imagenes/" + fileName;
            }

            // Crear la nueva institución
            _institucionRepo.Agregar(institucion);

            // Guardar cambios en la base de datos
            _institucionRepo.Grabar();

            // Mostrar mensaje de éxito
            TempData["Exitosa"] = "Institución creada exitosamente";
            return RedirectToAction(nameof(Index)); // Redirigir a la vista de listado
        }

        // Si ocurre algún error en el modelo
        TempData["Error"] = "Error al guardar la institución";
        return View(institucion);
    }

    // Acción GET para Editar (Upsert)
    public IActionResult Upsert(int? id)
    {
        Institucion institucion = new Institucion();

        if (id != null && id != 0)
        {
            institucion = _institucionRepo.Obtener(id.GetValueOrDefault());
            if (institucion == null)
            {
                return NotFound();
            }
        }

        return View(institucion);
    }

    // Acción POST para Editar (Upsert)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Institucion institucion, IFormFile imagenInstitucion)
    {
        if (ModelState.IsValid)
        {
            // Si se ha subido una imagen, guardarla en el servidor
            if (imagenInstitucion != null)
            {
                // Obtener la ruta de almacenamiento
                string folderPath = Path.Combine(_hostEnvironment.WebRootPath, "imagenes");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Crear la carpeta si no existe
                }

                // Generar un nombre único para la imagen
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenInstitucion.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                // Guardar la imagen en el directorio
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imagenInstitucion.CopyTo(stream);
                }

                // Asignar la ruta de la imagen a la propiedad del modelo
                institucion.ImagenUrlInstitucion = "/imagenes/" + fileName;
            }

            // Si el Id es 0, estamos creando una nueva institución
            if (institucion.Id == 0)
            {
                _institucionRepo.Agregar(institucion);
            }
            else
            {
                // Si el Id no es 0, estamos editando una institución existente
                _institucionRepo.Actualizar(institucion);
            }

            // Guardar cambios en la base de datos
            _institucionRepo.Grabar();

            // Mostrar mensaje de éxito
            TempData["Exitosa"] = institucion.Id == 0 ? "Institución creada exitosamente" : "Institución actualizada exitosamente";
            return RedirectToAction(nameof(Index)); // Redirigir a la vista de listado
        }

        // Si ocurre algún error en el modelo
        TempData["Error"] = "Error al guardar la institución";
        return View(institucion);
    }

    // GET: Eliminar
    public IActionResult Eliminar(int? Id)
    {
        if (Id == null || Id == 0)
        {
            return NotFound();
        }

        var obj = _institucionRepo.Obtener(Id.GetValueOrDefault());
        if (obj == null)
        {
            return NotFound();
        }

        return View(obj);
    }

    // POST: Eliminar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(Institucion institucion)
    {
        if (institucion == null)
        {
            return NotFound();
        }

        _institucionRepo.Remover(institucion);
        _institucionRepo.Grabar();
        TempData[WC.Exitosa] = "Institución eliminada exitosamente";
        return RedirectToAction(nameof(Index)); // Redirige a la vista de listado
    }

    // GET: Index (Lista de instituciones)
    public IActionResult Index()
    {
        var lista = _institucionRepo.ObtenerTodos();
        InstitucionVM model = new InstitucionVM
        {
            Institucion = lista.ToList() // Pasar la lista de instituciones
        };

        return View(model); // Pasar el modelo a la vista
    }
}
