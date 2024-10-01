using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecursosHumanos.Models;

namespace RecursosHumanos.Models
{
    public class EvaluacionColaborador
    {
        [Key]
        public int Id { get; set; }

        public int ColaboradorEvaluado { get; set; }
        [ForeignKey("IdColaboradorEvaluado")]

        public int ColaboradorEvaluador { get; set; }
        [ForeignKey("IdColaboradorEvaluador")]

        public int EvaluacionId { get; set; }
        [ForeignKey("EvaluacionId")]
        public virtual Evaluacion? Evaluacion { get; set; }

    }
}
