using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    namespace RecursosHumanos_Models.ViewModels
    {
        public class HomeVM
        {
            public IEnumerable<Colaborador> Colaborador { get; set; }
            public IEnumerable<Capacitacion> Capacitacion { get; set; }
            public IEnumerable<Evaluacion> Evaluacion { get; set; }

        }
    }
}