using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class Puesto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del puesto es obligatorio")]
        public string NombrePuesto { get; set; }

        [Required(ErrorMessage = "La descripcion del puesto es obligatoria")]
        public string DescripcionPuesto { get; set; }

    }
}
