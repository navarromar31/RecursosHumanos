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
    public class ColaboradorController : Controller
    {

        // vamos a invocar  a nuestro dbcontext 
        private readonly ILogger<EvaluacionController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IColaboradorRepositorio  _colaboradorRepo;
        public static Colaborador colaborador;
        public ColaboradorController(ILogger<EvaluacionController> logger, IColaboradorRepositorio colaboradorRepo, IWebHostEnvironment webHostEnvironment)//recibe nuestro contexto de BD
        {
            // _db = db;
            _colaboradorRepo = colaboradorRepo;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ColaboradorVM colaboradorVM = new ColaboradorVM()
            {
                ColaboradorLista= _colaboradorRepo.ObtenerTodos(incluirPropiedades: "Departamentos,Instituciones,Puestos")

            };
            if (colaborador == null)
            {
                colaborador = new Colaborador();
            }


            return View(colaboradorVM);
        }


        //Get
        public IActionResult Crear()
        {
            ColaboradorVM colaboradorVM = new ColaboradorVM()
            {
                Colaborador=colaborador

            };

            return View(colaboradorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ColaboradorVM colaboradorVM)
        {

            
            _colaboradorRepo.Agregar(colaboradorVM.Colaborador);
            _colaboradorRepo.Grabar();
            TempData[WC.Exitosa] = "Colaborador creado exitosamente";
            colaborador= new Colaborador();
            return RedirectToAction(nameof(Index)); //esto es para que ne redirigir al index


        }

        // ACA NO SOLAMENTE ELIMINAMOS EL PRODUCTO , SINO TBM LA IMG ASOCIADA A ESTE
        //GET


        public IActionResult Eliminar(int? id)
        {

            if (id == null || id == 0)
            {

                return NotFound();
            }

            Colaborador colaborador = _colaboradorRepo.ObtenerPrimero(p => p.Id == id);    //aca traemos los datos del producto de
                                                                                  //acuerdo con el ID que recibimos de la vista


            if (colaborador == null)
            {
                //en caso de que no exista 
                return NotFound();
            }

            return View(colaborador); //le retornamos a la vista aliminar los datos del producto a eliminar 

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Colaborador colaborador)
        {
            if (colaborador == null)
            {
                return NotFound();

            }

            //ahora lo primero es proceder a eliminar la imagen de nuestro server 


            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;//LA PRoPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE esta  GUARDAda LA IMAGEN


            var anteriorFile = Path.Combine(upload, colaborador.ImagenUrlCol);
            if (System.IO.File.Exists(anteriorFile))//VERIFICAMOS SI LA IMG ANTERIOR EXISTE
            {
                System.IO.File.Delete(anteriorFile);    // SI EXISTE LA BORRAMOS
            }

            _colaboradorRepo.Remover(colaborador);  //Ahora eliminamos el producto
            _colaboradorRepo.Grabar();
            return RedirectToAction(nameof(Index));


        }

        //GET EDITAR QUE RECIBE DE LA VISTA EL ID DE LA CAT A EDITAR

        public IActionResult Editar(int? Id)
        {


            if (Id == null || Id == 0)
            {

                return NotFound();
            }

            Colaborador colaborador = _colaboradorRepo.ObtenerPrimero(p => p.Id == Id);    //aca traemos los datos del producto de
                                                                                           //acuerdo con el ID que recibimos de la vista


            if (colaborador == null)
            {
                //en caso de que no exista 
                return NotFound();
            }

            return View(colaborador); //le retornamos a la vista aliminar los datos del producto a eliminar 


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Colaborador colaborador)
        {

            if (ModelState.IsValid)
            {

                _colaboradorRepo.Actualizar(colaborador);
                _colaboradorRepo.Grabar();


                return RedirectToAction(nameof(Index));
            }


            return View(colaborador);
        }



    }



}
