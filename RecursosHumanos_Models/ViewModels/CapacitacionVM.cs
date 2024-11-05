using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_Models;

namespace RecursosHumanos_Models.ViewModels
{
    public class CapacitacionVM
    {
        public Capacitacion Capacitacion { get; set; }

        public IEnumerable<SelectListItem>? ColaboradorLista { get; set; }
    }
}
