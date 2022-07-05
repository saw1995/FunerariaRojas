using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class VentaItemPresentacion
    {
        public string Id { get; set; }
        public string IdVenta { get; set; }
        public string IdItemPresentacion { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public bool? Estado { get; set; }
    }
}
