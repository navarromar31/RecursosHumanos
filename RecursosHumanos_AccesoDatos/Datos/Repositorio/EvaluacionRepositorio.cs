using RecursosHumanos_AccesoDatos.Datos.Repositorio;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio; // Importamos la interfaz del repositorio.
using RecursosHumanos_Models; // Importamos los modelos.
using RecursosHumanos_AccesoDatos;
using System; // Importamos System para las clases básicas del .NET Framework.
using System.Collections.Generic; // Importamos colecciones genéricas.
using System.Linq; // Importamos LINQ para consultas.
using System.Text; // Importamos el espacio de nombres para manejo de texto.
using System.Threading.Tasks; // Importamos para manejar tareas asíncronas.

namespace RecursosHumanos_AccesoDatos.Datos
{
    public class EvaluacionRepositorio : Repositorio<Evaluacion>, IEvaluacionRepositorio
    {
        private readonly AplicationDbContext _db;

        public EvaluacionRepositorio(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Evaluacion evaluacion)
        {
            var evAnterior = _db.evaluacion.FirstOrDefault(c => c.Id == evaluacion.Id); // Debería ser 'Categorias'.
            if (evaluacion != null)
            {
                evaluacion.Titulo = evaluacion.Titulo;
            }
        }
    }
}
