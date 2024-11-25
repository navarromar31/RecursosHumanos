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
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Utilidades;

namespace RecursosHumanos.Controllers
{
    public class PreguntaController : Controller
    {

        private readonly ILogger<PreguntaController> _logger;
        private readonly IEvaluacionRepositorio _evaluacionRepo;
        private readonly IPreguntaRepositorio _preguntaRepo;



        public PreguntaController(ILogger<PreguntaController> logger, IEvaluacionRepositorio evaluacionRepo, IPreguntaRepositorio preguntaRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _evaluacionRepo = evaluacionRepo;
            _logger = logger;
            _preguntaRepo = preguntaRepo;
        }



        public IActionResult Index()
        {

            PreguntaVM preguntaVM = new PreguntaVM()
            {
                Pregunta = (Pregunta)_preguntaRepo.ObtenerTodos()

            };

            return View(preguntaVM);
        }



        //GET

        public IActionResult Upsert(int? Id)
        {

            PreguntaVM preguntaVM = new PreguntaVM()
            {
                Pregunta = new Pregunta(),

               // EvaluacionLista = _preguntaRepo.ObtenerTodosDropDownList(WC.EvaluacionNombre),
                CapacitacionLista = _preguntaRepo.ObtenerTodosDropDownList(WC.CapacitacionNombre),
                

            };


            if (Id == null)
            {
                //crearemos un nuevo producto cuando no recibamos un ID

                return View(preguntaVM);

            }
            else
            {
                preguntaVM.Pregunta = _preguntaRepo.Obtener(Id.GetValueOrDefault());
                if (preguntaVM.Pregunta == null)
                {
                    return NotFound();
                }
                return View(preguntaVM);

            }

        }

      

        //GET


        public IActionResult Eliminar(int? id)
        {

            if (id == null || id == 0)
            {

                return NotFound();
            }

            Pregunta pregunta = _preguntaRepo.ObtenerPrimero(p => p.Id == id);    //aca traemos los datos del producto de
                                                                                           //acuerdo con el ID que recibimos de la vista


            if (pregunta == null)
            {
                //en caso de que no exista 
                return NotFound();
            }

            return View(pregunta); //le retornamos a la vista aliminar los datos del producto a eliminar 

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Pregunta pregunta)
        {
            if (pregunta == null)
            {
                return NotFound();

            }

            _preguntaRepo.Remover(pregunta);  //Ahora eliminamos el producto
            _preguntaRepo.Grabar();
            return RedirectToAction(nameof(Index));


        }



    }
}
