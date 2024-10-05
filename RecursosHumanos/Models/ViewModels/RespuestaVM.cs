using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels
{
    public class RespuestaVM
    {
        public Respuesta respuesta { get; set; }
        public IEnumerable<SelectListItem> ColaboradorLista { get; set; }
        public IEnumerable<SelectListItem> PreguntasLista { get; set; }
    }
}
