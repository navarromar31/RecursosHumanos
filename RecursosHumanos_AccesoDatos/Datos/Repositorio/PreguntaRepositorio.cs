using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_AccesoDatos.Datos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio
{
    public class PreguntaRepositorio : Repositorio<Pregunta>, IPreguntaRepositorio
    {

        private readonly AplicationDbContext _db;
        public PreguntaRepositorio(AplicationDbContext db) : base(db) //aca necesitamos implementar el db padre 
        {
            _db = db;
        }
        public void Actualizar(Pregunta pregunta)
        {
            _db.Update(pregunta);
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            //  throw new NotImplementedException();

            if (obj == WC.CapacitacionNombre)
            {
                return _db.capacitacion.Select(c => new SelectListItem
                {
                    Text = c.NombreCapacitacion,
                    Value = c.Id.ToString()

                });

            }

            if (obj == WC.EvaluacionNombre)
            {
                return _db.evaluacion.Select(t => new SelectListItem
                {
                    Text = t.Titulo,
                    Value = t.Id.ToString()

                });

            }
            return null;

        }
    }
}
