using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_ViewModels;

namespace RecursosHumanos_ViewModels.ViewModels
{
    public class PreguntaVM
    {
        public Pregunta Pregunta { get; set; }
        public IEnumerable<SelectListItem> CapacitacionLista { get; set; }
        public IEnumerable<SelectListItem> EvaluacionLista { get; set; }

    }
}
