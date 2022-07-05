using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class PlanPaquete
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public bool? Estado { get; set; }
    }
}
