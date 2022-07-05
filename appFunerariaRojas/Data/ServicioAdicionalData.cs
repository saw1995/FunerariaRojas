using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models;
using MySql.Data.MySqlClient;

namespace appFunerariaRojas.Data
{
    public class ServicioAdicionalData
    {
        public async Task<ResultadoModel> insertarServicioAdicional(ServicioAdicional param)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "No se agrego el registro bla bla";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "INSERT INTO servicio_adicional(id, nombre, descripcion, precio, estado) "
                            + "VALUES(@id, @nombre, @descripcion, @precio, @estado)";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", param.Id);
                        cmd.Parameters.AddWithValue("@nombre", param.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion", param.Descripcion);
                        cmd.Parameters.AddWithValue("@precio", param.Precio);
                        cmd.Parameters.AddWithValue("@estado", param.Estado);

                        var i = await cmd.ExecuteNonQueryAsync();

                        if (i == 1)
                        {
                            resultado.estado = true;
                            resultado.mensaje = "Se inserto el registro con exito";
                        }
                    }
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


        public async Task<ResultadoModel> actualizarEstadoById(bool estado, string id)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "No se ejecuto la peticion";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "UPDATE servicio_adicional SET estado=@estado WHERE id=@id ";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@id", id);
                        var i = await cmd.ExecuteNonQueryAsync();

                        if (i == 1)
                        {
                            resultado.estado = true;
                            resultado.mensaje = "Se actualizo con exito el registro";
                        }
                    }
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

        public async Task<List<ServicioAdicional>> listaServiciosAdicionalesByEstado(bool estado)
        {
            var resultado = new List<ServicioAdicional>();

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "SELECT id, nombre, descripcion, precio, estado FROM servicio_adicional "
                        + "WHERE estado=@estado ORDER BY nombre ASC";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);

                        using (var drd = await cmd.ExecuteReaderAsync())
                        {
                            while (await drd.ReadAsync())
                            {
                                var oArticulo = new ServicioAdicional
                                {
                                    Id = Convert.ToString(drd["id"]),
                                    Nombre = Convert.ToString(drd["nombre"]),
                                    Descripcion = Convert.ToString(drd["descripcion"]),
                                    Precio = Convert.ToDecimal(drd["precio"]),
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
                return new List<ServicioAdicional>();
            }
        }
    }
}
