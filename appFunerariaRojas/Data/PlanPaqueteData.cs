using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models;
using MySql.Data.MySqlClient;

namespace appFunerariaRojas.Data
{
    public class PlanPaqueteData
    {
        public async Task<ResultadoModel> insertarPlanPaqueteDetallado(PlanPaquete planPaquete, List<PlanPaqueteImagenes> imagenes, List<PlanPaqueteItems> items, List<PlanPaqueteServicios> servicios)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "No se ejecuto la peticion";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "INSERT INTO plan_paquete(id, nombre, descripcion, precio, estado) "
                            + "VALUES(@id, @nombre, @descripcion, @precio, @estado)";

                    using(var cmdPlan = new MySqlCommand(sql, con))
                    {
                        cmdPlan.Parameters.AddWithValue("@id", planPaquete.Id);
                        cmdPlan.Parameters.AddWithValue("@nombre", planPaquete.Nombre);
                        cmdPlan.Parameters.AddWithValue("@descripcion", planPaquete.Descripcion);
                        cmdPlan.Parameters.AddWithValue("@precio", planPaquete.Precio);
                        cmdPlan.Parameters.AddWithValue("@estado", planPaquete.Estado);
                        await cmdPlan.ExecuteNonQueryAsync();

                        sql = "INSERT INTO plan_paquete_imagenes(id, id_plan_paquete, imagen, estado)  "
                            + "VALUES(@id, @id_plan_paquete, @imagen, @estado)";

                        using(var cmdImagenes = new MySqlCommand(sql, con))
                        {
                            cmdImagenes.Parameters.Add("@id", MySqlDbType.VarChar, 50);
                            cmdImagenes.Parameters.Add("@id_plan_paquete", MySqlDbType.VarChar, 50);
                            cmdImagenes.Parameters.Add("@imagen", MySqlDbType.LongText);
                            cmdImagenes.Parameters.Add("@estado", MySqlDbType.Bit);
                            
                            foreach(var imagen in imagenes)
                            {
                                cmdImagenes.Parameters["@id"].Value = imagen.Id;
                                cmdImagenes.Parameters["@id_plan_paquete"].Value = planPaquete.Id;
                                cmdImagenes.Parameters["@imagen"].Value = imagen.Imagen;
                                cmdImagenes.Parameters["@estado"].Value = imagen.Estado;
                                await cmdImagenes.ExecuteNonQueryAsync();
                            }

                            sql = "INSERT INTO plan_paquete_items(id, id_plan_paquete, id_item_presentacion, precio_costo, cantidad, estado) "
                                + "VALUES(@id, @id_plan_paquete, @id_item_presentacion, @precio_costo, @cantidad, @estado)";

                            using (var cmdItems = new MySqlCommand(sql, con))
                            {
                                cmdItems.Parameters.Add("@id", MySqlDbType.VarChar, 50);
                                cmdItems.Parameters.Add("@id_plan_paquete", MySqlDbType.VarChar, 50);
                                cmdItems.Parameters.Add("@id_item_presentacion", MySqlDbType.VarChar, 50);
                                cmdItems.Parameters.Add("@precio_costo", MySqlDbType.Decimal);
                                cmdItems.Parameters.Add("@cantidad", MySqlDbType.Int32);
                                cmdItems.Parameters.Add("@estado", MySqlDbType.Bit);

                                foreach (var item in items)
                                {
                                    cmdItems.Parameters["@id"].Value = item.Id;
                                    cmdItems.Parameters["@id_plan_paquete"].Value = planPaquete.Id;
                                    cmdItems.Parameters["@id_item_presentacion"].Value = item.IdItemPresentacion;
                                    cmdItems.Parameters["@precio_costo"].Value = item.PrecioCosto;
                                    cmdItems.Parameters["@cantidad"].Value = item.Cantidad;
                                    cmdItems.Parameters["@estado"].Value = item.Estado;
                                    await cmdItems.ExecuteNonQueryAsync();
                                }

                                sql = "INSERT INTO plan_paquete_servicios(id, id_plan_paquete, id_servicio_adicional, precio_costo, estado) "
                                    + "VALUES(@id, @id_plan_paquete, @id_servicio_adicional, @precio_costo, @estado)";

                                using(var cmdServicios = new MySqlCommand(sql, con))
                                {
                                    cmdServicios.Parameters.Add("@id", MySqlDbType.VarChar, 50);
                                    cmdServicios.Parameters.Add("@id_plan_paquete", MySqlDbType.VarChar, 50);
                                    cmdServicios.Parameters.Add("@id_servicio_adicional", MySqlDbType.VarChar, 50);
                                    cmdServicios.Parameters.Add("@precio_costo", MySqlDbType.Decimal);
                                    cmdServicios.Parameters.Add("@estado", MySqlDbType.Bit);

                                    foreach(var servicio in servicios)
                                    {
                                        cmdServicios.Parameters["@id"].Value = servicio.Id;
                                        cmdServicios.Parameters["@id_plan_paquete"].Value = planPaquete.Id;
                                        cmdServicios.Parameters["@id_servicio_adicional"].Value = servicio.IdServicioAdicional;
                                        cmdServicios.Parameters["@precio_costo"].Value = servicio.PrecioCosto;
                                        cmdServicios.Parameters["@estado"].Value = servicio.Estado;
                                        await cmdServicios.ExecuteNonQueryAsync();
                                    }
                                }
                            }

                        }
                    }

                    resultado.estado = true;
                    resultado.mensaje = "procesos ejecutados con exito";

                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.estado = false;
                resultado.mensaje = ex.Message.ToString();
                return resultado;
            }
        }

        public async Task<List<Categoria>> listaCategoriasByEstado(bool estado)
        {
            var resultado = new List<Categoria>();

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "SELECT id, nombre, descripcion, cajon, estado FROM categoria "
                        + "WHERE estado=@estado ORDER BY nombre ASC";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);

                        using (var drd = await cmd.ExecuteReaderAsync())
                        {
                            while (await drd.ReadAsync())
                            {
                                var oArticulo = new Categoria
                                {
                                    Id = Convert.ToString(drd["id"]),
                                    Nombre = Convert.ToString(drd["nombre"]),
                                    Descripcion = Convert.ToString(drd["descripcion"]),
                                    Cajon = Convert.ToBoolean(drd["cajon"]),
                                    Estado = Convert.ToBoolean(drd["estado"])
                                };

                                resultado.Add(oArticulo);
                            }
                        }
                    }
                }

                return resultado;
            }
            catch
            {
                return new List<Categoria>();
            }
        }
    }
}
