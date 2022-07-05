using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class ItemImagenes
    {
        public string Id { get; set; }
        public string IdItem { get; set; }
        public string Imagen { get; set; }
        public bool? Estado { get; set; }
    }
}
