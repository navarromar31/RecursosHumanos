using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using System.ComponentModel.DataAnnotations;
using System;

public class LoginRRHH : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginRRHH> _logger;
    private readonly IColaboradorRepositorio _colaboradorRepo;

    public LoginRRHH(SignInManager<IdentityUser> signInManager, ILogger<LoginRRHH> logger, IColaboradorRepositorio colaboradorRepo)
    {
        _signInManager = signInManager;
        _logger = logger;
        _colaboradorRepo = colaboradorRepo;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Cedula { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [BindProperty]
        public LoginInput Input { get; set; }

        public string ReturnUrl { get; set; }
    }
       

    // Asegúrate de usar [FromBody] para recibir el cuerpo de la solicitud como JSON
    public async Task<JsonResult> OnPostAsync([FromBody] LoginInput input, string returnUrl = null)
    {
        _logger.LogInformation("Login attempt for user: {Email}", input.Email); // Log para verificar si está entrando

        returnUrl ??= Url.Content("~/");

        // Validar existencia en la base de datos
        var colaborador = await _colaboradorRepo.ObtenerPorCorreoYCedulaAsync(input.Email, input.Cedula);
        if (colaborador == null)
        {
            _logger.LogWarning("Colaborador no encontrado con correo: {Email} y cédula: {Cedula}", input.Email, input.Cedula);
            return new JsonResult(new { success = false, errorMessage = "Credenciales inválidas." });
        }

        // Buscar el usuario asociado en Identity
        var user = await _signInManager.UserManager.FindByEmailAsync(input.Email);
        if (user == null)
        {
            _logger.LogWarning("Usuario no encontrado con correo: {Email}", input.Email);
            return new JsonResult(new { success = false, errorMessage = "Usuario no encontrado en el sistema." });
        }

        // Usar la cédula como contraseña para autenticación
        var result = await _signInManager.PasswordSignInAsync(user, input.Cedula, input.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            _logger.LogInformation("Inicio de sesión exitoso para usuario: {Email}", input.Email);
            return new JsonResult(new { success = true });
        }
        else
        {
            _logger.LogWarning("Error en inicio de sesión para usuario: {Email}", input.Email);
            return new JsonResult(new { success = false, errorMessage = "Error en el inicio de sesión." });
        }
    }

    // Input model para capturar el email, cédula y la opción de "recordar"
    public class LoginInput
    {
        public string Email { get; set; }
        public string Cedula { get; set; }
        public bool RememberMe { get; set; }
    }
}