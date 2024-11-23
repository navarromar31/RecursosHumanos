using RecursosHumanos_AccesoDatos;
using RecursosHumanos_Models;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_AccesoDatos.Datos.Repositorio;

namespace RecursosHumanos.Controllers
{
    public class CapacitacionController : Controller
    {

        // vamos a invocar  a nuestro dbcontext 


        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICapacitacionRepositorio _capacitacionRepo;
        public CapacitacionController(ICapacitacionRepositorio capacitacionRepo, IWebHostEnvironment webHostEnvironment)//recibe nuestro contexto de BD
        {
            // _db = db;
            _capacitacionRepo = capacitacionRepo;
            _webHostEnvironment = webHostEnvironment;

        }



        public IActionResult Index()
        {
            IEnumerable<Capacitacion> lista = _capacitacionRepo.ObtenerTodos();

            return View(lista);
        }



        //GET

        public IActionResult Upsert(int? id)
        {
            // Si no se recibe un ID, se crea un nuevo objeto Capacitacion
            var capacitacion = id == null ? new Capacitacion() : _capacitacionRepo.Obtener(id.GetValueOrDefault());

            if (capacitacion == null && id.HasValue)
            {
                return NotFound();
            }

            return View(capacitacion); // Pasar el modelo Capacitacion directamente a la vista
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
  
        public IActionResult Upsert(Capacitacion capacitacion)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (capacitacion.Id == 0)
                {
                    // Crear un nuevo registro
                    if (files.Count > 0)
                    {
                        string upload = Path.Combine(webRootPath, WC.ImagenRuta);
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        capacitacion.ImagenUrlCap = fileName + extension;
                    }

                    _capacitacionRepo.Agregar(capacitacion);
                }
                else
                {
                    // Actualizar un registro existente
                    var objCapacitacion = _capacitacionRepo.ObtenerPrimero(p => p.Id == capacitacion.Id, isTracking: false);

                    if (files.Count > 0)
                    {
                        string upload = Path.Combine(webRootPath, WC.ImagenRuta);
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        // Eliminar imagen anterior si existe
                        var anteriorFile = Path.Combine(upload, objCapacitacion.ImagenUrlCap ?? "");
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        capacitacion.ImagenUrlCap = fileName + extension;
                    }
                    else
                    {
                        // Mantener imagen existente
                        capacitacion.ImagenUrlCap = objCapacitacion.ImagenUrlCap;
                    }

                    _capacitacionRepo.Actualizar(capacitacion);
                }

                _capacitacionRepo.Grabar();
                return RedirectToAction("Index");
            }

            return View(capacitacion); // Volver a la vista con el modelo Capacitacion
        }


        // ACA NO SOLAMENTE ELIMINAMOS EL PRODUCTO , SINO TBM LA IMG ASOCIADA A ESTE
        //GET


        public IActionResult Eliminar(int? id)
        {

            if (id == null || id == 0)
            {

                return NotFound();
            }

            Capacitacion capacitacion = _capacitacionRepo.ObtenerPrimero(p => p.Id == id);    //aca traemos los datos del producto de
                                                                                  //acuerdo con el ID que recibimos de la vista


            if (capacitacion == null)
            {
                //en caso de que no exista 
                return NotFound();
            }

            return View(capacitacion); //le retornamos a la vista aliminar los datos del producto a eliminar 

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Capacitacion capacitacion)
        {
            if (capacitacion == null)
            {
                return NotFound();

            }

            //ahora lo primero es proceder a eliminar la imagen de nuestro server 


            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;//LA PRoPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE esta  GUARDAda LA IMAGEN


            var anteriorFile = Path.Combine(upload, capacitacion.ImagenUrlCap);
            if (System.IO.File.Exists(anteriorFile))//VERIFICAMOS SI LA IMG ANTERIOR EXISTE
            {
                System.IO.File.Delete(anteriorFile);    // SI EXISTE LA BORRAMOS
            }

            _capacitacionRepo.Remover(capacitacion);  //Ahora eliminamos el producto
            _capacitacionRepo.Grabar();
            return RedirectToAction(nameof(Index));


        }



    }



}
