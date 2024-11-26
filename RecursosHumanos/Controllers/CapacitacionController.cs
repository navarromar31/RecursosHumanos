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
        public IActionResult Upsert(Capacitacion capacitacion, IFormFile imagenCapacitacion)
        {
            if (ModelState.IsValid)
            {
                // Manejo de imagen (solo si se sube una nueva)
                if (imagenCapacitacion != null)
                {
                    string folderPath = Path.Combine(_hostEnvironment.WebRootPath, "imagenes");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenCapacitacion.FileName);
                    string filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imagenCapacitacion.CopyTo(stream);
                    }

                    capacitacion.ImagenUrlCap = "/imagenes/" + fileName;
                }
                else if (capacitacion.Id != 0) // Conservar la imagen actual al editar
                {
                    var capacitacionEnDb = _capacitacionRepo.Obtener(capacitacion.Id);
                    capacitacion.ImagenUrlCap = capacitacionEnDb.ImagenUrlCap;
                }

                // Lógica para agregar o actualizar
                if (capacitacion.Id == 0)
                {
                    _capacitacionRepo.Agregar(capacitacion);
                }
                else
                {
                    _capacitacionRepo.Actualizar(capacitacion);
                }

                _capacitacionRepo.Grabar();
                TempData["Exitosa"] = capacitacion.Id == 0 ? "Capacitación creada exitosamente" : "Capacitación actualizada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Error al guardar la capacitación";
            return View(capacitacion);
        }




        // GET: Eliminar
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _capacitacionRepo.Obtener(Id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST: Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Capacitacion capacitacion)
        {
            if (capacitacion == null)
            {
                return NotFound();
            }

            _capacitacionRepo.Remover(capacitacion);
            _capacitacionRepo.Grabar();
            TempData[WC.Exitosa] = "Capacitacion eliminada exitosamente";
            return RedirectToAction(nameof(Index)); // Redirige a la vista de listado
        }

        // GET: Index (Lista de instituciones)
        public IActionResult Index()
        {
            var lista = _capacitacionRepo.ObtenerTodos();
            CapacitacionVM model = new CapacitacionVM
            {
                Capacitacion = lista.ToList() // Pasar la lista de instituciones
            };

            return View(model); // Pasar el modelo a la vista


        }
    }
}
