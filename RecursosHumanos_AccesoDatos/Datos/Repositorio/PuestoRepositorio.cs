using Microsoft.EntityFrameworkCore;
using RecursosHumanos_AccesoDatos.Datos.Repositorio;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio; // Importamos la interfaz del repositorio.
using RecursosHumanos_ViewModels.ViewModels.RecursosHumanos_Models; // Importamos los modelos.
using RecursosHumanos_ViewModels;
using System; // Importamos System para las clases básicas del .NET Framework.
using System.Collections.Generic; // Importamos colecciones genéricas.
using System.Linq; // Importamos LINQ para consultas.
using System.Text; // Importamos el espacio de nombres para manejo de texto.
using System.Threading.Tasks; // Importamos para manejar tareas asíncronas.

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio
{
    public class PuestoRepositorio : Repositorio<Puesto>, IPuestoRepositorio
    {
        private readonly AplicationDbContext _db;

        public PuestoRepositorio(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Puesto puesto)
        {
            _db.Update(puesto);
        }

        public IEnumerable<Puesto> ObtenerPuestosEliminado()
        {
            return dbSet.Where(i => i.Eliminado).ToList();
        }

        IEnumerable<Puesto> IPuestoRepositorio.ObtenerPuestoEliminado()
        {
            throw new NotImplementedException();
        }
    }
}
