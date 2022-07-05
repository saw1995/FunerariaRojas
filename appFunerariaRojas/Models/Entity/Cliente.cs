using System;
using System.Collections.Generic;

namespace appFunerariaRojas.Models.Entity
{
    public partial class Cliente
    {
        public string Id { get; set; }
        public string Ci { get; set; }
        public string CiExp { get; set; }
        public string Ciudad { get; set; }
        public string Nombre { get; set; }
        public string Appaterno { get; set; }
        public string Apmaterno { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool? Estado { get; set; }
    }
}
