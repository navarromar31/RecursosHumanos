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
    public class DepartamentoController : Controller
    {

        private readonly AplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DepartamentoController(AplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Departamento> lista = _db.departamento
                .Include(c => c.Departamento);

            return View(lista);
        }

        public IActionResult Upsert(int? Id)
        {
            DepartamentoVM departamentoVM = new DepartamentoVM()
            {
                Departamento = new Departamento(),
                DepartamentoLista = _db.departamento.Select(c => new SelectListItem
                {
                    Text = c.NombreDepartamento,
                    Value = c.Id.ToString()
                }),

            };

            if (Id == null)
            {
                return View(departamentoVM);
            }
            else
            {
                departamentoVM.Departamento = _db.departamento.Find(Id);
                if (departamentoVM == null)
                {
                    return NotFound();
                }
                return View(departamentoVM);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DepartamentoVM departamentoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (departamentoVM.Departamento.Id == 0)
                {
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //guardar imagen
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    departamentoVM.Departamento.ImagenUrlCap = fileName + extension;
                    _db.departamento.Add(departamentoVM.Departamento);
                }
                else
                {


                    var objDepartamento = _db.departamento.AsNoTracking().FirstOrDefault(p => p.Id == departamentoVM.Departamento.Id);



                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var anteriorFile = Path.Combine(upload, objDepartamento.ImagenUrlCap + extension);


                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }


                        departamentoVM.DepartamentoImagenUrlCap = fileName + extension;
                    }
                    else
                    {
                        departamentoVM.Departamento.ImagenUrlCap = objDepartamento.ImagenUrlCap;
                    }

                    _db.departamento.Update(departamentoVM.Departamento);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(departamentoVM);

        }

        //get
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            { return NotFound(); }

            Departamento departamento = _db.departamento.Include(c => c.NombreDepartamento)
                .FirstOrDefault(p => p.Id == Id);

            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Departamento departamento)
        {
            if (departamento == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;
            var anteriorFile = Path.Combine(upload, departamento.ImagenUrlCap);


            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            _db.departamento.RemoveRange(departamento);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}