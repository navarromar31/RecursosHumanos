using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels
{
    public class PreguntaVM
    {
        public Pregunta Pregunta { get; set; }
        public IEnumerable<SelectListItem>CapacitacionLista { get; set; }
        public IEnumerable<SelectListItem> EvaluacionLista { get; set; }

    }
}
