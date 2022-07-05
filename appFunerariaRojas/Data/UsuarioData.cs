﻿using appFunerariaRojas.Models.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models;

namespace appFunerariaRojas.Data
{
    public class UsuarioData
    {
        //insertar datos jajaj
        public async Task<ResultadoModel> insertarUsuario(Usuario oUSuario)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            resultado.mensaje = "No se agrego el registro intente bla bla";

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "INSERT INTO usuario(id, id_rol, ci, ci_exp,  ciudad, nombre, appaterno, apmaterno, telefono, direccion, contrasenna, foto, estado) "
                            + "VALUES(@id, @id_rol, @ci, @ci_exp, @ciudad, @nombre, @appaterno, @apmaterno, @telefono, @direccion, @contrasenna, @foto, @estado)";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", oUSuario.Id);
                        cmd.Parameters.AddWithValue("@id_rol", oUSuario.IdRol);
                        cmd.Parameters.AddWithValue("@ci", oUSuario.Ci);
                        cmd.Parameters.AddWithValue("@ci_exp", oUSuario.CiExp);                        
                        cmd.Parameters.AddWithValue("@ciudad", oUSuario.Ciudad);
                        cmd.Parameters.AddWithValue("@nombre", oUSuario.Nombre);
                        cmd.Parameters.AddWithValue("@appaterno", oUSuario.Appaterno);
                        cmd.Parameters.AddWithValue("@apmaterno", oUSuario.Apmaterno);
                        cmd.Parameters.AddWithValue("@telefono", oUSuario.Telefono);
                        cmd.Parameters.AddWithValue("@direccion", oUSuario.Direccion);
                        cmd.Parameters.AddWithValue("@contrasenna", oUSuario.Contrasenna);
                        cmd.Parameters.AddWithValue("@foto", oUSuario.Foto);
                        cmd.Parameters.AddWithValue("@estado", oUSuario.Estado);

                        var i = await cmd.ExecuteNonQueryAsync();

                        if(i == 1)
                        {
                            resultado.estado = true;
                            resultado.mensaje = "Se inserto el usuario con exito";
                        }
                    }
                }

                return resultado;
            }
            catch(Exception ex)
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
                    string sql = "UPDATE usuario SET estado=@estado WHERE id=@id ";

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

        public async Task<List<UsuarioRolModel>> listaUsuarioByEstado(int estado)
        {
            var resultado = new List<UsuarioRolModel>();

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "SELECT * FROM usuario WHERE estado=@estado ORDER BY nombre ASC";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado);

                        using (var drd = await cmd.ExecuteReaderAsync())
                        {
                            while (await drd.ReadAsync())
                            {
                                var oUsuarioRol = new UsuarioRolModel();

                                var oUsuario = new Usuario
                                {
                                    Id = Convert.ToString(drd["id"]),
                                    Ci = Convert.ToString(drd["ci"]),
                                    CiExp = Convert.ToString(drd["ci_exp"]),
                                    IdRol = Convert.ToInt32(drd["id_rol"]),
                                    Ciudad = Convert.ToString(drd["ciudad"]),
                                    Nombre = Convert.ToString(drd["nombre"]),
                                    Appaterno = Convert.ToString(drd["appaterno"]),
                                    Apmaterno = Convert.ToString(drd["apmaterno"]),
                                    Telefono = Convert.ToString(drd["telefono"]),
                                    Direccion = Convert.ToString(drd["direccion"]),
                                    Foto = Convert.ToString(drd["foto"]),
                                    Estado = Convert.ToBoolean(drd["estado"])
                                };

                                string imgBase64 = "data:image/png;base64,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";
                                oUsuario.Foto = oUsuario.Foto == "" ? imgBase64 : oUsuario.Foto;

                                oUsuarioRol.usuario = oUsuario;

                                var oRol = new Rol();
                                oRol.id = oUsuario.IdRol;
                                oRol.nombre = oUsuario.IdRol == 1 ? "Administrador" : "Vendedor";

                                oUsuarioRol.rol = oRol;

                                resultado.Add(oUsuarioRol);
                            }
                        }
                    }
                }

                return resultado;
            }
            catch
            {
                return new List<UsuarioRolModel>();
            }
        }

        public async Task<UsuarioRolModel> usuarioByCiPass(string ci, string password)
        {
            var resultado = new UsuarioRolModel();

            try
            {
                using (var con = new MySqlConnection(new Conexion().cn()))
                {
                    await con.OpenAsync();
                    string sql = "SELECT * FROM usuario WHERE ci=@ci AND contrasenna = @pass ";

                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ci", ci);
                        cmd.Parameters.AddWithValue("@pass", password);

                        using (var drd = await cmd.ExecuteReaderAsync())
                        {
                            while (await drd.ReadAsync())
                            {
                                var oUsuario = new Usuario
                                {
                                    Id = Convert.ToString(drd["id"]),
                                    Ci = Convert.ToString(drd["ci"]),
                                    CiExp = Convert.ToString(drd["ci_exp"]),
                                    IdRol = Convert.ToInt32(drd["id_rol"]),
                                    Ciudad = Convert.ToString(drd["ciudad"]),
                                    Nombre = Convert.ToString(drd["nombre"]),
                                    Appaterno = Convert.ToString(drd["appaterno"]),
                                    Apmaterno = Convert.ToString(drd["apmaterno"]),
                                    Telefono = Convert.ToString(drd["telefono"]),
                                    Direccion = Convert.ToString(drd["direccion"]),
                                    Foto = Convert.ToString(drd["foto"]),
                                    Estado = Convert.ToBoolean(drd["estado"])
                                };

                                resultado.usuario = oUsuario;

                                var oRol = new Rol();
                                oRol.id = oUsuario.IdRol;
                                oRol.nombre = oUsuario.IdRol == 1 ? "Administrador" : "Vendedor";

                                resultado.rol = oRol;
                            }
                        }
                    }
                }

                return resultado;
            }
            catch
            {
                return new UsuarioRolModel(); ;
            }
        }


    }
}
