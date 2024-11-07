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
            /* IEnumerable<Producto> lista = _db.Producto.Include(c => c.Categoria)
                                                       .Include(t => t.TipoAplicacion);*/


            IEnumerable<Capacitacion> lista = _capacitacionRepo.ObtenerTodos(incluirPropiedades: "Colaborador");

            return View(lista);
        }



        //GET

        public IActionResult Upsert(int? Id)
        {

            CapacitacionVM capacitacionVM = new CapacitacionVM()
            {
                Capacitacion = new Capacitacion(),

                ColaboradorLista = _capacitacionRepo.ObtenerTodosDropDownList(WC.ColaboradorNombre),
            };


            if (Id == null)
            {
                //crearemos un nuevo producto cuando no recibamos un ID

                return View(capacitacionVM);

            }
            else
            {
                capacitacionVM.Capacitacion  = _capacitacionRepo.Obtener(Id.GetValueOrDefault());
                if (capacitacionVM.Capacitacion == null)
                {
                    return NotFound();
                }
                return View(capacitacionVM);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Upsert(CapacitacionVM capacitacionVM)
        {
            //validamos el modelo
            if (ModelState.IsValid)
            {
                //esto para poder obtener la imagen que nos envia la vista
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                //ahora validamos si es o no un nuevo ingreso o si se trata de una actualizacion 
                if (capacitacionVM.Capacitacion.Id == 0)
                {
                    //toda esta logica es para lograr grabar un producto y una imagen
                    //crear si el ID es cero
                    string upload = webRootPath + WC.ImagenRuta;//LA PRIPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE GUARDAR LA IMAGEN
                    string fileName = Guid.NewGuid().ToString(); //esto es para que se le asigne un ID a la iamgen que se va a guardar
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    //finalmente grabanos la imagen y los datos de producto en los campos correspondientes

                    capacitacionVM.Capacitacion.ImagenUrlCap = fileName + extension;
                    _capacitacionRepo.Agregar(capacitacionVM.Capacitacion);
                }
                else
                {
                    //se actualiza si el ID es mayor a cero
                    var objCapacitacion = _capacitacionRepo.ObtenerPrimero(p => p.Id == capacitacionVM.Capacitacion.Id, isTracking: false);   //Obtenemos el producto que queremos editar 

                    if (files.Count > 0)//validamos con este si existe una imagen y si es asi el usuario lo que va hacer es cambiar la imagen actual por otra 
                    {
                        string upload = webRootPath + WC.ImagenRuta;//LA PRIPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE GUARDAR LA IMAGEN
                        string fileName = Guid.NewGuid().ToString(); //esto es para que se le asigne un ID a la iamgen que se va a guardar
                        string extension = Path.GetExtension(files[0].FileName);
                        //pero tendriamos que borrar la img anterior

                        var anteriorFile = Path.Combine(upload, objCapacitacion.ImagenUrlCap); //con esto obtnemos la anterior
                        if (System.IO.File.Exists(anteriorFile))//VERIFICAMOS SI LA IMG ANTERIOR EXISTE
                        {
                            System.IO.File.Delete(anteriorFile);    // SI EXISTE LA BORRAMOS
                        }  //FIN DE BORRAR LA IMG ANTERIOR                                                        //

                        //PROCEDEMOS A CARGAR LA NUEVA IMAGEN
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        //PROCEDEMOS ACTUALIZAR EL PRODUCTO CON SU NUEVA IMG

                        capacitacionVM.Capacitacion.ImagenUrlCap = fileName + extension;

                    }//CASO CONTRARIO SI NO SE CARGA UNA NUEVA IMG , ES DECIR ACTUALIZA OTROS DATOS PERO LA IMG NO
                    else
                    {
                        //en ese caso se mantiene la misma img

                        capacitacionVM.Capacitacion.ImagenUrlCap = objCapacitacion.ImagenUrlCap;

                    }
                    //ya luego actualizamos el producto

                    _capacitacionRepo.Actualizar(capacitacionVM.Capacitacion);

                }

                _capacitacionRepo.Grabar();
                return RedirectToAction("Index");
            }//ESTA LLAVE PERTENCE AL IF DEL ModelIsValidate


            capacitacionVM.ColaboradorLista = _capacitacionRepo.ObtenerTodosDropDownList(WC.ColaboradorNombre);

            return View(capacitacionVM);//si el modelo no es validado o sea no es correcto retornamos a la vista el objeto

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
