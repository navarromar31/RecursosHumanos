using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio
{
    public interface ILoginRepositorio 
    {
        bool ValidarColaborador(string correo, string cedula);
    }
}
