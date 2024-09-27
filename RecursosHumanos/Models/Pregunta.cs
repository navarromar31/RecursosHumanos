using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecursosHumanos.Models
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }

        public int IdPregunta { get; set; }
        [ForeignKey("IdPregunta")]

        public int IdCapacitacion { get; set; }
        [ForeignKey("IdCapacitacion")]

        public int IdEvaluacion { get; set; }
        [ForeignKey("IdEvaluacion")]

        [Required(ErrorMessage = "El texto de la pregunta es obligatoria")]
        public string Texto { get; set; }

        public int InicioEscala { get; set; }
        public int FinalEscala { get; set; }
    }
}
