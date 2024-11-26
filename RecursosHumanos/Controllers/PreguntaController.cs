using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_ViewModels;
using RecursosHumanos_ViewModels.ViewModels;
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

        // vamos a invocar  a nuestro dbcontext 


        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPreguntaRepositorio _preguntaRepo;
        public PreguntaController(IPreguntaRepositorio preguntaRepo, IWebHostEnvironment webHostEnvironment)//recibe nuestro contexto de BD
        {
            // _db = db;
            _preguntaRepo = preguntaRepo;
            _webHostEnvironment = webHostEnvironment;

        }



        public IActionResult Index()
        {
            /* IEnumerable<Producto> lista = _db.Producto.Include(c => c.Categoria)
                                                       .Include(t => t.TipoAplicacion);*/


            IEnumerable<Pregunta> lista = _preguntaRepo.ObtenerTodos(incluirPropiedades: "Pregunta,Evaluacion,Capacitacion");

            return View(lista);
        }



        //GET

        public IActionResult Upsert(int? Id)
        {

            PreguntaVM preguntaVM = new PreguntaVM()
            {
                Pregunta = new Pregunta(),

                EvaluacionLista = _preguntaRepo.ObtenerTodosDropDownList(WC.EvaluacionNombre),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Upsert(PreguntaVM preguntaVM)
        {
  
            preguntaVM.EvaluacionLista = _preguntaRepo.ObtenerTodosDropDownList(WC.EvaluacionNombre);
            preguntaVM.CapacitacionLista = _preguntaRepo.ObtenerTodosDropDownList(WC.CapacitacionNombre);

            return View(preguntaVM);//si el modelo no es validado o sea no es correcto retornamos a la vista el objeto

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
