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


namespace RecursosHumanos.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class EvaluacionController : Controller
    {

        //private readonly ApplicationDbContext _db;


        //public  EvaluacionController(ApplicationDbContext db)
        //{
        //    _db = db;  
        //}

       

        private readonly IEvaluacionRepositorio _evaluacionRepo;

        public EvaluacionController(IEvaluacionRepositorio evaluacionRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _evaluacionRepo = evaluacionRepo;

        }


        public IActionResult Index()
        {
            IEnumerable<Evaluacion> lista = _evaluacionRepo.ObtenerTodos();

            return View(lista);
        }

        //Get
        public IActionResult Crear()
        {


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Evaluacion evaluacion)
        {

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
