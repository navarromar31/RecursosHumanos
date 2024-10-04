using Microsoft.AspNetCore.Mvc;

using RecursosHumanos.Models;
using RecursosHumanos.Datos;


namespace RecursosHumanos.Controllers
{
    public class DepartamentoController : Controller
    {
        //Instancia del db context
        private readonly AplicationDbContext _db;

        //Metodo constructor que recibe parametros de la clase DBContext
        public DepartamentoController(AplicationDbContext db)
        {
            _db = db;

        }

        //Metodos que se ejecutan desde la vista 
        public IActionResult Index()
        {
            IEnumerable<Departamento> lista = _db.departamentos; //Accede a las categorias por medio del _db y trae a todos los objetos del modelo categoria y los almacena en la lista


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
        public IActionResult Crear(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _db.departamentos.Add(departamento);//Guarde los datos en la BD
                _db.SaveChanges();//Se salven los datos
                return RedirectToAction(nameof(Index));//Una vez los datos fueron insertados, muestre el index con la categoria insertada
            }
            return View(departamento);
        }



        //Get del action EDITAR
        public IActionResult Editar(int? Id) //Recibe el id de la categoria a editar, puede ser que venga nuelo, por eso el ?
        {
            if (Id == null || Id == 0)
            { return View(); }

            var obj = _db.departamentos.Find(Id); //Busque la categoria con el id y traiga el objeto de ese id
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        //Post Envia informacion
        [HttpPost]
        [ValidateAntiForgeryToken] //Datos encriptados
        public IActionResult Editar(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _db.departamentos.Update(departamento);//Actualiza los datos en la BD
                _db.SaveChanges();//Se salven los datos
                return RedirectToAction(nameof(Index));//Una vez los datos fueron insertados, muestre el index con la categoria insertada
            }

            return View(departamento);
        }


        //Get del action Eliminar
        public IActionResult Eliminar(int? Id) //Recibe el id de la categoria a eliminar, puede ser que venga nuelo, por eso el ?
        {
            if (Id == null || Id == 0)
            { return View(); }

            var obj = _db.departamentos.Find(Id); //Busque la categoria con el id y traiga el objeto de ese id
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        //Post Envia informacion
        [HttpPost]
        [ValidateAntiForgeryToken] //Datos encriptados
        public IActionResult Eliminar(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }

            _db.departamentos.Remove(departamento);//Actualiza los datos en la BD
            _db.SaveChanges();//Se salven los datos
            return RedirectToAction(nameof(Index));//Una vez los datos fueron insertados, muestre el index con la categoria insertada

        }

    }
}
