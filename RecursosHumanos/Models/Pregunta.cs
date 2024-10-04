using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;
using ProyectoYo.Models;

namespace RecursosHumanos.Models
{
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }
        public int IdCapacitacion { get; set; }
        [ForeignKey("IdCapacitacion")]
        public virtual Capacitacion? Capacitacion { get; set; }
        public int IdEvaluacion { get; set; }
        [ForeignKey("IdEvaluacion")]
        public virtual Evaluacion? Evaluacion { get; set; }

        [Required(ErrorMessage = "El texto de la pregunta es obligatoria")]
        public string Texto { get; set; }
        public int InicioEscala { get; set; }
        public int FinalEscala { get; set; }
        public bool EstadoPregunta { get; set; }    
    }
}
