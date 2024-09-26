using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es obligatorio")]
        public string NombreDepartamento { get; set; }

        [Required(ErrorMessage = "La descripcion del departamento es obligatoria")]
        public string DescripcionPuesto { get; set; }

        [Required(ErrorMessage = "El estado del departamento es obligatoria")]
        public string EstadoDepartamento { get; set; }
    }
}
