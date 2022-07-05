using appFunerariaRojas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Data
{
    public class CategoriaData
    {
        public async Task<ResultadoModel> insertarCategoría(Categoria param)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "No se agrego el registro bla bla";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "INSERT INTO categoria(id, nombre, descripcion, cajon, estado) "
                            + "VALUES(@id, @nombre, @descripcion, @cajon, @estado)";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", param.Id);
                        cmd.Parameters.AddWithValue("@nombre", param.Nombre);
                        cmd.Parameters.AddWithValue("@descripcion", param.Descripcion);
                        cmd.Parameters.AddWithValue("@cajon", param.Cajon);
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
                    string sql = "UPDATE categoria SET estado=@estado WHERE id=@id ";

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
