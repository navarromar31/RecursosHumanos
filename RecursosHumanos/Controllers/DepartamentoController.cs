using Microsoft.AspNetCore.Mvc;
using RecursosHumanos.Datos;
using WebApplication1.Models;

namespace RecursosHumanos.Controllers

{
    public class DepartamentoController : Controller
    {

        private readonly AplicationDbContext _db;

        public DepartamentoController(AplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Departamento> lista = _db.departamento;

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
        public IActionResult Crear(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _db.departamento.Add(departamento);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(departamento);
        }

        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.departamento.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _db.departamento.Update(departamento);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(departamento);
        }

        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }

            var obj = _db.departamento.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }
            _db.departamento.Remove(departamento);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
