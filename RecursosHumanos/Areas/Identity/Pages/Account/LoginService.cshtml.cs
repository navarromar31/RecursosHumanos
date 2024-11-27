// ValidadorLoginService.cs
using System.Threading.Tasks;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Identity;

namespace RecursosHumanos.Areas.Identity.Pages.Account
{
    public class LoginService
    {
        private readonly IColaboradorRepositorio _colaboradorRepo;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginService(IColaboradorRepositorio colaboradorRepo, SignInManager<IdentityUser> signInManager)
        {
            _colaboradorRepo = colaboradorRepo;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> ValidarYAutenticarAsync(string email, string cedula, bool rememberMe)
        {
            // Validar si el colaborador existe con el correo y la cédula
            var colaborador = _colaboradorRepo.ObtenerPorCorreoYCedula(email, cedula);
            if (colaborador == null)
            {
                return SignInResult.Failed;  // Si no existe, fallamos el login
            }

            // Si el colaborador existe, buscamos el usuario por su correo
            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return SignInResult.Failed;  // Si el usuario no existe, fallamos el login
            }

            // Intentamos hacer login usando la cédula como contraseña
            var result = await _signInManager.PasswordSignInAsync(user, cedula, rememberMe, lockoutOnFailure: false);
            return result;
        }
    }
}

