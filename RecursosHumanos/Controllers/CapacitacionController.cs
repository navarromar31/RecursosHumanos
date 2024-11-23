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

        //private readonly ApplicationDbContext _db;


        //public InstitucionController(ApplicationDbContext db)
        //{
        //    _db = db;  
        //}


        private readonly ICapacitacionRepositorio _capacitacionRepo;

        public CapacitacionController(ICapacitacionRepositorio capacitacionRepo)//recibe nuestro contexto de BD
        {
            //    _db = db;
            _capacitacionRepo = capacitacionRepo;

        }


        public IActionResult Index()
        {
            IEnumerable<Capacitacion> lista = _capacitacionRepo.ObtenerTodos();

            return View(lista);
        }

        //Get
        public IActionResult Crear()
        {


            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Institucion institucion)
        {

            if (ModelState.IsValid)
            {
                _institucionRepo.Agregar(institucion);
                _institucionRepo.Grabar();
                TempData[WC.Exitosa] = "Institucion creada exitosamente";
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            TempData[WC.Error] = "Error al crear nueva institucion";
            return View(institucion);
        }


        //GET EDITAR QUE RECIBE DE LA VISTA EL ID DE LA int A EDITAR
        public IActionResult Editar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var obj = _institucionRepo.Obtener(Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Institucion institucion)
        {

            if (ModelState.IsValid)
            {
                _institucionRepo.Actualizar(institucion);
                _institucionRepo.Grabar();
                return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index
            }
            return View(institucion);
        }



        //GET ELIMINAR
        public IActionResult Eliminar(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            var obj = _institucionRepo.Obtener(Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST ELIMINAR


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Institucion institucion)
        {

            if (institucion == null)
            {
                return NotFound();
            }
            _institucionRepo.Remover(institucion);
            _institucionRepo.Grabar();
            return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index

        }



    }
}

