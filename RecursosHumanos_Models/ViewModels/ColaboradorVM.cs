using Microsoft.AspNetCore.Mvc.Rendering;
using RecursosHumanos_Models;

namespace RecursosHumanos_Models.ViewModels
{
    public class ColaboradorVM
    {

        public Colaborador Colaborador { get; set; }
        public IEnumerable<SelectListItem>? NombreColaborador { get; set; }

        public IEnumerable<SelectListItem>? CedulaColaborador { get; set; }

        public IEnumerable<SelectListItem>? CorreoColaborador { get; set; }

        public IEnumerable<SelectListItem>? Puesto { get; set; }
        public IEnumerable<SelectListItem>? Departamento { get; set; }
        public IEnumerable<SelectListItem>? Institucion { get; set; }

        public IEnumerable<Colaborador>? Colaboradores { get; set; }

    }
}
