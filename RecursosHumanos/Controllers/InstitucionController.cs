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
    public class InstitucionController : Controller
    {

        private readonly AplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public InstitucionController(AplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Institucion> lista = _db.institucion
                .Include(c => c.Institucion);

            return View(lista);
        }

        public IActionResult Upsert(int? Id)
        {
            InstitucionVM institucionVM = new InstitucionVM()
            {
                Institucion = new Institucion(),
                InstitucionLista = _db.institucion.Select(c => new SelectListItem
                {
                    Text = c.NombreInstitucion,
                    Value = c.Id.ToString()
                }),

            };

            if (Id == null)
            {
                return View(institucionVM);
            }
            else
            {
                institucionVM.Institucion = _db.institucion.Find(Id);
                if (institucionVM == null)
                {
                    return NotFound();
                }
                return View(institucionVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(InstitucionVM institucionVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (institucionVM.Institucion.Id == 0)
                {
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //guardar imagen
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    institucionVM.Institucion.ImagenUrlCap = fileName + extension;
                    _db.institucion.Add(institucionVM.Institucion);
                }
                else
                {


                    var objInstitucion = _db.institucion.AsNoTracking().FirstOrDefault(p => p.Id == institucionVM.Institucion.Id);



                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var anteriorFile = Path.Combine(upload, objInstitucion.ImagenUrlCap + extension);


                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }


                        institucionVM.InstitucionImagenUrlCap = fileName + extension;
                    }
                    else
                    {
                        institucionVM.Institucion.ImagenUrlCap = objInstitucion.ImagenUrlCap;
                    }

                    _db.institucion.Update(institucionVM.Institucion);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(institucionVM);

        }

        //get
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            { return NotFound(); }

            Institucion institucion = _db.institucion.Include(c => c.NombreInstitucion)
                .FirstOrDefault(p => p.Id == Id);

            if (institucion == null)
            {
                return NotFound();
            }
            return View(institucion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Institucion institucion)
        {
            if (institucion == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;
            var anteriorFile = Path.Combine(upload, institucion.ImagenUrlCap);


            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            _db.institucion.RemoveRange(institucion);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}