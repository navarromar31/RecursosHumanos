using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecursosHumanos.Models
{
    public class Evaluacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El titulo de la evaluacion es obligatoria")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripcion de la evaluacion es obligatoria")]
        public string Descripcion { get; set; }

      
        public int ColaboradorEvaluado { get; set; }
        [ForeignKey("IdColaboradorEvaluado")]

        public int ColaboradorEvaluador { get; set; }
        [ForeignKey("IdColaboradorEvaluador")]

        public string TipoEvaluacion { get; set; }

    }

}
