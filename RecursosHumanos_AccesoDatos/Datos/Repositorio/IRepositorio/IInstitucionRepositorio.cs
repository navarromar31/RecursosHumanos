using RecursosHumanos_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecursosHumanos_AccesoDatos;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IInstitucionRepositorio : IRepositorio<Institucion>
    {
        void Actualizar(Institucion institucion);

    }
}
