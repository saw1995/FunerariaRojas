using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class ProductoItemStock
    {
        public Categoria categoria { get; set; }
        public Item item { get; set; }
        public ItemPresentacion item_presentacion { get; set; }
    }
}
