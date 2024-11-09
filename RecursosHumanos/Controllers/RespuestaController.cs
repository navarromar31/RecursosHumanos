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
            //validamos el modelo
            /*if (ModelState.IsValid)
            {
                //esto para poder obtener la imagen que nos envia la vista
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                //ahora validamos si es o no un nuevo ingreso o si se trata de una actualizacion 
                if (colaboradorVM.Colaborador.Id == 0)
                {
                    //toda esta logica es para lograr grabar un colaborador y una imagen
                    //crear si el ID es cero
                    string upload = webRootPath + WC.ImagenRuta;//LA PRIPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE GUARDAR LA IMAGEN
                    string fileName = Guid.NewGuid().ToString(); //esto es para que se le asigne un ID a la iamgen que se va a guardar
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    //finalmente grabanos la imagen y los datos del colaborador en los campos correspondientes

                    colaboradorVM.Colaborador.ImagenUrlCol = fileName + extension;
                    _colaboradorRepo.Agregar(colaboradorVM.Colaborador);
                }
                else
                {
                    //se actualiza si el ID es mayor a cero
                    var objColaborador = _colaboradorRepo.ObtenerPrimero(p => p.Id == colaboradorVM.Colaborador.Id, isTracking: false);   //Obtenemos el producto que queremos editar 

                    if (files.Count > 0)//validamos con este si existe una imagen y si es asi el usuario lo que va hacer es cambiar la imagen actual por otra 
                    {
                        string upload = webRootPath + WC.ImagenRuta;//LA PRIPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE GUARDAR LA IMAGEN
                        string fileName = Guid.NewGuid().ToString(); //esto es para que se le asigne un ID a la iamgen que se va a guardar
                        string extension = Path.GetExtension(files[0].FileName);
                        //pero tendriamos que borrar la img anterior

                        var anteriorFile = Path.Combine(upload, objColaborador.ImagenUrlCol); //con esto obtnemos la anterior
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

                        colaboradorVM.Colaborador.ImagenUrlCol = fileName + extension;

                    }//CASO CONTRARIO SI NO SE CARGA UNA NUEVA IMG , ES DECIR ACTUALIZA OTROS DATOS PERO LA IMG NO
                    else
                    {
                        //en ese caso se mantiene la misma img

                        colaboradorVM.Colaborador.ImagenUrlCol = objColaborador.ImagenUrlCol;

                    }
                    //ya luego actualizamos el producto

                    _colaboradorRepo.Actualizar(colaboradorVM.Colaborador);

                }

                _colaboradorRepo.Grabar();
                return RedirectToAction("Index");
            }//ESTA LLAVE PERTENCE AL IF DEL ModelIsValidate
            */

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

            //ahora lo primero es proceder a eliminar la imagen de nuestro server 


            //string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;//LA PRoPIEDAD DE WC ES LA QUE TIENE LA RUTA DE DONDE esta  GUARDAda LA IMAGEN


            /*var anteriorFile = Path.Combine(upload, colaborador.ImagenUrlCol);
            if (System.IO.File.Exists(anteriorFile))//VERIFICAMOS SI LA IMG ANTERIOR EXISTE
            {
                System.IO.File.Delete(anteriorFile);    // SI EXISTE LA BORRAMOS
            }*/

            _respuestaRepo.Remover(respuesta);  //Ahora eliminamos el producto
            _respuestaRepo.Grabar();
            return RedirectToAction(nameof(Index));


        }



    }
}
