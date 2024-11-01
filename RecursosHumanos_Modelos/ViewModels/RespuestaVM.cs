using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_Models;

namespace RecursosHumanos_Models.ViewModels
{
    public class RespuestaVM
    {
        public Respuesta respuesta { get; set; }
        public IEnumerable<SelectListItem> ColaboradorLista { get; set; }
        public IEnumerable<SelectListItem> PreguntasLista { get; set; }
    }
}
