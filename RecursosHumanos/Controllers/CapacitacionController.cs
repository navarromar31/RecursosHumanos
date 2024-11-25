using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_Models;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using System.Linq;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Utilidades;

namespace RecursosHumanos.Controllers
{
    public class CapacitacionController : Controller
    {
        private readonly ICapacitacionRepositorio _capacitacionRepo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CapacitacionController(ICapacitacionRepositorio capacitacionRepo, IWebHostEnvironment hostEnvironment)
        {
            _capacitacionRepo = capacitacionRepo;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Upsert (Crear o Editar)
        public IActionResult Upsert(int? id)
        {
            Capacitacion capacitacion = new Capacitacion();

            if (id != null && id != 0)
            {
                capacitacion = _capacitacionRepo.Obtener(id.GetValueOrDefault());
                if (capacitacion == null)
                {
                    return NotFound();
                }
            }

            return View(capacitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Capacitacion capacitacion, IFormFile imagenInstitucion)
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
}
*