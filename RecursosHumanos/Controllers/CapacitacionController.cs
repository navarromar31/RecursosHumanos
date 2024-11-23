using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_Models;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_Utilidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using NuGet.Packaging.Signing;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;


namespace RecursosHumanos.Controllers
{
    public class CapacitacionController : Controller
    {
        private readonly ICapacitacionRepositorio _capacitacionRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CapacitacionController(ICapacitacionRepositorio capacitacionRepo, IWebHostEnvironment webHostEnvironment)
        {
            _capacitacionRepo = capacitacionRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Capacitacion> lista = _capacitacionRepo.ObtenerTodos();
            return View(lista);
        }

        // GET Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Capacitacion capacitacion, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes/capacitacion");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    capacitacion.ImagenUrlCap = $"imagenes/capacitacion{fileName}";
                }

                _capacitacionRepo.Agregar(capacitacion);
                _capacitacionRepo.Grabar();
                TempData["Exitosa"] = "Capacitación creada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Error al crear nueva capacitación";
            return View(capacitacion);
        }

        // GET Editar
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _capacitacionRepo.Obtener(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Capacitacion capacitacion, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes/capacitacion");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Borrar la imagen anterior
                    if (!string.IsNullOrEmpty(capacitacion.ImagenUrlCap))
                    {
                        var previousFile = Path.Combine(_webHostEnvironment.WebRootPath, capacitacion.ImagenUrlCap);
                        if (System.IO.File.Exists(previousFile))
                        {
                            System.IO.File.Delete(previousFile);
                        }
                    }

                    // Guardar la nueva imagen
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    capacitacion.ImagenUrlCap = $"imagenes/capacitacion{fileName}";
                }

                _capacitacionRepo.Actualizar(capacitacion);
                _capacitacionRepo.Grabar();
                TempData["Exitosa"] = "Capacitación actualizada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            return View(capacitacion);
        }

        // GET Eliminar
        public IActionResult Eliminar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _capacitacionRepo.Obtener(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Capacitacion capacitacion)
        {
            if (capacitacion == null)
            {
                return NotFound();
            }

            // Borrar la imagen asociada
            if (!string.IsNullOrEmpty(capacitacion.ImagenUrlCap))
            {
                var fileToDelete = Path.Combine(_webHostEnvironment.WebRootPath, capacitacion.ImagenUrlCap);
                if (System.IO.File.Exists(fileToDelete))
                {
                    System.IO.File.Delete(fileToDelete);
                }
            }

            _capacitacionRepo.Remover(capacitacion);
            _capacitacionRepo.Grabar();
            TempData["Exitosa"] = "Capacitación eliminada exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
