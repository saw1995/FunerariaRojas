using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class Compra
    {
        public string Id { get; set; }
        public int Numero { get; set; }
        public string IdProveedor { get; set; }
        public string IdUsuario { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public string Cocepto { get; set; }
        public bool? Estado { get; set; }
    }
}
