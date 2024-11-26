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
    public class ColaboradorRepositorio : Repositorio<Colaborador>, IColaboradorRepositorio
    {

        private readonly AplicationDbContext _db;
        public ColaboradorRepositorio(AplicationDbContext db) : base(db) //aca necesitamos implementar el db padre 
        {
            _db = db;
        }
        public void Actualizar(Colaborador colaborador)
        {
            _db.Update(colaborador);
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            //  throw new NotImplementedException();

            if (obj == WC.DeartamentoNombre)
            {
                return _db.departamentos.Select(c => new SelectListItem
                {
                    Text = c.NombreDepartamento,
                    Value = c.Id.ToString()

                });

            }

            if (obj == WC.InstitucionNombre)
            {
                return _db.instituciones.Select(t => new SelectListItem
                {
                    Text = t.NombreInstitucion,
                    Value = t.Id.ToString()

                });

            }
            if (obj == WC.PuestoNombre)
            {
                return _db.puestos.Select(t => new SelectListItem
                {
                    Text = t.NombrePuesto,
                    Value = t.Id.ToString()

                });

            }
            return null;

        }
    }
}
