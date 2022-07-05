using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models;
using MySql.Data.MySqlClient;

namespace appFunerariaRojas.Data
{
    public class ComprasData
    {
        public async Task<List<CompraDatos>> listaComprasByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var result = new List<CompraDatos>();

            using(var con = new MySqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT compra.id, compra.numero, compra.id_proveedor, compra.id_usuario, "
                    + "compra.fecha, compra.hora, compra.cocepto, compra.estado, "
                    + "proveedor.razon_social, proveedor.nit, usuario.nombre, usuario.appaterno, usuario.apmaterno "
                    + "FROM compra INNER JOIN proveedor ON compra.id_proveedor = proveedor.id "
                    + "INNER JOIN usuario ON usuario.id = compra.id_usuario "
                    + "WHERE compra.fecha BETWEEN @fecha1 AND @fecha2 AND compra.estado=True "
                    + "ORDER BY compra.fecha DESC ";

                using(var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@fecha1", fechaInicio.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@fecha2", fechaFin.ToString("yyyy-MM-dd"));

                    using(var drd = await cmd.ExecuteReaderAsync())
                    {
                        while(await drd.ReadAsync())
                        {
                            var oCompraDatos = new CompraDatos();

                            var oCompra = new Compra
                            {
                                Id = Convert.ToString(drd["id"]),
                                Numero = Convert.ToInt32(drd["numero"]),
                                IdProveedor = Convert.ToString(drd["id_proveedor"]),
                                IdUsuario = Convert.ToString(drd["id_usuario"]),
                                Fecha = Convert.ToDateTime(drd["fecha"]),
                                Hora = (TimeSpan)(drd["hora"]),
                                Cocepto = Convert.ToString(drd["cocepto"])
                            };

                            oCompraDatos.datos = oCompra;

                            var oProveedor = new Proveedor
                            {
                                RazonSocial = Convert.ToString(drd["razon_social"]),
                                Nit = Convert.ToString(drd["nit"])
                            };

                            oCompraDatos.proveedor = oProveedor;

                            var usuario = new Usuario
                            {
                                Nombre = Convert.ToString(drd["nombre"]),
                                Appaterno = Convert.ToString(drd["appaterno"]),
                                Apmaterno = Convert.ToString(drd["apmaterno"]),
                            };

                            oCompraDatos.usuario = usuario;

                            result.Add(oCompraDatos);
                        }
                    }
                }
            }

            return result;
        }
    }
}
