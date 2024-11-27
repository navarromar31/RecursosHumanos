using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IPuestoRepositorio : IRepositorio<Puesto>
    {
        void Actualizar(Puesto puesto);
        IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj);
        IEnumerable<Puesto> ObtenerPuestosEliminados();

    }
}
