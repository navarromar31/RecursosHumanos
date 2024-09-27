using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecursosHumanos.Models
{
    public class Respuesta
    {
        [Key]
        public int Id { get; set; }
        
        public int IdPregunta { get; set; }
        [ForeignKey("IdPregunta")]

        public int ColaboradorEvaluado { get; set; }
        [ForeignKey("IdColaboradorEvaluado")]

        public int ColaboradorEvaluador { get; set; }
        [ForeignKey("IdColaboradorEvaluador")]

        public int ValorRespuesta { get; set; }
    }
}
