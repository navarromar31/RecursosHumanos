using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_ViewModels;
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
    public class RespuestaRepositorio : Repositorio<Respuesta>, IRespuestaRepositorio
    {

        private readonly AplicationDbContext _db;
        public RespuestaRepositorio(AplicationDbContext db) : base(db) //aca necesitamos implementar el db padre 
        {
            _db = db;
        }
        public void Actualizar(Respuesta respuesta)
        {
            _db.Update(respuesta);
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

            if (obj == WC.PreguntaNo)
            {
                return _db.pregunta.Select(t => new SelectListItem
                {
                    Text = t.Texto,
                    Value = t.Id.ToString()

                });

            }
            return null;

        }
    }
}
