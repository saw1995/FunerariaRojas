using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Data;
using appFunerariaRojas.Models;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models.Validate;
using Microsoft.AspNetCore.Http;
using appFunerariaRojas.Util;

namespace appFunerariaRojas.Controllers
{
    public class UsuarioController : Controller
    {
        //[ValidateSession]
        public IActionResult Index()
        {
            ViewBag.btnMenu = 2;
            ViewBag.btnSubMenu = "";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            return View();
        }

        [HttpPost]
        public async Task<ResultadoModel> insertarUsuario(Usuario usuario)
        {
            usuario.Id = new FuncionesAux().generarId();
            usuario.Foto = "";
            usuario.Estado = true;
            usuario.Contrasenna = usuario.Telefono;
            return await new UsuarioData().insertarUsuario(usuario);
        }

        [HttpPost]
        public async Task<List<UsuarioRolModel>> listaUsuarioByEstado(int estado)
        {
            return await new UsuarioData().listaUsuarioByEstado(estado);
        }

        [HttpPost]
        public async Task<ResultadoModel> actualizarEstadoById(bool estado, string id)
        {
            return await new UsuarioData().actualizarEstadoById(estado, id);
        }
    }
}

