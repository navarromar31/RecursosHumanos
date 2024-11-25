using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    public class PreguntaVM
    {
        public Pregunta Pregunta { get; set; }
        public IEnumerable<Pregunta> Preguntas { get; set; }
        public IEnumerable<SelectListItem> CapacitacionLista { get; set; }

    }
}
