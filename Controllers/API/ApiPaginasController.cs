using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms_stock.Models.Dominio.Entidades;
using cms_stock.Models.Infraestrutura.Database;
using cms_stock.Models.Infraestrutura.Autenticacao;

namespace cms_stock.Controllers.API
{
    [Logado]

    public class ApiPaginasController : ControllerBase
    {
        private readonly ContextoCms _context;

        public ApiPaginasController(ContextoCms context)
        {
            _context = context;
        }

        // GET: Paginas
        [HttpGet]
        [Route("/api/paginas.json")]
        public async Task<IActionResult> Index()
        {
            return StatusCode(200, await _context.Paginas.ToListAsync());
        }

        // CREATE: Paginas
        [HttpPost]
        [Route("/api/paginas.json")]
        public async Task<IActionResult> Create([FromBody] Pagina pagina)
        {
            _context.Add(pagina);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        // UPDATE: Paginas
        [HttpPut]
        [Route("/api/paginas/{id}.json")]
        public async Task<IActionResult> Update(int id, [FromBody] Pagina pagina)
        {
            pagina.Id = id;
            _context.Update(pagina);
            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        // GET: Paginas/Delete/5
        [HttpDelete]
        [Route("/api/paginas/{id}.json")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return StatusCode(400, new { Mensagem = "O Id é obrigatório"});
            }

            var pagina = await _context.Paginas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pagina == null)
            {
                return StatusCode(404, new { Mensagem = "A página não foi encontrada" });
            }

            await _context.Paginas.FindAsync(id);
            _context.Paginas.Remove(pagina);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }
    }
}
