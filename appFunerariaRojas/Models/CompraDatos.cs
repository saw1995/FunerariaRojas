using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class CompraDatos
    {
        public Compra datos { get; set; }
        public Proveedor proveedor { get; set; }
        public Usuario usuario { get; set; }
        public List<CompraDatosDetalleItem> items_compra { get; set; }
    }
}
