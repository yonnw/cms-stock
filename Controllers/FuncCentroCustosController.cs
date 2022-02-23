using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms_stock.Models.Dominio.Entidades;
using cms_stock.Models.Infraestrutura.Database;

namespace cms_stock.Controllers
{
    public class FuncCentroCustosController : Controller
    {
        private readonly ContextoCms _context;

        public FuncCentroCustosController(ContextoCms context)
        {
            _context = context;
        }

        // GET: FuncCentroCustos
        public async Task<IActionResult> Index()
        {
            var contextoCms = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario);
            return View(await contextoCms.ToListAsync());
        }

        // GET: FuncCentroCustos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcCentroCusto = await _context.FuncCentroCustos
                .Include(f => f.CentroCusto)
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcCentroCusto == null)
            {
                return NotFound();
            }

            return View(funcCentroCusto);
        }

        // GET: FuncCentroCustos/Create
        public IActionResult Create(int CCustoId, string NCCusto)
        {
            // Recebe o id, nome do centro de custo vindo do btn index-centrocustos para criar o artigo
            ViewBag.ccusto = CCustoId;
            ViewBag.nomeccusto = NCCusto;

            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome");
            return View();
        }

        // POST: FuncCentroCustos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CentroCustoId,FuncionarioId,Data,Qtd")] FuncCentroCusto funcCentroCusto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcCentroCusto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", funcCentroCusto.CentroCustoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", funcCentroCusto.FuncionarioId);
            return View(funcCentroCusto);
        }

        // GET: FuncCentroCustos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcCentroCusto = await _context.FuncCentroCustos.FindAsync(id);
            if (funcCentroCusto == null)
            {
                return NotFound();
            }
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", funcCentroCusto.CentroCustoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", funcCentroCusto.FuncionarioId);
            return View(funcCentroCusto);
        }

        // POST: FuncCentroCustos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CentroCustoId,FuncionarioId,Data,Qtd,Valor")] FuncCentroCusto funcCentroCusto)
        {
            if (id != funcCentroCusto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcCentroCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncCentroCustoExists(funcCentroCusto.Id))
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
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", funcCentroCusto.CentroCustoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", funcCentroCusto.FuncionarioId);
            return View(funcCentroCusto);
        }

        // GET: FuncCentroCustos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcCentroCusto = await _context.FuncCentroCustos
                .Include(f => f.CentroCusto)
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcCentroCusto == null)
            {
                return NotFound();
            }

            return View(funcCentroCusto);
        }

        // POST: FuncCentroCustos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcCentroCusto = await _context.FuncCentroCustos.FindAsync(id);
            _context.FuncCentroCustos.Remove(funcCentroCusto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncCentroCustoExists(int id)
        {
            return _context.FuncCentroCustos.Any(e => e.Id == id);
        }
    }
}
