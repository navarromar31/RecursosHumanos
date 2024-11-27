using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_AccesoDatos.Datos.Repositorio;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio; // Importamos la interfaz del repositorio.
using RecursosHumanos_Models; // Importamos los modelos.
using RecursosHumanos_Utilidades;
using System; // Importamos System para las clases básicas del .NET Framework.
using System.Collections.Generic; // Importamos colecciones genéricas.
using System.Linq; // Importamos LINQ para consultas.
using System.Text; // Importamos el espacio de nombres para manejo de texto.
using System.Threading.Tasks; // Importamos para manejar tareas asíncronas.

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio
{
    public class DepartamentoRepositorio : Repositorio<Departamento>, IDepartamentoRepositorio
    {
        private readonly AplicationDbContext _db;

        public DepartamentoRepositorio(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Departamento departamento)
        {
            _db.Update(departamento);
        }
        public IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj)
        {
            //  throw new NotImplementedException();

            if (obj == WC.DepartamentoNombre)
            {
                return _db.departamentos.Select(c => new SelectListItem
                {
                    Text = c.NombreDepartamento,
                    Value = c.Id.ToString()

                });

            }

            return null;

        }
    }
}
