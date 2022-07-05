using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class PlanPaqueteServicios
    {
        public string Id { get; set; }
        public string IdPlanPaquete { get; set; }
        public string IdServicioAdicional { get; set; }
        public decimal? PrecioCosto { get; set; }
        public bool? Estado { get; set; }
    }
}
