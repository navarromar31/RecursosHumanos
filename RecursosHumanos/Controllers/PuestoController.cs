using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_Models;

namespace RecursosHumanos.Controllers
{
    public class PuestoController : Controller
    {

        private readonly AplicationDbContext _db;

        public PuestoController(AplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Puesto> lista = _db.puesto;

            return View(lista);
        }

        //CREAR

        public IActionResult Crear()
        {
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                _db.puesto.Add(puesto);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(puesto);
        }

        //EDITAR

        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.puesto.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                _db.puesto.Update(puesto);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(puesto);
        }

        //ELIMINAR

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.puesto.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Puesto puesto)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }
            _db.puesto.Remove(puesto);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
