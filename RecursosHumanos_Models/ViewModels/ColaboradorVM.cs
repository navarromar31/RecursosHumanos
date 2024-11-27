using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecursosHumanos_Models.ViewModels
{
    public class ColaboradorVM
    {
        public Colaborador Colaborador { get; set; }

        public IEnumerable<SelectListItem> InstitucionLista { get; set; }
        public IEnumerable<SelectListItem> DepartamentoLista { get; set; }
        public IEnumerable<SelectListItem> PuestoLista { get; set; }
        public IEnumerable<Colaborador> ColaboradorLista { get; set; }

    }
}
