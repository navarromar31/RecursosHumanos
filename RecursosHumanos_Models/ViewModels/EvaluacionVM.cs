using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    public class EvaluacionVM
    {
        public Evaluacion Evaluacion { get; set; }
        public IEnumerable<Evaluacion> EvaluacionListaDB{ get; set; }
    }
}
