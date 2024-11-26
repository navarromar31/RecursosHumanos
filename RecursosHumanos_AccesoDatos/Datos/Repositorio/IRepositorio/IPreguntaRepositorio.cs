using RecursosHumanos_ViewModels;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IPreguntaRepositorio : IRepositorio<Pregunta>
    {
        void Actualizar(Pregunta pregunta);

        IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj);

    }
}
