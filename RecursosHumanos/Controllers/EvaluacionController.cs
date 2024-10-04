using Microsoft.AspNetCore.Mvc;
using RecursosHumanos.Datos;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.Net.Http.Headers;
using WebApplication1.Models;


namespace RecursosHumanos.Controllers
{
    public class EvaluacionController : Controller
    {
        private readonly AplicationDbContext _db;

        /*se utiliza para declarar una variable de solo lectura que almacena una instancia 
         * de IWebHostEnvironment. Esta interfaz proporciona información sobre el entorno 
         * de hospedaje web en el que se está ejecutando la aplicación, como el nombre del 
         * entorno (desarrollo, producción, etc.), la ruta del contenido web y otros detalles 
         * específicos del entorno.*/
      
        public EvaluacionController(AplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index ()
        {
            IEnumerable<Evaluacion> lista = _db.evaluacion.Where(x => x.EstadoEvaluacion == true);

            return View(lista);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Evaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                _db.evaluacion.Add(evaluacion);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(evaluacion);
        }

        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.evaluacion.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Evaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                _db.evaluacion.Update(evaluacion);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(evaluacion);
        }

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.evaluacion    .Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Evaluacion evaluacion)
        {
            if (ModelState.IsValid)
            {
                evaluacion.EstadoEvaluacion = false;
                _db.evaluacion.Update(evaluacion);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            //Meter control de errores
            return View(evaluacion);
        }
    }
}
