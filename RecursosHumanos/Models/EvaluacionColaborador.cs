using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int Evaluacion{ get; set; }
        [ForeignKey("IdEvaluacion")]

    }
}
