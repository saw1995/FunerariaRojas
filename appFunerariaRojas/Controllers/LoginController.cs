using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using appFunerariaRojas.Models;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Data;
using Newtonsoft.Json;

namespace appFunerariaRojas.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ResultadoModel> usuarioByCIByPass(string ci, string pass)
        {
            var resultado = new ResultadoModel();
            resultado.mensaje = "usuario no encontrado";
            resultado.estado = false;

            if (ci != null || pass != null)
            {
                var oLogin = await new UsuarioData().usuarioByCiPass(ci, pass);
                if (oLogin.usuario != null && oLogin.rol != null)
                {
                    //Guardamos Session
                    HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(oLogin));
                    resultado.estado = true;
                    resultado.mensaje = "Registro encontrado con Exito:" + HttpContext.Session.GetString("usuario");
                }
            }
            return resultado;
        }

    }
}
