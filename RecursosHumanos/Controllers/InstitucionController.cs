using Microsoft.AspNetCore.Mvc;
using RecursosHumanos.Datos;
using RecursosHumanos.Models;

namespace RecursosHumanos.Controllers

{
    public class IntitucionController : Controller
    {

        private readonly AplicationDbContext _db;

        public IntitucionController(AplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Institucion> lista = _db.institucion;

            return View(lista);
        }

        //Get

        public IActionResult Crear()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _db.institucion.Add(institucion);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(institucion);
        }

        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.institucion.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                _db.institucion.Update(institucion);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(institucion);
        }

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.institucion.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Institucion institucion)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }
            _db.institucion.Remove(institucion);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
