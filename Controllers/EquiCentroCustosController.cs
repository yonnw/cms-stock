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
using System.Text.RegularExpressions;
using cms_stock.Models.Dominio.Servico;
using cms_stock.Models.Dominio.Views;

namespace cms_stock.Controllers
{
    public class EquiCentroCustosController : Controller
    {
        private readonly ContextoCms _context;

        public EquiCentroCustosController(ContextoCms context)
        {
            _context = context;
        }

        [Logado]
        // GET: EquiCentroCustos
        public async Task<IActionResult> Index(int? CCustoid, string searchString, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 15;

            if (CCustoid > 0)
            {
                var equiCentroCustos = _context.EquiCentroCustos.Include(e => e.CentroCusto).Include(e => e.Equipamento).Where(i => i.CentroCustoId == CCustoid).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(equiCentroCustos);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                var equiCentroCustos = _context.EquiCentroCustos.Include(e => e.CentroCusto).Include(e => e.Equipamento).Where(a => a.Equipamento.Nome.Contains(searchString)).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(equiCentroCustos);
            }
            else
            {
                var equiCentroCustos = _context.EquiCentroCustos.Include(e => e.CentroCusto).Include(e => e.Equipamento).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(equiCentroCustos);
            }
        }

        public async Task<IActionResult> IndexUser(int? CCustoid, string searchString, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 15;

            if (CCustoid > 0)
            {
                var equiCentroCustos = _context.EquiCentroCustos.Include(e => e.CentroCusto).Include(e => e.Equipamento).Where(i => i.CentroCustoId == CCustoid).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(equiCentroCustos);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                var equiCentroCustos = _context.EquiCentroCustos.Include(e => e.CentroCusto).Include(e => e.Equipamento).Where(a => a.Equipamento.Nome.Contains(searchString)).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(equiCentroCustos);
            }
            else
            {
                var equiCentroCustos = _context.EquiCentroCustos.Include(e => e.CentroCusto).Include(e => e.Equipamento).OrderByDescending(d => d.Data).ToPagedList(pageNumber, pageSize);
                return View(equiCentroCustos);
            }
        }

        [Logado]
        // GET: EquiCentroCustos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiCentroCusto = await _context.EquiCentroCustos
                .Include(e => e.CentroCusto)
                .Include(e => e.Equipamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equiCentroCusto == null)
            {
                return NotFound();
            }

            return View(equiCentroCusto);
        }

        // GET: EquiCentroCustos/Create
        public IActionResult Create(int CCustoId, string NCCusto)
        {
            // Recebe o id, nome do centro de custo vindo do btn index-centrocustos para criar o artigo
            ViewBag.ccusto = CCustoId;
            ViewBag.nomeccusto = NCCusto;

            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome");
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "Id", "Nome");

            ViewData["Equipamentos"] = _context.Equipamentos.ToList();

            return View();
        }

        // POST: EquiCentroCustos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CentroCustoId,EquipamentoId,Qtd,Valor,Data")] EquiCentroCusto equiCentroCusto)
        {
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Id"))
                {
                    if (equiCentroCusto.Id != 0)
                    {
                        equiCentroCusto.Id = 0;
                    }

                    var apenasDigitos = new Regex(@"[^\d]");
                    var b = apenasDigitos.Replace(key, "");
                    equiCentroCusto.EquipamentoId = Convert.ToInt32(b);

                    if (ModelState.IsValid)
                    {
                        if (equiCentroCusto.CentroCustoId > 0 && equiCentroCusto.EquipamentoId > 0)
                        {
                            Calcular.CalcularEqui(equiCentroCusto);
                        }
                        _context.Add(equiCentroCusto);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", equiCentroCusto.CentroCustoId);
            ViewData["EquipamentosId"] = new SelectList(_context.Equipamentos, "Id", "Nome", equiCentroCusto.EquipamentoId);
            return RedirectToAction("Index", "CentroCustos");
        }

        [Logado]
        // GET: EquiCentroCustos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiCentroCusto = await _context.EquiCentroCustos.FindAsync(id);
            if (equiCentroCusto == null)
            {
                return NotFound();
            }
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", equiCentroCusto.CentroCustoId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "Id", "Nome", equiCentroCusto.EquipamentoId);
            return View(equiCentroCusto);
        }

        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiCentroCusto = await _context.EquiCentroCustos.FindAsync(id);
            if (equiCentroCusto == null)
            {
                return NotFound();
            }
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", equiCentroCusto.CentroCustoId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "Id", "Nome", equiCentroCusto.EquipamentoId);
            return View(equiCentroCusto);
        }

        // POST: EquiCentroCustos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CentroCustoId,EquipamentoId,Qtd,ValorUnit,Data,VVendaUnit")] EquiCentroCusto equiCentroCusto)
        {
            if (id != equiCentroCusto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (equiCentroCusto.CentroCustoId > 0 && equiCentroCusto.EquipamentoId > 0 && equiCentroCusto.Valor == 0)
                    {
                        Calcular.CalcularEqui(equiCentroCusto); 
                    }
                    equiCentroCusto.VVenda = equiCentroCusto.VVendaUnit * equiCentroCusto.Qtd;
                    _context.Update(equiCentroCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquiCentroCustoExists(equiCentroCusto.Id))
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
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", equiCentroCusto.CentroCustoId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "Id", "Nome", equiCentroCusto.EquipamentoId);
            return View(equiCentroCusto);
        }

        [Logado]
        // GET: EquiCentroCustos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiCentroCusto = await _context.EquiCentroCustos
                .Include(e => e.CentroCusto)
                .Include(e => e.Equipamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equiCentroCusto == null)
            {
                return NotFound();
            }

            return View(equiCentroCusto);
        }

        // POST: EquiCentroCustos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equiCentroCusto = await _context.EquiCentroCustos.FindAsync(id);
            _context.EquiCentroCustos.Remove(equiCentroCusto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquiCentroCustoExists(int id)
        {
            return _context.EquiCentroCustos.Any(e => e.Id == id);
        }
    }
}
