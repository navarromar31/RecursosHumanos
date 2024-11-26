using RecursosHumanos_ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IColaboradorRepositorio : IRepositorio<Colaborador>
    {
        void Actualizar(Colaborador colaborador);

        IEnumerable<SelectListItem> ObtenerTodosDropDownList(string obj);

    }
}
