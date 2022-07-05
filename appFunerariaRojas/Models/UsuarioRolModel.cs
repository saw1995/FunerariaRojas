using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Models
{
    public class UsuarioRolModel
    {
        public Usuario usuario { get; set; }
        public Rol rol { get; set; }
    }
    public class Rol
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }

}
