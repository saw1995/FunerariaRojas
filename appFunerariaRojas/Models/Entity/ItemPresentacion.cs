using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class ItemPresentacion
    {
        public string Id { get; set; }
        public string IdItem { get; set; }
        public string Descripcion { get; set; }
        public bool? Cajon { get; set; }
        public string Color { get; set; }
        public string UnidadMedida { get; set; }
        public string Tamaño { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }
        public string Imagen { get; set; }
        public int? Stock { get; set; }
        public bool? Estado { get; set; }
    }
}
