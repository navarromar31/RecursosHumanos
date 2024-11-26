using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecursosHumanos_Models;

namespace RecursosHumanos_Models
{
    public class Capacitacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre de la categoria obligatorio")]
        public string NombreCapacitacion { get; set; }
        public string Duracion { get; set; }
        [Required(ErrorMessage = "El minimo de personas es obligatorio")]
        //El orden de los datos sea mayor a 0, no puede ser un numero negativo
        [Range(1, double.MaxValue, ErrorMessage = "El minimo de personas tiene que ser mayor a 0")]
        public int MinPersonas { get; set; }
        public int MaxPersonas { get; set; }
        public string Modalidad { get; set; }
        public string? ImagenUrlCap { get; set; }

        public bool EstadoCapacitacion { get; set; }

    }
}
