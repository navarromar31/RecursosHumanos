using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_AccesoDatos.Datos.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly IColaboradorRepositorio _colaboradorRepo;

        public LoginRepositorio(IColaboradorRepositorio colaboradorRepo)
        {
            _colaboradorRepo = colaboradorRepo;
        }

        public bool ValidarColaborador(string correo, string cedula)
        {
            var colaborador = _colaboradorRepo.ObtenerPorCorreoYCedula(correo, cedula);
            return colaborador != null; // Devuelve true si existe
        }
    }
}
