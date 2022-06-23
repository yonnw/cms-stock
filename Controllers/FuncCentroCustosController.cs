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
using System.Text.RegularExpressions;
using X.PagedList;
using cms_stock.Models.Dominio.Servico;

namespace cms_stock.Controllers
{
    public class FuncCentroCustosController : Controller
    {
        private readonly ContextoCms _context;

        public FuncCentroCustosController(ContextoCms context)
        {
            _context = context;
        }

        [Logado]
        // GET: FuncCentroCustos
        public async Task<IActionResult> Index(int? CCustoid, string searchString, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 15;

            if (CCustoid > 0)
            {
                var contextoCms1 = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario).Where(i => i.CentroCustoId == CCustoid).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(contextoCms1);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var funcCentroCustos = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario).Where(f => f.Funcionario.Nome.Contains(searchString)).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(funcCentroCustos);
            }
            else
            {
                var funcCentroCustos = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(funcCentroCustos);
            }
        }

        public async Task<IActionResult> IndexUser(int? CCustoid, string searchString, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 15;

            if (CCustoid > 0)
            {
                var contextoCms1 = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario).Where(i => i.CentroCustoId == CCustoid).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(contextoCms1);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var funcCentroCustos = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario).Where(f => f.Funcionario.Nome.Contains(searchString)).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(funcCentroCustos);
            }
            else
            {
                var funcCentroCustos = _context.FuncCentroCustos.Include(f => f.CentroCusto).Include(f => f.Funcionario).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(funcCentroCustos);
            }
        }

        [Logado]
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

            ViewData["Funcionarios"] = _context.Funcionarios.ToList();            

            return View();
        }

        // POST: FuncCentroCustos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CentroCustoId,FuncionarioId,Data,Qtd,Valor,DataFim,Observacao")] FuncCentroCusto funcCentroCusto)
        {
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Id"))
                {
                    if(funcCentroCusto.Id != 0)
                    {
                        funcCentroCusto.Id = 0;
                    }

                    var apenasDigitos = new Regex(@"[^\d]");
                    var b = apenasDigitos.Replace(key, "");
                    funcCentroCusto.FuncionarioId = Convert.ToInt32(b);

                    funcCentroCusto.CalcData = funcCentroCusto.DataFim - funcCentroCusto.Data;

                    if (ModelState.IsValid)
                    {
                        if (funcCentroCusto.CentroCustoId > 0 && funcCentroCusto.FuncionarioId > 0)
                        {
                            Calcular.CalcularFunc(funcCentroCusto);
                        }
                        _context.Add(funcCentroCusto);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", funcCentroCusto.CentroCustoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionarios, "Id", "Nome", funcCentroCusto.FuncionarioId);
            return RedirectToAction("Index", "CentroCustos");
        }

        [Logado]
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

        public async Task<IActionResult> EditUser(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,CentroCustoId,FuncionarioId,Data,Qtd,ValorUnit,VVendaUnit,Valor,DataFim,Observacao")] FuncCentroCusto funcCentroCusto)
        {
            if (id != funcCentroCusto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (funcCentroCusto.CentroCustoId > 0 && funcCentroCusto.FuncionarioId > 0 && funcCentroCusto.Valor == 0)
                    {
                        Calcular.CalcularFunc(funcCentroCusto);
                    }
                    funcCentroCusto.VVenda = funcCentroCusto.VVendaUnit * funcCentroCusto.Qtd;
                    funcCentroCusto.CalcData = funcCentroCusto.DataFim - funcCentroCusto.Data;
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
