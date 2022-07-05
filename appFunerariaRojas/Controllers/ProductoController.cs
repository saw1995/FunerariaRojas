using appFunerariaRojas.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appFunerariaRojas.Data;
using appFunerariaRojas.Models;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Listado","Producto");
        }
        
        public IActionResult Categoria()
        {
            ViewBag.btnMenu = 3;
            ViewBag.btnSubMenu = "A";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            return View();
        }

        public async Task<ActionResult> Listado()
        {
            ViewBag.btnMenu = 3;
            ViewBag.btnSubMenu = "B";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            var _productos = await new ProductoData().listaProductoCategoriaResultadoByEstado(true);
            foreach(var producto in _productos)
            {
                producto.imagenes = await new ProductoData().imagenesItemByItemID(producto.item.Id);
            }

            ViewBag.Productos = _productos;

            return View();
        }
        public async Task<IActionResult> Agregar()
        {
            ViewBag.btnMenu = 3;
            ViewBag.btnSubMenu = "B";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            ViewBag.Categorias = await new CategoriaData().listaCategoriasByEstado(true);

            return View();
        }

        public async Task<IActionResult> ServiciosAdicionales()
        {
            ViewBag.btnMenu = 3;
            ViewBag.btnSubMenu = "C";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));


            var oServiciosAdicionales = await new ServicioAdicionalData().listaServiciosAdicionalesByEstado(true);
            ViewBag.ServiciosAdicionales = oServiciosAdicionales;

            return View();
        }

        public IActionResult PlanPaquetes()
        {
            ViewBag.btnMenu = 3;
            ViewBag.btnSubMenu = "D";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));


            return View();
        }

        public IActionResult PlanPaqueteAgregar()
        {
            ViewBag.btnMenu = 3;
            ViewBag.btnSubMenu = "D";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));


            return View();
        }

        //Fucniones POST!

        [HttpPost]
        public async Task<ResultadoModel> insertarCategoría(Categoria param)
        {
            param.Id = new FuncionesAux().generarId();
            param.Estado = true;

            return await new CategoriaData().insertarCategoría(param);
        }

        [HttpPost]
        public async Task<ResultadoModel> insertarServicioAdicional(ServicioAdicional param)
        {
            param.Id = new FuncionesAux().generarId();
            param.Estado = true;

            return await new ServicioAdicionalData().insertarServicioAdicional(param);
        }

        [HttpPost]
        public async Task<ResultadoModel> insertarProductoDetallesImagenes(productoAgregarParameter parameters)
        {
            var resultado = new ResultadoModel();
            resultado.estado = false;

            if (parameters == null)
            {
                resultado.mensaje = "falta de parametros";
            }
            else if (parameters.item == null)
            {
                resultado.mensaje = "falta de parametros CajonModelo";
            }
            else if (parameters.imagenes == null)
            {
                resultado.mensaje = "falta de parametros Cajon Imagenes";
            }
            else if (parameters.presentaciones == null)
            {
                resultado.mensaje = "falta de parametros Cajon_presentaciones";
            }
            else
            {
                parameters.item.Id = new FuncionesAux().generarId();
                parameters.item.Estado = true;

                for (int i = 0; i <= parameters.imagenes.Count - 1; i++)
                {
                    parameters.imagenes[i].Id = new FuncionesAux().generarId();
                    parameters.imagenes[i].Estado = true;
                }

                for (int i = 0; i <= parameters.presentaciones.Count - 1; i++)
                {
                    parameters.presentaciones[i].Id = new FuncionesAux().generarId();
                    parameters.presentaciones[i].Estado = true;
                }

                var oConsultaInsert = await new ProductoData()
                    .insertarProductoDetallesImagenes(parameters.item, parameters.imagenes, parameters.presentaciones);

                if (oConsultaInsert.estado)
                {
                    resultado.estado = true;
                    resultado.mensaje = "Se insertaon con exito los registros a la BD";
                }
                else
                {
                    resultado.estado = false;
                    resultado.mensaje = oConsultaInsert.mensaje;
                }
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ResultadoModel> actualizarEstadoByIdCategoria(bool estado, string id)
        {
            return await new CategoriaData().actualizarEstadoById(estado, id);
        }

        [HttpPost]
        public async Task<ResultadoModel> actualizarEstadoByIdProducto(string id)
        {
            return await new ProductoData().actualizarEstadoById(false, id);
        }

        [HttpPost]
        public async Task<ResultadoModel> actualizarEstadoByIdServicioAdicional(bool estado, string id)
        {
            return await new ServicioAdicionalData().actualizarEstadoById(estado, id);
        }

        [HttpPost]
        public async Task<List<Categoria>> listaCategoriasByEstado()
        {
            return await new CategoriaData().listaCategoriasByEstado(true);
        }

        [HttpPost]
        public async Task<List<ServicioAdicional>> listaServiciosAdicionalesByEstado()
        {
            return await new ServicioAdicionalData().listaServiciosAdicionalesByEstado(true);
        }

        
    }
}
