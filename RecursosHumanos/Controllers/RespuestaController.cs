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
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Utilidades;

namespace RecursosHumanos.Controllers
{
    public class RespuestaController : Controller
    {

        // vamos a invocar  a nuestro dbcontext 


        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRespuestaRepositorio _respuestaRepo;
        public RespuestaController(IRespuestaRepositorio respuestaRepo, IWebHostEnvironment webHostEnvironment)//recibe nuestro contexto de BD
        {
            // _db = db;
            _respuestaRepo = respuestaRepo;
            _webHostEnvironment = webHostEnvironment;

        }



        public IActionResult Index()
        {
            /* IEnumerable<Producto> lista = _db.Producto.Include(c => c.Categoria)
                                                       .Include(t => t.TipoAplicacion);*/


            IEnumerable<Respuesta> lista = _respuestaRepo.ObtenerTodos(incluirPropiedades: "Respuesta,Colaborador,Pregunta");

            return View(lista);
        }



        //GET

        public IActionResult Upsert(int? Id)
        {

            RespuestaVM respuestaVM = new RespuestaVM()
            {
                respuesta = new Respuesta(),

                ColaboradorLista = _respuestaRepo.ObtenerTodosDropDownList(WC.ColaboradorNombre),
                PreguntasLista = _respuestaRepo.ObtenerTodosDropDownList(WC.PreguntaNo),


            };


            if (Id == null)
            {
                //crearemos un nuevo producto cuando no recibamos un ID

                return View(respuestaVM);

            }
            else
            {
                respuestaVM.respuesta = _respuestaRepo.Obtener(Id.GetValueOrDefault());
                if (respuestaVM.respuesta == null)
                {
                    return NotFound();
                }
                return View(respuestaVM);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Upsert(RespuestaVM respuestaVM)
        {

            respuestaVM.ColaboradorLista = _respuestaRepo.ObtenerTodosDropDownList(WC.ColaboradorNombre);
            respuestaVM.PreguntasLista = _respuestaRepo.ObtenerTodosDropDownList(WC.PreguntaNo);

            return View(respuestaVM);//si el modelo no es validado o sea no es correcto retornamos a la vista el objeto

        }

        // ACA NO SOLAMENTE ELIMINAMOS EL PRODUCTO , SINO TBM LA IMG ASOCIADA A ESTE
        //GET


        public IActionResult Eliminar(int? id)
        {

            if (id == null || id == 0)
            {

                return NotFound();
            }

            Respuesta respuesta = _respuestaRepo.ObtenerPrimero(p => p.Id == id);    //aca traemos los datos del producto de
                                                                                  //acuerdo con el ID que recibimos de la vista


            if (respuesta == null)
            {
                //en caso de que no exista 
                return NotFound();
            }

            return View(respuesta); //le retornamos a la vista aliminar los datos del producto a eliminar 

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Respuesta respuesta)
        {
            if (respuesta == null)
            {
                return NotFound();

            }

            _respuestaRepo.Remover(respuesta);  //Ahora eliminamos el producto
            _respuestaRepo.Grabar();
            return RedirectToAction(nameof(Index));


        }



    }
}
