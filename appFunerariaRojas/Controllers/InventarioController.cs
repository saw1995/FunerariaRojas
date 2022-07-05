using appFunerariaRojas.Data;
using appFunerariaRojas.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models;

namespace appFunerariaRojas.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Compras()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "E";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            return View();
        }
        public async Task<IActionResult> Proveedor()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "F";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            ViewBag.Proveedores = await new ProveedorData().listaProveedorByEstado(true);

            return View();
        }
        public IActionResult Kardex()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "G";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            return View();
        }

        public IActionResult Stock()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "H";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));
            return View();
        }

        [HttpPost]
        public async Task<ResultadoModel> insertarProveedor(Proveedor proveedor)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;
            proveedor.Id = new Util.FuncionesAux().generarId();
            proveedor.Estado = true;

            resultado = await new ProveedorData().insertarProveedor(proveedor);

            return resultado;
        }

        [HttpPost]
        public async Task<ResultadoModel> actualizarEstadoByIdProveedor(string id)
        {
            return await new ProveedorData().actualizarEstadoById(false, id);
        }

        [HttpPost]
        public async Task<List<CompraDatos>> listaComprasByFecha(DateTime fecha_inicio, DateTime fecha_final)
        {
            return await new ComprasData().listaComprasByFecha(fecha_inicio, fecha_final);
        }

    }
}
