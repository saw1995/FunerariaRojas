using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appFunerariaRojas.Models.Entity
{
    public class PlanPaqueteImagenes
    {
        public string Id { get; set; }
        public string IdPlanPaquete { get; set; }
        public string Imagen { get; set; }
        public bool? Estado { get; set; }
    }
}
