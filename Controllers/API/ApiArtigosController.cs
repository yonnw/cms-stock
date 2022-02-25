using cms_stock.Models.Infraestrutura.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cms_stock.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiArtigosController : ControllerBase
    {
        private readonly ContextoCms _context;

        public ApiArtigosController(ContextoCms context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var artigoNome = _context.Artigos.Where(a => a.Nome.Contains(term))
                                                 .Select(a => a.Nome).ToList();
                return Ok(artigoNome);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
