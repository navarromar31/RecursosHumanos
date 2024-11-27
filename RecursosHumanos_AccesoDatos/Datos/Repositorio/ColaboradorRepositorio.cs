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
using Microsoft.EntityFrameworkCore;


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

        public async Task<Colaborador> ObtenerPorCorreoYCedulaAsync(string correo, string cedula)
        {
            return await _db.colaborador
                .FirstOrDefaultAsync(c => c.CorreoColaborador == correo && c.CedulaColaborador == cedula);
        }


        // Método para obtener un colaborador de la base de datos
        // que coincida con el correo y la cédula proporcionados.
        public Colaborador ObtenerPorCorreoYCedula(string correo, string cedula)
        {
            // Se consulta en la base de datos la tabla "Colaboradores".
            // Usamos FirstOrDefault para obtener el primer colaborador que coincida con el correo y la cédula.
            // Si no se encuentra ningún colaborador, se devuelve null.
            return _db.colaborador
                      .FirstOrDefault(c => c.CorreoColaborador == correo && c.CedulaColaborador == cedula);
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
                return _db.puesto.Select(t => new SelectListItem
                {
                    Text = t.NombrePuesto,
                    Value = t.Id.ToString()

                });

            }
            return null;

        }
    }
}
