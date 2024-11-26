using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    public class PuestoVM
    {
        public IEnumerable<Puesto> Puesto { get; set; }

        public IEnumerable<SelectListItem> PuestoLista { get; set; }
    }
}