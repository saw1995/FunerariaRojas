using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class CompraDatosDetalleItem
    {
        public ItemDatos producto { get; set; }
        public CompraDetalle detalle { get; set; }
    }
}
