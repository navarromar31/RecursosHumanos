using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_Models;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using System.Linq;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Utilidades;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace RecursosHumanos.Controllers
{
    public class ColaboradorController : Controller
    {

        // vamos a invocar  a nuestro dbcontext 

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IColaboradorRepositorio  _colaboradorRepo;
        public ColaboradorController(IColaboradorRepositorio colaboradorRepo, IWebHostEnvironment webHostEnvironment)//recibe nuestro contexto de BD
        {
            // _db = db;
            _colaboradorRepo = colaboradorRepo;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            var lista = _colaboradorRepo.ObtenerTodos();
            return View(lista);
        }

        public IActionResult Upsert(int? id)
        {
            // Inicializar ViewModel con las listas necesarias
            ColaboradorVM colaboradorVM = new ColaboradorVM
            {
                Colaborador = new Colaborador(),
                InstitucionLista = _colaboradorRepo.ObtenerTodos()
                    .Select(i => new SelectListItem
                    {
                        Text = i.NombreColaborador,
                        Value = i.Id.ToString()
                    }),
                DepartamentoLista = _colaboradorRepo.ObtenerTodos()
                    .Select(d => new SelectListItem
                    {
                        Text = d.NombreColaborador,
                        Value = d.Id.ToString()
                    }),
                PuestoLista = _colaboradorRepo.ObtenerTodos()
                    .Select(p => new SelectListItem
                    {
                        Text = p.NombreColaborador,
                        Value = p.Id.ToString()
                    })
            };

            // Si es edición, cargar la capacitación desde la base de datos
            if (id != null && id != 0)
            {
                colaboradorVM.Colaborador = _colaboradorRepo.Obtener(id.GetValueOrDefault());
                if (colaboradorVM.Colaborador == null)
                {
                    return NotFound();
                }
            }

            return View(colaboradorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ColaboradorVM colaboradorVM, IFormFile imagenColaborador)
        {
            if (ModelState.IsValid)
            {
                // Manejo de imagen (igual que antes)
                if (imagenColaborador != null)
                {
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "imagenes");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenColaborador.FileName);
                    string filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imagenColaborador.CopyTo(stream);
                    }

                    colaboradorVM.Colaborador.ImagenUrlCol = "/imagenes/" + fileName;
                }
                else if (colaboradorVM.Colaborador.Id != 0) // Conservar la imagen actual al editar
                {
                    var colaboradorEnDb = _colaboradorRepo.Obtener(colaboradorVM.Id);
                    colaboradorVM.ImagenUrlCol = colaboradorEnDb.ImagenUrlCol;
                }

                // Guardar en la base de datos
                if (colaboradorVM.Colaborador.Id == 0)
                {
                    _colaboradorRepo.Agregar(colaboradorVM.Colaborador);
                }
                else
                {
                    _colaboradorRepo.Actualizar(colaboradorVM.Colaborador);
                }

                _colaboradorRepo.Grabar();
                TempData["Exitosa"] = colaboradorVM.Colaborador.Id == 0 ? "Colaborador creada exitosamente" : "Colaborador actualizada exitosamente";
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Error al guardar la colaborador";

            // Volver a cargar las listas para evitar errores al recargar la vista
            colaboradorVM.InstitucionLista = _colaboradorRepo.ObtenerTodos()
                .Select(i => new SelectListItem
                {
                    Text = i.NombreColaborador,
                    Value = i.Id.ToString()
                });
            colaboradorVM.DepartamentoLista = _colaboradorRepo.ObtenerTodos()
                .Select(d => new SelectListItem
                {
                    Text = d.NombreColaborador,
                    Value = d.Id.ToString()
                });
            colaboradorVM.PuestoLista = _colaboradorRepo.ObtenerTodos()
                .Select(p => new SelectListItem
                {
                    Text = p.NombreColaborador,
                    Value = p.Id.ToString()
                });

            return View(colaboradorVM);
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



    }



}
