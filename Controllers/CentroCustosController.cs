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
    public class CentroCustosController : Controller
    {
        private readonly ContextoCms _context;

        public CentroCustosController(ContextoCms context)
        {
            _context = context;
        }

        [Logado]
        // GET: CentroCustos
        public async Task<IActionResult> Index(string error, string searchString, int? page)
        {
            ViewBag.error = error;

            var pageNumber = page ?? 1;
            int pageSize = 10;

            if (!String.IsNullOrEmpty(searchString))
            {
                var centroCustos = _context.CentroCustos.Where(a => a.Nome.Contains(searchString)).ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
            else
            {
                var centroCustos = _context.CentroCustos.ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
        }

        // GET: CentroCustos
        public async Task<IActionResult> IndexUser(string error, string searchString, int? page)
        {
            ViewBag.error = error;

            var pageNumber = page ?? 1;
            int pageSize = 10;

            if (!String.IsNullOrEmpty(searchString))
            {
                var centroCustos = _context.CentroCustos.Where(a => a.Nome.Contains(searchString)).ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
            else
            {
                var centroCustos = _context.CentroCustos.ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
        }

        [Logado]
        // GET: CentroCustos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroCusto = await _context.CentroCustos
                .FirstOrDefaultAsync(m => m.Id == id);
            // Faz update a tabela CentroCustos.ValorTotal de todos os ArtCentroCustos, FuncCentroCustos, EquiCentroCustos
            var totalArtigos = _context.ArtCentroCustos.Where(i => i.CentroCusto.Id == centroCusto.Id).Sum(v => v.Valor);
            var totalFuncionarios = _context.FuncCentroCustos.Where(i => i.CentroCusto.Id == centroCusto.Id).Sum(v => v.Valor);
            var totalEquipamentos = _context.EquiCentroCustos.Where(i => i.CentroCusto.Id == centroCusto.Id).Sum(v => v.Valor);
            centroCusto.ValorTotal = totalArtigos + totalFuncionarios + totalEquipamentos;
            _context.SaveChanges();

            ViewBag.totalArtigos = totalArtigos;
            ViewBag.totalFuncionarios = totalFuncionarios;
            ViewBag.totalEquipamentos = totalEquipamentos;

            if (centroCusto == null)
            {
                return NotFound();
            }

            return View(centroCusto);
        }

        // GET: CentroCustos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CentroCustos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataInicial,DataFinal,Fechada,Observacao")] CentroCusto centroCusto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centroCusto);
                await _context.SaveChangesAsync();
                if (HttpContext.Request.Cookies.Keys.Contains("adm_cms_dv"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(IndexUser));
                }
            }
            return View(centroCusto);
        }

        // GET: CentroCustos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroCusto = await _context.CentroCustos.FindAsync(id);
            if (centroCusto == null)
            {
                return NotFound();
            }
            return View(centroCusto);
        }

        // POST: CentroCustos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataInicial,DataFinal,Fechada,Observacao")] CentroCusto centroCusto)
        {
            if (id != centroCusto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centroCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentroCustoExists(centroCusto.Id))
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
            return View(centroCusto);
        }

        // GET: CentroCustos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centroCusto = await _context.CentroCustos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (centroCusto == null)
            {
                return NotFound();
            }

            return View(centroCusto);
        }

        // POST: CentroCustos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centroCusto = await _context.CentroCustos.FindAsync(id);
            _context.CentroCustos.Remove(centroCusto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentroCustoExists(int id)
        {
            return _context.CentroCustos.Any(e => e.Id == id);
        }
    }
}
