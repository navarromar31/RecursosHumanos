using System.ComponentModel.DataAnnotations;
namespace RecursosHumanos_ViewModels
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es obligatorio")]
        public string NombreDepartamento { get; set; }

        [Required(ErrorMessage = "La descripcion del departamento es obligatoria")]
        public string DescripcionDepartamento { get; set; }

        public bool EstadoDepartamento { get; set; }

    }
}
