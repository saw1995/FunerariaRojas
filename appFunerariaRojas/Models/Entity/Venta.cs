using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class Venta
    {
        public string Id { get; set; }
        public string PlanPaquete { get; set; }
        public string IdCliente { get; set; }
        public string IdUsuario { get; set; }
        public int? Correlativo { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public string Concepto { get; set; }
        public bool? Venta1 { get; set; }
        public string CiFallecido { get; set; }
        public string CiExpFallecido { get; set; }
        public string CiudadFallecido { get; set; }
        public string NombreFallecido { get; set; }
        public string AppaternoFallecido { get; set; }
        public string ApmaternoFallecido { get; set; }
        public DateTime? FechaNacimientoFallecido { get; set; }
        public DateTime? FechaFallecimientoFallecido { get; set; }
        public string DetalleFallecimiento { get; set; }
        public bool? CertDefuncion { get; set; }
        public string CertDefuncionImg { get; set; }
        public bool? CertMedico { get; set; }
        public string CertMedicoImg { get; set; }
        public DateTime? CortejoFecha { get; set; }
        public TimeSpan? CortejoHora { get; set; }
        public string CorejoDireccion { get; set; }
        public bool? Estado { get; set; }
    }
}
