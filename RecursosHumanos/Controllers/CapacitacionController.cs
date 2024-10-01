using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RecursosHumanos.Datos;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels;
using System.Collections.Generic;
using RecursosHumanos.Models;

namespace RecursosHumanos.Controllers
{
    public class CapacitacionController : Controller
    {

        private readonly AplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CapacitacionController(AplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Capacitacion> lista = _db.capacitacion
                .Include(c => c.Colaborador);

            return View(lista);
        }

        public IActionResult Upsert(int? Id)
        {
            CapacitacionVM capacitacionVM = new CapacitacionVM()
            {
                Capacitacion = new Capacitacion(),
                ColaboradorLista = _db.colaborador.Select(c => new SelectListItem
                {
                    Text = c.NombreColaborador,
                    Value = c.Id.ToString()
                }),

            };

            if (Id == null)
            {
                return View(capacitacionVM);
            }
            else
            {
                capacitacionVM.Capacitacion = _db.capacitacion.Find(Id);
                if (capacitacionVM == null)
                {
                    return NotFound();
                }
                return View(capacitacionVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CapacitacionVM capacitacionVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;   //Archivos que son enviados desde la vista
                string webRootPath = _webHostEnvironment.WebRootPath;//Ruta de donde se almcena la imagen

                if (capacitacionVM.Capacitacion.Id == 0)
                {
                    string upload = webRootPath + WC.ImagenRuta; //Repositorio fisico de las imagenes
                    string fileName = Guid.NewGuid().ToString();//Asingnar a la imagen un nombre automaticamente
                    string extension = Path.GetExtension(files[0].FileName);//Extrae la extension para que se gaurde con la imagen de la respectiva extension

                    //PROCESO PARA GUARDAR LA IMAGEN
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    capacitacionVM.Capacitacion.ImagenUrlCap = fileName + extension;
                    _db.capacitacion.Add(capacitacionVM.Capacitacion); //Si el modelo no existe, lo crea
                }
                else
                {

                    //ACTUALIZAR CAPACITACION
                    var objCapacitacion = _db.capacitacion.AsNoTracking().FirstOrDefault(p => p.Id == capacitacionVM.Capacitacion.Id);//Si la imagen asociada al producto ya existe, la actualizamos

                    //Comprobacion de actualizacion o gaurdar nuevo
                    //En caso de que el archivo sea mayor a 0, se actualiza la imagen
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta; //Repositorio fisico de las imagenes
                        string fileName = Guid.NewGuid().ToString();//Asingnar a la imagen un nombre automaticamente
                        string extension = Path.GetExtension(files[0].FileName);//Extrae la extension para que se gaurde con la imagen de la respectiva extension

                        var anteriorFile = Path.Combine(upload, objCapacitacion.ImagenUrlCap + extension);

                        //Verificar si el producto tiene una iamgen asociada
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream); //Especifica que si el archivo ya existe, se sobreescribe
                        }

                        //actualizamos nuestro producto con la nueva imagen
                        capacitacionVM.Capacitacion.ImagenUrlCap = fileName + extension;
                    }
                    else //Mantiene lamisma imagen
                    {
                        capacitacionVM.Capacitacion.ImagenUrlCap = objCapacitacion.ImagenUrlCap;
                    }

                    _db.capacitacion.Update(capacitacionVM.Capacitacion);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");

            }//If ModelIsValided cierre

            return View(capacitacionVM); //Si algo no es valido, retorna a la vista, el modleo tal y como estaba

        }
    }
}
