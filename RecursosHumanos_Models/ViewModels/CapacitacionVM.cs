using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_ViewModels;

namespace RecursosHumanos_ViewModels.ViewModels
{
    public class CapacitacionVM
    {
        public Capacitacion Capacitacion { get; set; }

        public IEnumerable<SelectListItem>? ColaboradorLista { get; set; }
    }
}
