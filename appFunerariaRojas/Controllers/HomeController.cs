using appFunerariaRojas.Models;
using appFunerariaRojas.Models.Entity;
using appFunerariaRojas.Models.Validate;
using appFunerariaRojas.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace appFunerariaRojas.Controllers
{
    public class HomeController : Controller
    {

        //[ValidateSession]
        public IActionResult Index()
        {
            ViewBag.btnMenu = 1;
            ViewBag.btnSubMenu = "";

            ViewBag.usuarioData = new UsuarioRolDesarialize()
                .usuarioRolModeloSession(Convert.ToString(HttpContext.Session.GetString("usuario")));

            return View();
        }

    }
}
