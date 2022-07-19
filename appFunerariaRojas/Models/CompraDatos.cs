using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class CompraDatos
    {
        public decimal total { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public Compra datos { get; set; }
        public Proveedor proveedor { get; set; }
        public UsuarioRolModel usuario { get; set; }
        public List<CompraDatosDetalleItem> items_compra { get; set; }
    }
}
