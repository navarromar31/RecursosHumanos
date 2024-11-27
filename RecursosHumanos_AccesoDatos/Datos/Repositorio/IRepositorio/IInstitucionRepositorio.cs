using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IInstitucionRepositorio : IRepositorio<Institucion>
    {
        void Actualizar(Institucion institucion);
        IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj);
        IEnumerable<Institucion> ObtenerInstitucionesEliminadas();

    }
}
