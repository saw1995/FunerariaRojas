using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class Proveedor
    {
        public string Id { get; set; }
        public string Nit { get; set; }
        public string Ciudad { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool? Estado { get; set; }
    }
}
