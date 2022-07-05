using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class Item
    {
        public string Id { get; set; }
        public string IdCategoria { get; set; }
        public string Codigo { get; set; }
        public int? RangoNivel { get; set; }
        public bool? Cajon { get; set; }
        public string Descripcion { get; set; }
        public string CajonDetalle { get; set; }
        public string CajonAcabado { get; set; }
        public bool? Estado { get; set; }
    }
}
