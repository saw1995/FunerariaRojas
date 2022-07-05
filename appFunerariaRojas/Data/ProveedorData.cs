using appFunerariaRojas.Models;
using appFunerariaRojas.Models.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appFunerariaRojas.Data
{
    public class ProveedorData
    {

        public async Task<ResultadoModel> insertarProveedor(Proveedor param)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "No se agrego el registro bla bla";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "INSERT INTO proveedor(id, nit, ciudad, razon_social, telefono, direccion, estado) "
                            + "VALUES(@id, @nit, @ciudad, @razon_social, @telefono, @direccion, @estado)";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", param.Id);
                        cmd.Parameters.AddWithValue("@nit", param.Nit);
                        cmd.Parameters.AddWithValue("@ciudad", param.Ciudad);
                        cmd.Parameters.AddWithValue("@razon_social", param.RazonSocial);
                        cmd.Parameters.AddWithValue("@telefono", param.Telefono);
                        cmd.Parameters.AddWithValue("@direccion", param.Direccion);
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
                    string sql = "UPDATE proveedor SET estado=@estado WHERE id=@id ";

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

        public async Task<List<Proveedor>> listaProveedorByEstado(bool estado)
        {
            var resultado = new List<Proveedor>();

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "SELECT id, nit, ciudad, razon_social, telefono, direccion, estado FROM proveedor "
                        + "WHERE estado=@estado ORDER BY razon_social ASC";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);

                        using (var drd = await cmd.ExecuteReaderAsync())
                        {
                            while (await drd.ReadAsync())
                            {
                                var oProveedor = new Proveedor
                                {
                                    Id = Convert.ToString(drd["id"]),
                                    Nit = Convert.ToString(drd["nit"]),
                                    Ciudad = Convert.ToString(drd["ciudad"]),
                                    RazonSocial = Convert.ToString(drd["razon_social"]),
                                    Telefono = Convert.ToString(drd["telefono"]),
                                    Direccion = Convert.ToString(drd["direccion"]),
                                    Estado = Convert.ToBoolean(drd["estado"])
                                };

                                resultado.Add(oProveedor);
                            }
                        }
                    }
                }

                return resultado;
            }
            catch
            {
                return new List<Proveedor>();
            }
        }


    }
}
