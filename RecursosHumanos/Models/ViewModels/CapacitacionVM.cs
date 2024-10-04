using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos.Models;

namespace RecursosHumanos.Models.ViewModels
{
    public class CapacitacionVM
    {
        public Capacitacion Capacitacion { get; set; }

        public IEnumerable<SelectListItem>? ColaboradorLista { get; set; }
    }
}
