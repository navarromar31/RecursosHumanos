using RecursosHumanos_ViewModels.ViewModels.RecursosHumanos_Models;
using RecursosHumanos_ViewModels;
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

        IEnumerable<Puesto> ObtenerPuestoEliminado();

    }
}