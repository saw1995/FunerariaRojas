using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models;

namespace appFunerariaRojas.Models
{
    public class CompraDetalleParameter
    {
        public Compra compra { get; set; }
        public List<CompraDetalle> detalle { get; set; }
    }
}
