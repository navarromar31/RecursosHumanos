using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    public class CapacitacionVM
    {
        public IEnumerable<Capacitacion> Capacitacion { get; set; }

        public IEnumerable<SelectListItem> CapacitacionLista { get; set; }

    }
}
