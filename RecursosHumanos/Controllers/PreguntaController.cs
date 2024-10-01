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
    public class PreguntaController : Controller
    {
        private readonly AplicationDbContext _db;

        /*se utiliza para declarar una variable de solo lectura que almacena una instancia 
         * de IWebHostEnvironment. Esta interfaz proporciona información sobre el entorno 
         * de hospedaje web en el que se está ejecutando la aplicación, como el nombre del 
         * entorno (desarrollo, producción, etc.), la ruta del contenido web y otros detalles 
         * específicos del entorno.*/

        public PreguntaController(AplicationDbContext db, IWebHostEnvironment webhHostEnvironment)
        {
            _db = db;
          
        }
        public IActionResult Upsert()
        {
            IEnumerable<Pregunta> lista = _db.pregunta
                .Include(evaluacion=> evaluacion.Evaluacion)
                .Include(capacitacion=> capacitacion.Capacitacion);

            return View(lista);
        }
        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> evaluacionDropDown = _db.evaluacion.Select(Evaluacion=> new SelectListItem
            {
                Text = Evaluacion.Titulo,
                Value = Evaluacion.Id.ToString()
            });
            ViewBag.evaluacionDropDown= evaluacionDropDown;
            IEnumerable<SelectListItem> capacitacionDropDown = _db.capacitacion.Select(capacitacion => new SelectListItem
            {
                Text = capacitacion.NombreCapacitacion,
                Value = capacitacion.Id.ToString()
            });
            ViewBag.capacitacionDropDown=capacitacionDropDown;

            Pregunta pregunta= new Pregunta();
            PreguntaVM preguntavm= new PreguntaVM();
            {
                preguntavm.Pregunta = new Pregunta();
                preguntavm.EvaluacionLista = _db.evaluacion.Select(Evaluacion => new SelectListItem
                {
                    Text = Evaluacion.Titulo,
                    Value = Evaluacion.Id.ToString(),
                });
                preguntavm.CapacitacionLista = _db.capacitacion.Select(capacitacion => new SelectListItem
                {
                    Text = capacitacion.NombreCapacitacion,
                    Value = capacitacion.Id.ToString(),
                });
                if (Id == null)
                {
                    return View(preguntavm);
                }
                else
                {
                    preguntavm.Pregunta = _db.pregunta.Find(Id);
                    if (preguntavm == null)
                    {
                        return NotFound();
                    }
                    return View(preguntavm);
                }
                
            }

        }//cierreupsert
         //post
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.pregunta.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                _db.pregunta.Update(pregunta);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(pregunta);
        }

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.pregunta.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }
            _db.pregunta.Remove(pregunta);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
