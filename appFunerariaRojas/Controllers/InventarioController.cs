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
        public async Task<IActionResult> Compras()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "E";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            DateTime fecha = DateTime.Now;
            ViewBag.indexMes = fecha.Month;

            ViewBag.Usuarios = await new UsuarioData().listaUsuarioByEstado(1);
            ViewBag.Proveedores = await new ProveedorData().listaProveedorByEstado(true);

            return View();
        }

        public async Task<IActionResult> agregarCompra()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "E";

            ViewBag.NumeroCompra = await new ComprasData().numeroCompraGenerado();

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            ViewBag.Categorias = await new CategoriaData().listaCategoriasByEstado(true);

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

        public async Task<IActionResult> Stock()
        {
            ViewBag.btnMenu = 4;
            ViewBag.btnSubMenu = "H";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            ViewBag.Categorias = await new CategoriaData().listaCategoriasByEstado(true);

            return View();
        }

        [HttpPost]
        public async Task<List<Proveedor>> listaProveedorByEstado()
        {
            return await new ProveedorData().listaProveedorByEstado(true);
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
        public async Task<List<CompraDatos>> listaComprasByFecha(int mes, string id_proveedor, string id_usuario)
        {
            DateTime fechaHoy = DateTime.Now;

            DateTime fecha_inicio = Convert.ToDateTime("01/" + mes + "/" + fechaHoy.Year);
            DateTime fecha_final = Convert.ToDateTime(fecha_inicio).AddMonths(1).AddDays(-1);

            return await new ComprasData().listaComprasByFecha(fecha_inicio, fecha_final, id_proveedor, id_usuario);
        }

        [HttpPost]
        public async Task<List<ItemDatos>> listaProductosByCategoria(string id_categoria)
        {
            return await new ProductoData().listaProductosByCategoria(true, id_categoria);
        }

        [HttpPost]
        public async Task<List<ProductoItemStock>> ListaProductosItemStockByCategoria(string id_categoria)
        {
            return await new ProductoData().ListaProductosItemStockByCategoria(id_categoria);
        }

        [HttpPost]
        public async Task<ResultadoModel> insertarcompraDetalle(CompraDetalleParameter param)
        {
            var result = new ResultadoModel();

            var oUsuario = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            DateTime fecha = DateTime.Now;

            param.compra.Id = new Util.FuncionesAux().generarId();
            param.compra.IdUsuario = oUsuario.usuario.Id;
            param.compra.Estado = true;
            param.compra.Fecha = fecha;
            param.compra.Hora = fecha.TimeOfDay;

            foreach(var obj in param.detalle)
            {
                obj.Estado = true;
            }

            result = await new ComprasData().insertarDetalleCompras(param.compra, param.detalle);

            return result;
        }


    }
}
