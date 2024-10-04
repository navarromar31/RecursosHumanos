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
    public class RespuestaController : Controller
    {
        private readonly AplicationDbContext _db;

        /*se utiliza para declarar una variable de solo lectura que almacena una instancia 
         * de IWebHostEnvironment. Esta interfaz proporciona información sobre el entorno 
         * de hospedaje web en el que se está ejecutando la aplicación, como el nombre del 
         * entorno (desarrollo, producción, etc.), la ruta del contenido web y otros detalles 
         * específicos del entorno.*/

        public RespuestaController(AplicationDbContext db, IWebHostEnvironment webhHostEnvironment)
        {
            _db = db;

        }
        public IActionResult Upsert()
        {
            IEnumerable<Respuesta> lista = _db.respuesta
                .Include(Colaborador =>Colaborador.ColaboradorEvaluado)
                .Include(Colaborador=> Colaborador.ColaboradorEvaluador)
                .Include(Pregunta => Pregunta.IdPregunta).Where( respuesta => respuesta.EstadoRespuesta == true);

            return View(lista);
        }
        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> ColaboradorEvaluadoDropDown = _db.colaborador.Select(Colaborador=> new SelectListItem
            {
                Text = Colaborador.NombreColaborador,
                Value = Colaborador.Id.ToString()
            });
            ViewBag.ColaboradorEvaluadoDropDown = ColaboradorEvaluadoDropDown;

            IEnumerable<SelectListItem> ColaboradorEvaluadorDropDown = _db.colaborador.Select(Colaborador => new SelectListItem
            {
                Text = Colaborador.NombreColaborador,
                Value = Colaborador.Id.ToString()
            });
            ViewBag.ColaboradorEvaluadoDropDown = ColaboradorEvaluadoDropDown;

            IEnumerable<SelectListItem> PreguntaDropDown = _db.pregunta.Select(Pregunta => new SelectListItem
            {
                Text = Pregunta.Texto,
                Value = Pregunta.Id.ToString()
            });
            ViewBag.PreguntaDropDown = PreguntaDropDown;

            Respuesta respuesta = new Respuesta();
            RespuestaVM respuestaVM = new RespuestaVM();
            {
                respuestaVM.respuesta = new Respuesta();
                respuestaVM.ColaboradorLista = _db.colaborador.Select(Evaluado=> new SelectListItem
                {
                    Text = Evaluado.NombreColaborador,
                    Value = Evaluado.Id.ToString(),
                });
                respuestaVM.ColaboradorLista = _db.colaborador.Select(Evaluador => new SelectListItem
                {
                    Text = Evaluador.NombreColaborador,
                    Value = Evaluador.Id.ToString(),
                });
                respuestaVM.PreguntasLista = _db.pregunta.Select(Pregunta=> new SelectListItem
                {
                    Text = Pregunta.Texto,
                    Value = Pregunta.Id.ToString(),
                });

                if (Id == null)
                {
                    return View(respuestaVM);
                }
                else
                {
                    respuestaVM.respuesta = _db.respuesta.Find(Id);
                    if (respuestaVM == null)
                    {
                        return NotFound();
                    }
                    return View(respuestaVM);
                }

            }


        }//cierreupsert
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.respuesta.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                _db.respuesta.Update(respuesta);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(respuesta);
        }

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _db.respuesta.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Respuesta respuesta)
        {
            if (ModelState.IsValid)
            {
                respuesta.EstadoRespuesta = false;
                _db.respuesta.Update(respuesta);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(respuesta);
        }

    }
}
