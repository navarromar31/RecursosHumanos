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
    [Authorize(Roles = WC.AdminRole)]
    public class InstitucionController : Controller
    {
        private readonly IInstitucionRepositorio _institucionRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InstitucionController(IInstitucionRepositorio institucionRepo, IWebHostEnvironment webHostEnvironment)
        {
            _institucionRepo = institucionRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Institucion> lista = _institucionRepo.ObtenerTodos();
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
        public IActionResult Crear(Institucion institucion, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes/institucion");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    institucion.ImagenUrlInstitucion = $"imagenes/institucion{fileName}";
                }

                _institucionRepo.Agregar(institucion);
                _institucionRepo.Grabar();
                TempData["Exitosa"] = "Institución creada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Error al crear nueva institución";
            return View(institucion);
        }

        // GET Editar
        public IActionResult Editar(int? id)
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

            return View(obj);
        }

        // POST Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Institucion institucion, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes/institucion");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Borrar la imagen anterior
                    if (!string.IsNullOrEmpty(institucion.ImagenUrlInstitucion))
                    {
                        var previousFile = Path.Combine(_webHostEnvironment.WebRootPath, institucion.ImagenUrlInstitucion);
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

                    institucion.ImagenUrlInstitucion = $"imagenes/institucion{fileName}";
                }

                _institucionRepo.Actualizar(institucion);
                _institucionRepo.Grabar();
                TempData["Exitosa"] = "Institución actualizada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            return View(institucion);
        }

        // GET Eliminar
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

            return View(obj);
        }

        // POST Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Institucion institucion)
        {
            if (institucion == null)
            {
                return NotFound();
            }

            // Borrar la imagen asociada
            if (!string.IsNullOrEmpty(institucion.ImagenUrlInstitucion))
            {
                var fileToDelete = Path.Combine(_webHostEnvironment.WebRootPath, institucion.ImagenUrlInstitucion);
                if (System.IO.File.Exists(fileToDelete))
                {
                    System.IO.File.Delete(fileToDelete);
                }
            }

            _institucionRepo.Remover(institucion);
            _institucionRepo.Grabar();
            TempData["Exitosa"] = "Institución eliminada exitosamente";
            return RedirectToAction(nameof(Index));
        }
    }
}

