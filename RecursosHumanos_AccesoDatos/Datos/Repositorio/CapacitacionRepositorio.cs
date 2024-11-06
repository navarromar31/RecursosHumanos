using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_Models;
using RecursosHumanos_Utilidades;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio
{
    public class CapacitacionRepositorio : Repositorio<Capacitacion>, ICapacitacionRepositorio
    {
        private readonly AplicationDbContext _db;
        public CapacitacionRepositorio(AplicationDbContext db) : base(db) //aca necesitamos implementar el db padre 
        {
            _db = db;
        }
        public void Actualizar(Capacitacion capacitacion)
        {
            _db.Update(capacitacion);
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            //  throw new NotImplementedException();

            if (obj == WC.ColaboradorNombre)
            {
                return _db.colaborador.Select(c => new SelectListItem
                {
                    Text = c.NombreColaborador,
                    Value = c.Id.ToString()

                });

            }
        }
    }
}