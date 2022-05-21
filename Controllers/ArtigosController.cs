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
using X.PagedList;

namespace cms_stock.Controllers
{

    public class ArtigosController : Controller
    {
        private readonly ContextoCms _context;

        public ArtigosController(ContextoCms context)
        {
            _context = context;
        }

        [Logado]
        // GET: Artigos
        public async Task<IActionResult> Index(string artigoNome, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 15;

            if (!String.IsNullOrEmpty(artigoNome))
            {
                var artigos = _context.Artigos.Where(a => a.Nome.Contains(artigoNome)).ToPagedList(pageNumber, pageSize);
                return View(artigos);
            }
            else
            {
                var artigos = _context.Artigos.ToPagedList(pageNumber, pageSize);
                return View(artigos);
            }  
        }

        public async Task<IActionResult> IndexUser(string artigoNome, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 15;

            if (!String.IsNullOrEmpty(artigoNome))
            {
                var artigos = _context.Artigos.Where(a => a.Nome.Contains(artigoNome)).ToPagedList(pageNumber, pageSize);
                return View(artigos);
            }
            else
            {
                var artigos = _context.Artigos.ToPagedList(pageNumber, pageSize);
                return View(artigos);
            }
        }

        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var customers = (from artigo in this._context.Artigos
                             where artigo.Nome.StartsWith(prefix)
                             select new
                             {
                                 label = artigo.Nome,
                                 val = artigo.Id
                             }).ToList();

            return Json(customers);
        }

        [Logado]
        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        [Logado]
        // GET: Artigos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Referencia,Nome,PCusto,Unidade,Inativo,Observacao")] Artigo artigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artigo);
        }

        [Logado]
        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos.FindAsync(id);
            if (artigo == null)
            {
                return NotFound();
            }
            return View(artigo);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Referencia,Nome,PCusto,Unidade,Inativo,Observacao")] Artigo artigo)
        {
            if (id != artigo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigoExists(artigo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artigo);
        }

        [Logado]
        // GET: Artigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artigo = await _context.Artigos.FindAsync(id);
            _context.Artigos.Remove(artigo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigoExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
