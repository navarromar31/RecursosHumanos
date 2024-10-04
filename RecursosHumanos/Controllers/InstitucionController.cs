using Microsoft.AspNetCore.Mvc;

using RecursosHumanos.Models;
using RecursosHumanos.Datos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using NuGet.Packaging.Signing;
using System.Collections;


namespace RecursosHumanos.Controllers
{
    public class InstitucionController : Controller
    {
        //Instancia del db context
        private readonly AplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //Metodo constructor que recibe parametros de la clase DBContext
        public InstitucionController(AplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        //Metodos que se ejecutan desde la vista 
        public IActionResult Index()
        {
            IEnumerable<Institucion> lista = _db.instituciones; //Accede a las categorias por medio del _db y trae a todos los objetos del modelo categoria y los almacena en la lista


            return View(lista);
        }

        //Get del action crear
        public IActionResult Crear()
        {
            return View();
        }

        //Post Envia informacion
        [HttpPost]
        [ValidateAntiForgeryToken] //Datos encriptados
        public IActionResult Crear(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _db.instituciones.Add(institucion);//Guarde los datos en la BD
                _db.SaveChanges();//Se salven los datos
                return RedirectToAction(nameof(Index));//Una vez los datos fueron insertados, muestre el index con la categoria insertada
            }
            return View(institucion);
        }



        //Get del action EDITAR
        public IActionResult Editar(int? Id) //Recibe el id de la categoria a editar, puede ser que venga nuelo, por eso el ?
        {
            if (Id == null || Id == 0)
            { return View(); }

            var obj = _db.instituciones.Find(Id); //Busque la categoria con el id y traiga el objeto de ese id
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        //Post Envia informacion
        [HttpPost]
        [ValidateAntiForgeryToken] //Datos encriptados
        public IActionResult Editar(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _db.instituciones.Update(institucion);//Actualiza los datos en la BD
                _db.SaveChanges();//Se salven los datos
                return RedirectToAction(nameof(Index));//Una vez los datos fueron insertados, muestre el index con la categoria insertada
            }

            return View(institucion);
        }


        //Get del action Eliminar
        public IActionResult Eliminar(int? Id) //Recibe el id de la categoria a eliminar, puede ser que venga nuelo, por eso el ?
        {
            if (Id == null || Id == 0)
            { return View(); }

            var obj = _db.instituciones.Find(Id); //Busque la categoria con el id y traiga el objeto de ese id
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        //Post Envia informacion
        [HttpPost]
        [ValidateAntiForgeryToken] //Datos encriptados
        public IActionResult Eliminar(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }

            _db.instituciones.Remove(institucion);//Actualiza los datos en la BD
            _db.SaveChanges();//Se salven los datos
            return RedirectToAction(nameof(Index));//Una vez los datos fueron insertados, muestre el index con la categoria insertada

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertInstitucion(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files; // Archivos enviados desde la vista
                string webRootPath = _webHostEnvironment.WebRootPath; // Ruta física para las imágenes

                if (institucion.Id == 0)
                {
                    // NUEVA INSTITUCIÓN
                    string upload = webRootPath + WC.ImagenRuta; // Repositorio de imágenes
                    string fileName = Guid.NewGuid().ToString(); // Nombre único para la imagen
                    string extension = Path.GetExtension(files[0].FileName); // Obtener la extensión de la imagen

                    // Guardar la imagen
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    institucion.ImagenUrlInstitucion = fileName + extension; // Asignar el nombre y extensión a la propiedad
                    _db.instituciones.Add(institucion); // Añadir la nueva institución
                }
                else
                {
                    // ACTUALIZAR INSTITUCIÓN
                    var objInstitucion = _db.instituciones.AsNoTracking().FirstOrDefault(i => i.Id == institucion.Id);

                    // Si se envió una nueva imagen, actualizarla
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta; // Ruta de las imágenes
                        string fileName = Guid.NewGuid().ToString(); // Nombre único para la imagen
                        string extension = Path.GetExtension(files[0].FileName); // Obtener la extensión de la nueva imagen

                        var anteriorFile = Path.Combine(upload, objInstitucion.ImagenUrlInstitucion);

                        // Verificar si existe una imagen anterior y eliminarla
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        // Guardar la nueva imagen
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        institucion.ImagenUrlInstitucion = fileName + extension; // Asignar la nueva imagen
                    }
                    else
                    {
                        // Mantener la imagen anterior
                        institucion.ImagenUrlInstitucion = objInstitucion.ImagenUrlInstitucion;
                    }

                    _db.instituciones.Update(institucion); // Actualizar la institución
                }

                _db.SaveChanges(); // Guardar los cambios en la base de datos
                return RedirectToAction("Index"); // Redirigir a la vista principal
            }

            // Si el modelo no es válido, retornar a la vista con el mismo objeto
            return View(institucion);
        }
    }

    }


