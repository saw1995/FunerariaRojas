using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class productoAgregarParameter
    {
        public Item item { get; set; }
        public List<ItemImagenes> imagenes { get; set; }
        public List<ItemPresentacion> presentaciones { get; set; }
    }
}
