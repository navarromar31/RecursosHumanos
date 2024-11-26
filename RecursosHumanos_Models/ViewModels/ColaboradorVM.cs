using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_ViewModels;

namespace RecursosHumanos_ViewModels.ViewModels
{
    public class ColaboradorVM
    {

        public Colaborador Colaborador { get; set; }
        public IEnumerable<SelectListItem>? Puesto { get; set; }
        public IEnumerable<SelectListItem>? Departamento { get; set; }
        public IEnumerable<SelectListItem>? Institucion { get; set; }


    }
}
