using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    public class InstitucionVM
    {
        public IEnumerable<Institucion> Institucion { get; set; }

        public IEnumerable<SelectListItem> InstitucionLista { get; set; }
    }
}
