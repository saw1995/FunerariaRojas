using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class CompraDetalle
    {
        public int Id { get; set; }
        public string IdCompra { get; set; }
        public string IdItemPresentacion { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public bool? Estado { get; set; }
    }
}
