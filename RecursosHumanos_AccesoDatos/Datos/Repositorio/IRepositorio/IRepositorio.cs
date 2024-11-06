using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        // Método para obtener una entidad por su ID.
        T Obtener(int id);

        // Método para obtener todas las entidades que cumplan con un filtro específico.
        IEnumerable<T> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,  // Filtro opcional basado en una expresión lambda.
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  // Función opcional para ordenar los resultados.
            string incluirPropiedades = null,  // Propiedades relacionadas a incluir en el resultado.
            bool isTracking = true  // Indica si se realiza el seguimiento de cambios en las entidades.
        );

        // Método para obtener la primera entidad que cumpla con un filtro específico.
        T ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,  // Filtro opcional basado en una expresión lambda.
            string incluirPropiedades = null,  // Propiedades relacionadas a incluir en el resultado.
            bool isTracking = true  // Indica si se realiza el seguimiento de cambios en las entidades.
        );

        // Método para agregar una nueva entidad.
        void Agregar(T entidad);

        // Método para eliminar una entidad existente.
        void Remover(T entidad);

        // Método para guardar los cambios en la base de datos.
        void Grabar();
        //
        void RemoverRango(IEnumerable<T> entidad);
    }
}