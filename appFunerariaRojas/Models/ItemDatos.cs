using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class ItemDatos
    {
        public Item item { get; set; }
        public ItemPresentacion presentacion { get; set; }
        public List<ItemImagenes> imagenes { get; set; }
    }
}
