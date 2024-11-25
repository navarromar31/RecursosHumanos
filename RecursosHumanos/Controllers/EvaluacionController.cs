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

        private List<Pregunta> listaPreguntas = new List<Pregunta>();

        public EvaluacionController(ILogger<EvaluacionController> logger, IEvaluacionRepositorio evaluacionRepo, IPreguntaRepositorio preguntaRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _evaluacionRepo = evaluacionRepo;
            _preguntaRepo = preguntaRepo;
            _logger = logger;
        }


        public IActionResult Index()
        {

            EvaluacionVM evaluacionVM = new EvaluacionVM()
            {
               Evaluacion=_evaluacionRepo.ObtenerTodos()

            };

            return View(evaluacionVM);
        }

        //Get
        public IActionResult Crear()
        {
            PreguntaVM preguntaVM = new PreguntaVM()
            {
                Preguntas = _preguntaRepo.ObtenerTodos()

            };

            return View(preguntaVM);
        }

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
          
            listaPreguntas.Add(preguntaVM.Pregunta);

            return View(preguntaVM);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Evaluacion evaluacion)
        {
            evaluacion.Preguntas = listaPreguntas;

            if (ModelState.IsValid)
            {
                _evaluacionRepo.Agregar(evaluacion);
                _evaluacionRepo.Grabar();
                TempData[WC.Exitosa] = "Evaluacion creada exitosamente";
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            TempData[WC.Error] = "Error al crear nueva evaluacion";
            return View(evaluacion);
        }


        //GET EDITAR QUE RECIBE DE LA VISTA EL ID DE LA CAT A EDITAR
        public IActionResult Editar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var obj = _evaluacionRepo.Obtener(Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Evaluacion evaluacion)
        {

            if (ModelState.IsValid)
            {
                _evaluacionRepo.Actualizar(evaluacion);
                _evaluacionRepo.Grabar();
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            return View(evaluacion);
        }



        //GET ELIMINAR
        public IActionResult Eliminar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var obj = _evaluacionRepo.Obtener(Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
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

    }
}
