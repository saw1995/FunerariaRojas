using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class VentaPagos
    {
        public string Id { get; set; }
        public string IdUsuario { get; set; }
        public string IdVenta { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public decimal? SaldoInicial { get; set; }
        public decimal? SaldoActual { get; set; }
        public decimal? SaldoCancelado { get; set; }
        public bool? Estado { get; set; }
    }
}
