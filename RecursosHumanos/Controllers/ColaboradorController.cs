﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos.Datos;
using RecursosHumanos.Models;
using RecursosHumanos.Models.ViewModels;
using System.Collections.Generic;

namespace RecursosHumanos.Controllers
{
    public class ColaboradorController : Controller
    {

        private readonly AplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ColaboradorController(AplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Colaborador> lista = _db.colaborador.Where(x => x.EstadoColaborador == true)
                .Include(p => p.Puesto)
                .Include(d => d.Departamento)
                .Include(i => i.Institucion);

            return View(lista);
        }

        public IActionResult Insertar(int? Id)
        {
            //******************************* COMIENZA EL VIEWBAG *******************************

            /*IEnumerable<SelectListItem> puestoDropDown = _db.puesto.Select(
                p => new SelectListItem
                {
                    Text = p.NombrePuesto,
                    Value = p.Id.ToString()
                });
            ViewBag.puestoDropDown = puestoDropDown;

            IEnumerable<SelectListItem> departamentoDropDown = _db.departamento.Select(
                d => new SelectListItem
                {
                    Text = d.NombreDepartamento,
                    Value = d.Id.ToString()
                });
            ViewBag.puestoDropDown = departamentoDropDown;

            IEnumerable<SelectListItem> institucionDropDown = _db.institucion.Select(
                i => new SelectListItem
                {
                    Text = i.NombreInstitucion,
                    Value = i.Id.ToString()
                });
            ViewBag.puestoDropDown = institucionDropDown;

            Colaborador colaborador = new Colaborador();*/

            //******************************** FINALIZA EL VIEWBAG *********************************
            
            ColaboradorVM colaboradorVM = new ColaboradorVM
            {
                
                Puesto = _db.puesto.Select(p => new SelectListItem
                {
                    Text = p.NombrePuesto,
                    Value = p.Id.ToString()
                }),
                Departamento = _db.departamentos.Select(d => new SelectListItem
                {
                    Text = d.NombreDepartamento,
                    Value = d.Id.ToString()
                }),
                Institucion = _db.instituciones.Select(i => new SelectListItem
                {
                    Text = i.NombreInstitucion,
                    Value = i.Id.ToString()
                })
            };



            //BUSCAR
            if (Id == null)
            {
                return View(colaboradorVM);
            }
            else
            {
                colaboradorVM.Colaborador = _db.colaborador.Find(Id);
                if (colaboradorVM == null)
                {
                    return NotFound();
                }
                return View(colaboradorVM);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ColaboradorVM colaboradorVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;   //Archivos que son enviados desde la vista
                string webRootPath = _webHostEnvironment.WebRootPath;//Ruta de donde se almcena la imagen

                if (colaboradorVM.Colaborador.Id == 0)
                {
                    string upload = webRootPath + WC.ImagenRuta; //Repositorio fisico de las imagenes
                    string fileName = Guid.NewGuid().ToString();//Asingnar a la imagen un nombre automaticamente
                    string extension = Path.GetExtension(files[0].FileName);//Extrae la extension para que se gaurde con la imagen de la respectiva extension

                    //PROCESO PARA GUARDAR LA IMAGEN
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    colaboradorVM.Colaborador.ImagenUrlCol = fileName + extension;
                    _db.colaborador.Add(colaboradorVM.Colaborador); //Si el modelo no existe, lo crea
                }
                else
                {

                    //*******************************************************ACTUALIZAR PRODUCTO***********************************************//


                    var objColaborador = _db.colaborador.AsNoTracking().FirstOrDefault(p => p.Id == colaboradorVM.Colaborador.Id);//Si la imagen asociada al producto ya existe, la actualizamos

                    //Comprobacion de actualizacion o gaurdar nuevo
                    //En caso de que el archivo sea mayor a 0, se actualiza la imagen
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta; //Repositorio fisico de las imagenes
                        string fileName = Guid.NewGuid().ToString();//Asingnar a la imagen un nombre automaticamente
                        string extension = Path.GetExtension(files[0].FileName);//Extrae la extension para que se gaurde con la imagen de la respectiva extension

                        var anteriorFile = Path.Combine(upload, objColaborador.ImagenUrlCol + extension);

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
                        colaboradorVM.Colaborador.ImagenUrlCol = fileName + extension;
                    }
                    else //Mantiene lamisma imagen
                    {
                        colaboradorVM.Colaborador.ImagenUrlCol = objColaborador.ImagenUrlCol;
                    }
                    _db.colaborador.Update(colaboradorVM.Colaborador);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");

            }//If ModelIsValided cierre

            return View(colaboradorVM); //Si algo no es valido, retorna a la vista, el modleo tal y como estaba

        }

        //***************************************************** ELIMINAR PRODUCTO **************************************************//
        //METODO GET
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();

            }
            Colaborador colaborador = _db.colaborador.Include(p => p.Puesto)
                .Include(d => d.Departamento).FirstOrDefault(p => p.Id == Id);
            if (colaborador == null)
            {
                return NotFound();

            }
            return View(colaborador);
        }

        // CIERRE ELIMINAR
        //METODO POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Colaborador colaborador)
        {
            if (colaborador == null)
            {
                return NotFound();

            }
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;
            var anteriorFile = Path.Combine(upload, colaborador.ImagenUrlCol);
            if (System.IO.File.Exists(anteriorFile))
            {

                System.IO.File.Delete(anteriorFile);
            }

            colaborador.EstadoColaborador = false; 

            _db.colaborador.Remove(colaborador);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}