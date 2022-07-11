using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;
using appFunerariaRojas.Models.Entity;

namespace appFunerariaRojas.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ReporteController : Controller
    {
        readonly IGeneratePdf _generatePdf;
        public ReporteController(IGeneratePdf generatePdf)
        {
            _generatePdf = generatePdf;
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> get()
        {
            var usuario = new Usuario
            {
                Id = "123456789",
                IdRol = 1,
                Nombre = "Nombre"
            };

            return await _generatePdf.GetPdf("Views/Reportes/usuario.cshtml", usuario);
        }
    }
}
