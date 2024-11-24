using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursosHumanos_Models.ViewModels
{
    public class EvaluacionUsuarioVM
    {

       

        public EvaluacionUsuarioVM()
        {
            EvaluacionLista = new List<Evaluacion>();
        }

        //pROPIEDAD PARA OBTENER LOS DATOS DEL CLIENTE QUE TENGA ESA SESION
        public UsuarioAplicacion UsuarioAplicacion { get; set; }
        //LA LISTA DE PRODUCTOS QUE TENGA ESE CLIENTE
        public IList<Evaluacion> EvaluacionLista { get; set; }
    }
}
