using Microsoft.AspNetCore.Mvc;
using RecursosHumanos.Datos;
using WebApplication1.Models;

namespace RecursosHumanos.Controllers
{
    public class ColaboradorController : Controller
    {

        private readonly AplicationDbContext _db;

        public ColaboradorController(AplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Colaborador> lista = _db.colaborador;

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
        public IActionResult Crear(Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                _db.colaborador.Add(colaborador);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(colaborador);
        }

        //EDITAR

        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.colaborador.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                _db.colaborador.Update(colaborador);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(colaborador);
        }

        //ELIMINAR

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.colaborador.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Colaborador colaborador)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }
            _db.colaborador.Remove(colaborador);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
