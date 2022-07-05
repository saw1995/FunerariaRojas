using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class Categoria
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Cajon { get; set; }
        public bool? Estado { get; set; }
    }
}
