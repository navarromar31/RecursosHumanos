using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_Models;
using RecursosHumanos_Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

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
        public IActionResult Index()
        {
            IEnumerable<Pregunta> lista = _db.pregunta
                .Include(evaluacion=> evaluacion.Evaluacion)
                .Include(capacitacion=> capacitacion.Capacitacion).Where(pregunta => pregunta.EstadoPregunta == true);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PreguntaVM preguntaVM)
        {
            if (ModelState.IsValid)
            {
              
                if (preguntaVM.Pregunta.Id == 0)// id es 0, no hay objeto , hay que crear uno nuevo 
                {
                   
                    _db.pregunta.Add(preguntaVM.Pregunta);
                }
                else
                {
                    // si hay un producto ya creado y hay que actualizar 
                    //ACTULIZAR PRODUCTO }
                    var objProducto = _db.pregunta.AsNoTracking().FirstOrDefault(p => p.Id == preguntaVM.Pregunta.Id);

                    
                    _db.pregunta.Update(preguntaVM.Pregunta);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }//cierre modelo no valido 
            return View(preguntaVM);

        }

        //GET ELIMINAR 
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            Pregunta pregunta = _db.pregunta
                .Include(evaluacion => evaluacion.Evaluacion)
                .Include(capacitacion => capacitacion.Capacitacion).FirstOrDefault(p=>p.Id==0);
            if (pregunta == null)
            {
                return NotFound();

            }
            return View(pregunta);
        }// CIERRE ELIMINAR
        //METODO POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                pregunta.EstadoPregunta = false;
                _db.pregunta.Update(pregunta);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pregunta);

        }

    }
}
