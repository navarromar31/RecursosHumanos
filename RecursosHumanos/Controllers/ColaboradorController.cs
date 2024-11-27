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
        private readonly IDepartamentoRepositorio _departamentoRepo;
        private readonly IInstitucionRepositorio _institucionRepo;
        private readonly IPuestoRepositorio _puestoRepo;
        public static Colaborador colaborador;
        public ColaboradorController(ILogger<EvaluacionController> logger, IColaboradorRepositorio colaboradorRepo, IWebHostEnvironment webHostEnvironment, IDepartamentoRepositorio departamentoRepo, IInstitucionRepositorio institucionRepo, IPuestoRepositorio puestoRepos )//recibe nuestro contexto de BD
        {
            // _db = db;
            _colaboradorRepo = colaboradorRepo;
            _departamentoRepo= departamentoRepo;
            _institucionRepo= institucionRepo;
            _puestoRepo= puestoRepos;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ColaboradorVM colaboradorVM = new ColaboradorVM()
            {
                ColaboradorLista = _colaboradorRepo.ObtenerTodos(incluirPropiedades: "Departamento,Institucion,Puesto"),
                DepartamentoLista = _departamentoRepo.ObtenerTodosDropDownList(WC.DepartamentoNombre),
                InstitucionLista = _institucionRepo.ObtenerTodosDropDownList(WC.InstitucionNombre),
                PuestoLista = _puestoRepo.ObtenerTodosDropDownList(WC.PuestoNombre)
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
                Colaborador=colaborador,
                ColaboradorLista = _colaboradorRepo.ObtenerTodos(incluirPropiedades: "Departamento,Institucion,Puesto"),
                DepartamentoLista = _departamentoRepo.ObtenerTodosDropDownList(WC.DepartamentoNombre),
                InstitucionLista = _institucionRepo.ObtenerTodosDropDownList(WC.InstitucionNombre),
                PuestoLista = _puestoRepo.ObtenerTodosDropDownList(WC.PuestoNombre)

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

            // Obtén el colaborador
            var colaborador = _colaboradorRepo.ObtenerPrimero(p => p.Id == Id);
            if (colaborador == null)
            {
                return NotFound();
            }

            // Crea el ViewModel
            var colaboradorVM = new ColaboradorVM
            {
                Colaborador = colaborador,
                DepartamentoLista = _departamentoRepo.ObtenerTodos().Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.NombreDepartamento
                }).ToList(),
                InstitucionLista = _institucionRepo.ObtenerTodos().Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.NombreInstitucion
                }).ToList(),
                PuestoLista = _puestoRepo.ObtenerTodos().Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.NombrePuesto
                }).ToList()
            };

            return View(colaboradorVM); // Devuelve el ViewModel a la vista
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(ColaboradorVM colaboradorVM)
        {
            if (ModelState.IsValid)
            {
                // Actualiza el colaborador
                _colaboradorRepo.Actualizar(colaboradorVM.Colaborador);
                _colaboradorRepo.Grabar();

                // Mensaje de confirmación
                TempData["Success"] = "El colaborador ha sido actualizado correctamente.";

                // Redirigir al índice tras guardar los cambios
            }

            // Si hay errores, recarga las listas
            colaboradorVM.DepartamentoLista = _departamentoRepo.ObtenerTodos().Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.NombreDepartamento
            }).ToList();
            colaboradorVM.InstitucionLista = _institucionRepo.ObtenerTodos().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.NombreInstitucion
            }).ToList();
            colaboradorVM.PuestoLista = _puestoRepo.ObtenerTodos().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.NombrePuesto
            }).ToList();

            return RedirectToAction(nameof(Index));
        }




    }

}