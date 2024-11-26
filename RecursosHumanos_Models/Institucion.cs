using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos_ViewModels
{
    public class Institucion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la institucion es obligatorio")]
        public string NombreInstitucion { get; set; }

        [Required(ErrorMessage = "La descripcion de la institucion es obligatoria")]
        public string DescripcionInstitucion { get; set; }
        public string? ImagenUrlInstitucion{ get; set; }

        public bool EstadoInstitucion { get; set; }

    }
}
