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
using Microsoft.AspNetCore.Authorization;
using RecursosHumanos_Utilidades;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models.ViewModels.RecursosHumanos_Models.ViewModels;
using System.Collections;
namespace RecursosHumanos.Controllers
{

    [Authorize(Roles = WC.AdminRole)]
    public class EvaluacionController : Controller
    {

        private readonly ILogger<EvaluacionController> _logger;
        private readonly IEvaluacionRepositorio _evaluacionRepo;
        private readonly IPreguntaRepositorio _preguntaRepo;

        public static Evaluacion evaluacion;


        public EvaluacionController(ILogger<EvaluacionController> logger, IEvaluacionRepositorio evaluacionRepo, IPreguntaRepositorio preguntaRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _evaluacionRepo = evaluacionRepo;
            _preguntaRepo = preguntaRepo;
            _logger = logger;

            if (evaluacion == null)
            {
                evaluacion = new Evaluacion();
            }


        }


        public IActionResult Index()
        {

            EvaluacionVM evaluacionVM = new EvaluacionVM()
            {
                EvaluacionListaDB = _evaluacionRepo.ObtenerTodos(incluirPropiedades: "Preguntas")

            };

            return View(evaluacionVM);
        }



        //Get
        public IActionResult Crear()
        {

            EvaluacionVM evaluacionVM = new EvaluacionVM()
            {

                Evaluacion = evaluacion

            };

            return View(evaluacionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(EvaluacionVM evaluacionVM)
        {
            evaluacionVM.Evaluacion.Preguntas = evaluacion.Preguntas;
            foreach (Pregunta pregunta in evaluacionVM.Evaluacion.Preguntas)
            {
                if (pregunta.Id != 0)
                {
                    pregunta.Id = 0;
                }

            }

            _evaluacionRepo.Agregar(evaluacionVM.Evaluacion);
            _evaluacionRepo.Grabar();
            TempData[WC.Exitosa] = "Evaluacion creada exitosamente";
            evaluacion = new Evaluacion();
            return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index


        }


        //GET EDITAR QUE RECIBE DE LA VISTA EL ID DE LA CAT A EDITAR

        public IActionResult Editar(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            Evaluacion evaluacion = _evaluacionRepo.ObtenerPrimero(d => d.Id == Id, incluirPropiedades: "Preguntas");



            return View(evaluacion);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Evaluacion evaluacion)
        {

            if (ModelState.IsValid)
            {

                _evaluacionRepo.Actualizar(evaluacion);
                _evaluacionRepo.Grabar();


                return RedirectToAction(nameof(Index));
            }


            return View(evaluacion);
        }


        //GET ELIMINAR
        public IActionResult Eliminar(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            Evaluacion evaluacion = _evaluacionRepo.ObtenerPrimero(d => d.Id == Id, incluirPropiedades: "Preguntas");



            return View(evaluacion);

        }

        //POST ELIMINAR


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Evaluacion evaluacion)
        {

            if (evaluacion == null)
            {
                return NotFound();
            }
            _evaluacionRepo.Remover(evaluacion);
            _evaluacionRepo.Grabar();
            return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index

        }
        //*******************************COMIENZAN LOS CONTROLADORES DE PREGUNTA*************//
        [HttpGet]
        public IActionResult UpsertPregunta()
        {
            PreguntaVM preguntaVM = new PreguntaVM()
            {
                CapacitacionLista = _preguntaRepo.ObtenerTodosDropDownList(WC.CapacitacionNombre)

            };

            return View(preguntaVM);

        }

        [HttpPost]
        public IActionResult UpsertPregunta(PreguntaVM preguntaVM)
        {
            // Guarda cada producto en una lista interna del controlador
            int id = evaluacion.Preguntas.Count() + 1;
            preguntaVM.Pregunta.Id = id;

            evaluacion.Preguntas.Add(preguntaVM.Pregunta);

            return RedirectToAction("Crear");

        }

        //GET
        [HttpGet]
        //[Route("/{id?}")]
        public IActionResult EliminarPregunta(int? id)
        {
            Pregunta preguntaEliminar = new Pregunta();
            if (id == null || id == 0)
            {

                return NotFound();
            }

            foreach (Pregunta pregunta in evaluacion.Preguntas)
            {s
                if (pregunta.Id == id)
                {

                    preguntaEliminar = pregunta;

                }

            }
            evaluacion.Preguntas.Remove(preguntaEliminar);

            return RedirectToAction("Crear");

            //return View(preguntavm); //le retornamos a la vista aliminar los datos del producto a eliminar 

        }



    }
}
