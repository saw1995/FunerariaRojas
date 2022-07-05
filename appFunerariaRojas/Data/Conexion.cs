using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appFunerariaRojas.Data
{
    public class Conexion
    {
        public string cn()
        {
            var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("connectionStrings")["defaultConnection"];

            return AppName;
        }
    }
}
