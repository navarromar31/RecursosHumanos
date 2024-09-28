using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos.Models.ViewModels
{
    public class ColaboradorVM
    {

        public ColaboradorVM Colaborador { get; set; }

        public IEnumerable<SelectListItem>? Puesto { get; set; }
        public IEnumerable<SelectListItem>? Departamento { get; set; }
        public IEnumerable<SelectListItem>? Institucion { get; set; }


    }
}
