using appFunerariaRojas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Data
{
    public class ProductoData
    {
        public async Task<ResultadoModel> insertarProductoDetallesImagenes(Item _item, List<ItemImagenes> imagenes, List<ItemPresentacion> presentaciones)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "Peticion no ejecutada cone xito";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();

                    string sql = "INSERT INTO item(id, id_categoria, codigo, rango_nivel, cajon, descripcion, cajon_detalle, cajon_acabado, estado)  "
                        + "VALUES(@id, @id_categoria, @codigo, @rango_nivel, @cajon, @descripcion, @cajon_detalle, @cajon_acabado, @estado)";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", _item.Id);
                        cmd.Parameters.AddWithValue("@id_categoria", _item.IdCategoria);
                        cmd.Parameters.AddWithValue("@codigo", _item.Codigo);
                        cmd.Parameters.AddWithValue("@rango_nivel", _item.RangoNivel);
                        cmd.Parameters.AddWithValue("@cajon", _item.Cajon);
                        cmd.Parameters.AddWithValue("@descripcion", _item.Descripcion);
                        cmd.Parameters.AddWithValue("@cajon_detalle", _item.CajonDetalle);
                        cmd.Parameters.AddWithValue("@cajon_acabado", _item.CajonAcabado);
                        cmd.Parameters.AddWithValue("@estado", _item.Estado);
                        await cmd.ExecuteNonQueryAsync();

                        sql = "INSERT INTO item_imagenes(id, id_item, imagen, estado) "
                            + "VALUES(@id, @id_item, @imagen, @estado)";

                        using (var cmd2 = new MySqlCommand(sql, con))
                        {
                            cmd2.Parameters.Add("@id", MySqlDbType.VarChar, 50);
                            cmd2.Parameters.Add("@id_item", MySqlDbType.VarChar, 50);
                            cmd2.Parameters.Add("@imagen", MySqlDbType.LongText);
                            cmd2.Parameters.Add("@estado", MySqlDbType.Bit);

                            foreach (var imagen in imagenes)
                            {
                                cmd2.Parameters["@id"].Value = imagen.Id;
                                cmd2.Parameters["@id_item"].Value = _item.Id;
                                cmd2.Parameters["@imagen"].Value = imagen.Imagen;
                                cmd2.Parameters["@estado"].Value = imagen.Estado;
                                await cmd2.ExecuteNonQueryAsync();
                            }

                            sql = "INSERT INTO  item_presentacion(id, id_item, descripcion, cajon, color, unidad_medida, tamaño, precio_compra, precio_venta, imagen, stock, estado) "
                                + "VALUES(@id, @id_item, @descripcion, @cajon, @color, @unidad_medida, @tamaño, @precio_compra, @precio_venta, @imagen, @stock, @estado)";

                            using (var cmd3 = new MySqlCommand(sql, con))
                            {
                                cmd3.Parameters.Add("@id", MySqlDbType.VarChar, 50);
                                cmd3.Parameters.Add("@id_item", MySqlDbType.VarChar, 50);
                                cmd3.Parameters.Add("@descripcion", MySqlDbType.VarChar, 50);
                                cmd3.Parameters.Add("@cajon", MySqlDbType.Bit);
                                cmd3.Parameters.Add("@color", MySqlDbType.VarChar, 50);
                                cmd3.Parameters.Add("@unidad_medida", MySqlDbType.VarChar, 50);
                                cmd3.Parameters.Add("@tamaño", MySqlDbType.VarChar, 50);
                                cmd3.Parameters.Add("@precio_compra", MySqlDbType.Decimal);
                                cmd3.Parameters.Add("@precio_venta", MySqlDbType.Decimal);
                                cmd3.Parameters.Add("@imagen", MySqlDbType.LongText);
                                cmd3.Parameters.Add("@stock", MySqlDbType.Int32);
                                cmd3.Parameters.Add("@estado", MySqlDbType.Bit);

                                foreach (var presentacion in presentaciones)
                                {
                                    cmd3.Parameters["@id"].Value = presentacion.Id;
                                    cmd3.Parameters["@id_item"].Value = _item.Id;
                                    cmd3.Parameters["@descripcion"].Value = presentacion.Descripcion;
                                    cmd3.Parameters["@cajon"].Value = presentacion.Cajon;
                                    cmd3.Parameters["@color"].Value = presentacion.Color;
                                    cmd3.Parameters["@unidad_medida"].Value = presentacion.UnidadMedida;
                                    cmd3.Parameters["@tamaño"].Value = presentacion.Tamaño;
                                    cmd3.Parameters["@precio_compra"].Value = presentacion.PrecioCompra;
                                    cmd3.Parameters["@precio_venta"].Value = presentacion.PrecioVenta;
                                    cmd3.Parameters["@imagen"].Value = presentacion.Imagen;
                                    cmd3.Parameters["@stock"].Value = presentacion.Stock;
                                    cmd3.Parameters["@estado"].Value = presentacion.Estado;
                                    await cmd3.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }

                    resultado.estado = true;
                    resultado.mensaje = "Se completaron los procesoso";
                }

            }
            catch (Exception ex)
            {
                resultado.estado = false;
                resultado.mensaje = ex.Message.ToString();
            }
            return resultado;
        }

        public async Task<List<ProductoCategoriaResultado>> listaProductoCategoriaResultadoByEstado(bool estado)
        {
            var resultado = new List<ProductoCategoriaResultado>();

            using(var con = new MySqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT item.id, item.id_categoria, item.codigo, item.rango_nivel, item.cajon, "
                    + "item.descripcion, item.cajon_detalle, item.cajon_acabado, item.estado, categoria.nombre as 'categoria', "
                    + "categoria.descripcion as  'categoria_descripcion', categoria.estado as 'categoria_estado' "
                    + "FROM item INNER JOIN categoria ON item.id_categoria = categoria.id "
                    + "WHERE item.estado=@estado ORDER BY item.descripcion DESC ";

                using(var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@estado", estado);
                    
                    using(var drd = await cmd.ExecuteReaderAsync())
                    {
                        while(await drd.ReadAsync())
                        {
                            var oProductoCategoria = new ProductoCategoriaResultado();

                            var oItem = new Item
                            {
                                Id = Convert.ToString(drd["id"]),
                                IdCategoria = Convert.ToString(drd["id_categoria"]),
                                Codigo = Convert.ToString(drd["codigo"]),
                                RangoNivel = Convert.ToInt32(drd["rango_nivel"]),
                                Cajon = Convert.ToBoolean(drd["cajon"]),
                                Descripcion = Convert.ToString(drd["descripcion"]),
                                CajonDetalle = Convert.ToString(drd["cajon_detalle"]),
                                CajonAcabado = Convert.ToString(drd["cajon_acabado"]),
                                Estado = Convert.ToBoolean(drd["estado"])
                            };

                            oProductoCategoria.item = oItem;

                            var oCategoria = new Categoria
                            {
                                Id = Convert.ToString(drd["id_categoria"]),
                                Nombre = Convert.ToString(drd["categoria"]),
                                Descripcion = Convert.ToString(drd["categoria_descripcion"]),
                                Estado = Convert.ToBoolean(drd["categoria_estado"])
                            };

                            oProductoCategoria.categoria = oCategoria;

                            resultado.Add(oProductoCategoria);
                        }
                    }
                }
            }
            return resultado;
        }

        public async Task<List<ItemImagenes>> imagenesItemByItemID(string id_item)
        {
            var resultado = new List<ItemImagenes>();

            using (var con = new MySqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT id, id_item, imagen, estado "
                    + "FROM item_imagenes WHERE id_item = @id_item ";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_item", id_item);

                    using (var drd = await cmd.ExecuteReaderAsync())
                    {
                        while (await drd.ReadAsync())
                        {
                            var oImagen = new ItemImagenes {
                                Id = Convert.ToString(drd["id"]),
                                IdItem = Convert.ToString(drd["id_item"]),
                                Imagen = Convert.ToString(drd["imagen"]),
                                Estado = Convert.ToBoolean(drd["estado"])
                            };

                            resultado.Add(oImagen);
                        }
                    }
                }
            }
            return resultado;
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
                    string sql = "UPDATE item SET estado=@estado WHERE id=@id ";

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

        public async Task<List<ItemDatos>> listaProductosByCategoria(bool estado, string id_categoria)
        {
            var resultado = new List<ItemDatos>();

            using(var con = new MySqlConnection(new Conexion().cn()))
            {
                await con.OpenAsync();

                string sql = "SELECT item.id, item.id_categoria, item.codigo, item.rango_nivel, item.cajon, "
                    + "item.descripcion, item.cajon_detalle, item.cajon_acabado, item.estado, "
                    + "item_presentacion.descripcion as 'descripcion_presentacion', item_presentacion.color, "
                    + "item_presentacion.unidad_medida, item_presentacion.tamaño, item_presentacion.precio_compra, "
                    + "item_presentacion.precio_venta, item_presentacion.stock, item_presentacion.id as 'id_presentacion' "
                    + "FROM item INNER JOIN item_presentacion ON item.id = item_presentacion.id_item "
                    + "WHERE item_presentacion.estado=@estado AND item.id_categoria=@id_categoria ORDER BY item.descripcion DESC ";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria);

                    using(var drd = await cmd.ExecuteReaderAsync())
                    {
                        while(await drd.ReadAsync())
                        {
                            var producto = new ItemDatos();

                            var oItem = new Item
                            {
                                Id = Convert.ToString(drd["id"]),
                                IdCategoria = Convert.ToString(drd["id_categoria"]),
                                Codigo = Convert.ToString(drd["codigo"]),
                                Cajon = Convert.ToBoolean(drd["cajon"]),
                                Descripcion = Convert.ToString(drd["descripcion"]),
                                CajonDetalle = Convert.ToString(drd["cajon_detalle"]),
                                CajonAcabado = Convert.ToString(drd["cajon_acabado"])
                            };

                            producto.item = oItem;

                            var oPresentacion = new ItemPresentacion
                            {
                                Id = Convert.ToString(drd["id_presentacion"]),
                                Descripcion = Convert.ToString(drd["descripcion_presentacion"]),
                                Color = Convert.ToString(drd["color"]),
                                UnidadMedida = Convert.ToString(drd["unidad_medida"]),
                                Tamaño = Convert.ToString(drd["tamaño"]),
                                PrecioCompra = Convert.ToDecimal(drd["precio_compra"]),
                                PrecioVenta = Convert.ToDecimal(drd["precio_venta"]),
                                Stock = Convert.ToInt32(drd["stock"])
                            };

                            producto.presentacion = oPresentacion;

                            resultado.Add(producto);
                        }
                    }
                }
            }

            return resultado;
        }

    }
}
