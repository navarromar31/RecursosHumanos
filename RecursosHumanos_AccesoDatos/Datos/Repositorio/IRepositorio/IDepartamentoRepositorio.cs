using RecursosHumanos_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface IDepartamentoRepositorio : IRepositorio<Departamento>
    {
        void Actualizar(Departamento departamento);


    }
}
