using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_ViewModels;

namespace RecursosHumanos_ViewModels.ViewModels
{
    public class RespuestaVM
    {
        public Respuesta respuesta { get; set; }
        public IEnumerable<SelectListItem> ColaboradorLista { get; set; }
        public IEnumerable<SelectListItem> PreguntasLista { get; set; }
    }
}
