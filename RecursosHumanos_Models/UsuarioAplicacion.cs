using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_ViewModels
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string NombreCompleto { get; set; }
        [NotMapped]
        public string Correo { get; set; }
        [NotMapped]
        public string Contrasena { get; set; }
    }
}
