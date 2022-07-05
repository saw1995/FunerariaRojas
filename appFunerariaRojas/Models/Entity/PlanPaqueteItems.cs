using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class PlanPaqueteItems
    {
        public string Id { get; set; }
        public string IdPlanPaquete { get; set; }
        public string IdItemPresentacion { get; set; }
        public decimal? PrecioCosto { get; set; }
        public int? Cantidad { get; set; }
        public bool? Estado { get; set; }
    }
}
