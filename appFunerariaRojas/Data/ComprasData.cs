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
        public async Task<ResultadoModel> insertarDetalleCompras(Compra compra, List<CompraDetalle> detallesCompra)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "no respuesta";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();

                    string sql = "INSERT INTO compra(id, numero, id_proveedor, id_usuario, fecha, hora, cocepto, estado) "
                        + "VALUES(@id, @numero, @id_proveedor, @id_usuario, @fecha, @hora, @cocepto, @estado)";

                    using (var cmdCompra = new MySqlCommand(sql, con))
                    {
                        cmdCompra.Parameters.AddWithValue("@id", compra.Id);
                        cmdCompra.Parameters.AddWithValue("@numero", compra.Numero);
                        cmdCompra.Parameters.AddWithValue("@id_proveedor", compra.IdProveedor);
                        cmdCompra.Parameters.AddWithValue("@id_usuario", compra.IdUsuario);
                        cmdCompra.Parameters.AddWithValue("@fecha", compra.Fecha);
                        cmdCompra.Parameters.AddWithValue("@hora", compra.Hora);
                        cmdCompra.Parameters.AddWithValue("@cocepto", compra.Cocepto);
                        cmdCompra.Parameters.AddWithValue("@estado", compra.Estado);
                        await cmdCompra.ExecuteNonQueryAsync();

                        sql = "INSERT INTO compra_detalle(id_compra, id_item_presentacion, precio, cantidad, estado) "
                            + "VALUES(@id_compra, @id_item_presentacion, @precio, @cantidad, @estado) ";

                        using (var cmdDetalle = new MySqlCommand(sql, con))
                        {
                            cmdDetalle.Parameters.Add("@id_compra", MySqlDbType.VarChar, 50);
                            cmdDetalle.Parameters.Add("@id_item_presentacion", MySqlDbType.VarChar, 50);
                            cmdDetalle.Parameters.Add("@precio", MySqlDbType.Decimal);
                            cmdDetalle.Parameters.Add("@cantidad", MySqlDbType.Int32);
                            cmdDetalle.Parameters.Add("@estado", MySqlDbType.Bit);

                            foreach (var detalle in detallesCompra)
                            {
                                cmdDetalle.Parameters["@id_compra"].Value = compra.Id;
                                cmdDetalle.Parameters["@id_item_presentacion"].Value = detalle.IdItemPresentacion;
                                cmdDetalle.Parameters["@precio"].Value = detalle.Precio;
                                cmdDetalle.Parameters["@cantidad"].Value = detalle.Cantidad;
                                cmdDetalle.Parameters["@estado"].Value = detalle.Estado;
                                await cmdDetalle.ExecuteNonQueryAsync();

                                string query = "SELECT stock FROM item_presentacion WHERE id=@id";

                                int stock = 0;

                                using (var cmdBusqueda = new MySqlCommand(query, con))
                                {
                                    cmdBusqueda.Parameters.AddWithValue("@id", detalle.IdItemPresentacion);

                                    var obj = await cmdBusqueda.ExecuteScalarAsync();

                                    stock = Convert.ToString(obj) == "" ? 0 : Convert.ToInt32(obj);
                                }

                                query = "UPDATE item_presentacion SET stock=@stock WHERE id=@id ";

                                using (var cmdActualizar = new MySqlCommand(query, con))
                                {
                                    cmdActualizar.Parameters.AddWithValue("@stock", (stock + detalle.Cantidad));
                                    cmdActualizar.Parameters.AddWithValue("@id", detalle.IdItemPresentacion);

                                    await cmdActualizar.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }

                    resultado.estado = true;
                    resultado.mensaje = "Aqui se completo las tareas, like :D";
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                resultado.estado = false;
                resultado.mensaje = ex.Message.ToString();
                return resultado;
            }
        }

        public async Task<int> numeroCompraGenerado()
        {
            using(var con = new MySqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT MAX(numero) FROM compra";

                using(var cmd = new MySqlCommand(sql, con))
                {
                    var obj = await cmd.ExecuteScalarAsync();

                    return Convert.ToString(obj) == "" ? 10001 : Convert.ToInt32(obj) + 1;
                }
            }
        }

        public async Task<List<CompraDatos>> listaComprasByFecha(DateTime fechaInicio, DateTime fechaFin, string id_proveedor, string id_usuario)
        {
            var result = new List<CompraDatos>();

            using(var con = new MySqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string queryUsuario = id_usuario != "0" ? " AND compra.id_usuario='" + id_usuario + "' ": "";
                string queryProveedor = id_proveedor != "0" ? " AND compra.id_proveedor='" + id_proveedor + "' " : "";

                string sql = "SELECT compra.id, compra.numero, compra.fecha, compra.hora, "
                           + "compra.cocepto, usuario.id as 'id_usuario', usuario.id_rol, usuario.nombre, "
                           + "usuario.appaterno, usuario.apmaterno, proveedor.id as 'id_proveedor', proveedor.nit, "
                           + "proveedor.razon_social, sum(compra_detalle.precio * compra_detalle.cantidad) as 'total' "
                           + "FROM compra INNER JOIN compra_detalle ON compra.id = compra_detalle.id_compra "
                           + "INNER JOIN proveedor ON compra.id_proveedor = proveedor.id "
                           + "INNER JOIN usuario ON usuario.id = compra.id_usuario "
                           + "WHERE compra.fecha BETWEEN @fecha1 AND @fecha2 AND compra.estado = True "
                           + queryUsuario + queryProveedor
                           + "GROUP BY compra.id ORDER BY compra.fecha DESC ";

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

                            var usuario = new UsuarioRolModel
                            {
                                rol = new Rol {
                                    nombre = Convert.ToInt32(drd["id_rol"]) == 1 ? "Administrador" : "Vendedor"
                                },
                                usuario = new Usuario {
                                    Nombre = Convert.ToString(drd["nombre"]),
                                    Appaterno = Convert.ToString(drd["appaterno"]),
                                    Apmaterno = Convert.ToString(drd["apmaterno"]),
                                }
                            };

                            oCompraDatos.fecha = Convert.ToDateTime(oCompra.Fecha).ToString("dd/MMMM/yyyy");
                            oCompraDatos.hora = oCompra.Hora.Value.Hours.ToString("00") + ":" + oCompra.Hora.Value.Minutes.ToString("00");
                            oCompraDatos.usuario = usuario;
                            oCompraDatos.total = Convert.ToDecimal(drd["total"]);

                            result.Add(oCompraDatos);
                        }
                    }
                }
            }

            return result;
        }
    }
}
