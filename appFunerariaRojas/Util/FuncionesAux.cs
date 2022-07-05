using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appFunerariaRojas.Util
{
    public class FuncionesAux
    {
        public string generarId()
        {
            DateTime fecha = DateTime.Now;
            int random = new Random().Next(1000,9999);
            return random.ToString() + fecha.ToString("ddMMyyHmm");
        }
    }
}
