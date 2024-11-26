using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_Models;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;
using System.Linq;
using RecursosHumanos_Models.ViewModels;
using RecursosHumanos_Utilidades;

namespace RecursosHumanos.Controllers
{
    public class PuestoController : Controller
    {
        private readonly IPuestoRepositorio _puestoRepo;

        public PuestoController(IPuestoRepositorio puestoRepo)
        {
            _puestoRepo = puestoRepo;
        }

        public IActionResult Index()
        {
            var lista = _puestoRepo.ObtenerTodos(); // Obtener todos los puestos
            return View(lista);
        }

        public IActionResult Upsert(int? id)
        {
            Puesto model = new Puesto();

            if (id.HasValue)
            {
                // Obtener puesto por ID si se especifica
                model = _puestoRepo.Obtener(id.GetValueOrDefault());
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Upsert(Puesto model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _puestoRepo.Agregar(model); // Agregar nuevo puesto
                }
                else
                {
                    _puestoRepo.Actualizar(model); // Actualizar puesto existente
                }

                _puestoRepo.Grabar(); // Guardar cambios en la base de datos
                return RedirectToAction(nameof(Index));
            }

            return View(model); // Si hay un error de validación, vuelve a mostrar el formulario
        }

        // Otras acciones del controlador (e.g. Eliminar, etc.)
    



// Acción GET para eliminar un puesto (moverlo a eliminados)
public IActionResult Eliminar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var obj = _puestoRepo.Obtener(id.GetValueOrDefault());
        if (obj == null)
        {
            return NotFound();
        }

        // Cambiar el estado de Eliminado a true (mover a eliminados)
        obj.Eliminada = true;
        _puestoRepo.Actualizar(obj);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto movido a eliminados";
        return RedirectToAction(nameof(Index));
    }

    // Acción GET para restaurar un puesto eliminado
    public IActionResult Restaurar(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var puesto = _puestoRepo.Obtener(id.GetValueOrDefault());
        if (puesto == null)
        {
            return NotFound();
        }

        // Restaurar el puesto cambiando su estado de eliminado a falso
        puesto.Eliminada = false;
        _puestoRepo.Actualizar(puesto);
        _puestoRepo.Grabar();

        TempData["Exitosa"] = "Puesto restaurado exitosamente";
        return RedirectToAction(nameof(Index));
}
}

}