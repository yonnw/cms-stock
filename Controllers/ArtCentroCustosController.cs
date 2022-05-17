using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cms_stock.Models.Dominio.Entidades;
using cms_stock.Models.Infraestrutura.Database;
using Microsoft.Data.SqlClient;
using cms_stock.Models.Infraestrutura.Autenticacao;

namespace cms_stock.Controllers
{

    public class ArtCentroCustosController : Controller
    {
        private readonly ContextoCms _context;

        public ArtCentroCustosController(ContextoCms context)
        {
            _context = context;
        }

        // GET: ArtCentroCustos
        public async Task<IActionResult> Index(int? CCustoid)
        {
            if (CCustoid > 0)
            {
                var contextoCms1 = _context.ArtCentroCustos.Include(a => a.Artigo).Include(a => a.CentroCusto).Where(i => i.CentroCustoId == CCustoid);
                return View(await contextoCms1.ToListAsync());
            }
            var contextoCms = _context.ArtCentroCustos.Include(a => a.Artigo).Include(a => a.CentroCusto);
            return View(await contextoCms.ToListAsync());
        }

        [Logado]
        // GET: ArtCentroCustos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artCentroCusto = await _context.ArtCentroCustos
                .Include(a => a.Artigo)
                .Include(a => a.CentroCusto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artCentroCusto == null)
            {
                return NotFound();
            }

            return View(artCentroCusto);
        }

        // GET: ArtCentroCustos/DetailsUser/5
        public async Task<IActionResult> DetailsUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artCentroCusto = await _context.ArtCentroCustos
                .Include(a => a.Artigo)
                .Include(a => a.CentroCusto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artCentroCusto == null)
            {
                return NotFound();
            }

            return View(artCentroCusto);
        }

        // GET: ArtCentroCustos/Create
        public IActionResult Create(int CCustoId, string NCCusto)
        {
            // Recebe o id, nome do centro de custo vindo do btn index-centrocustos para criar o artigo
            ViewBag.ccusto = CCustoId;
            ViewBag.nomeccusto = NCCusto;

            ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome");
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome");
            return View();
        }

        // POST: ArtCentroCustos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CentroCustoId,ArtigoId,Qtd,Nomeservico,Observacoes,Uniservico")] ArtCentroCusto artCentroCusto)
        {
            var a = Request.Form.ToList();
            var b = a[4].Value.ToString();

            if (b.Contains(','))
            {
                var preco = b.Replace(",", ".");
                artCentroCusto.Qtd = float.Parse(preco);
            }

            if (artCentroCusto.ArtigoId == 0 && artCentroCusto.Nomeservico != null)
            {
                //Artigo de Serviço
                artCentroCusto.ArtigoId = 2;
            }

            //Alterar o id do Artigo de Serviço
            if (artCentroCusto.CentroCustoId > 0 && artCentroCusto.ArtigoId > 0 && artCentroCusto.ArtigoId != 2)
            {
                var artigo = _context.Artigos.Where(i => i.Id == artCentroCusto.ArtigoId).ToList();
                artCentroCusto.Valor = artigo[0].PCusto * artCentroCusto.Qtd;
            }

            if (artCentroCusto.CentroCustoId >0 && artCentroCusto.ArtigoId >0 && artCentroCusto.Qtd > 0)
            {
                _context.Add(artCentroCusto);
                await _context.SaveChangesAsync();

                if (HttpContext.Request.Cookies.Keys.Contains("adm_cms_dv"))
                {
                    ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
                    ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
                    return RedirectToAction("Index", "CentroCustos");
                }
                else
                {
                    ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
                    ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
                    return RedirectToAction("IndexUser", "CentroCustos");
                }
            }

            if (HttpContext.Request.Cookies.Keys.Contains("adm_cms_dv"))
            {
                ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
                ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
                return RedirectToAction("Index", "CentroCustos", new { controller = "CentroCustos", action = "Index", error = "Não foi possível gravar, tente novamente." });
            }
            else
            {
                ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
                ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
                return RedirectToAction("IndexUser", "CentroCustos", new { controller = "CentroCustos", action = "Index", error = "Não foi possível gravar, tente novamente." });
            }
        }

        [Logado]
        // GET: ArtCentroCustos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artCentroCusto = await _context.ArtCentroCustos.FindAsync(id);
            if (artCentroCusto == null)
            {
                return NotFound();
            }
            ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
            return View(artCentroCusto);
        }

        // GET: ArtCentroCustos/Edit/5
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artCentroCusto = await _context.ArtCentroCustos.FindAsync(id);
            if (artCentroCusto == null)
            {
                return NotFound();
            }
            ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
            return View(artCentroCusto);
        }

        // POST: ArtCentroCustos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CentroCustoId,ArtigoId,Qtd,Nomeservico,Observacoes,Uniservico,Valor")] ArtCentroCusto artCentroCusto)
        {
            if (id != artCentroCusto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (artCentroCusto.CentroCustoId > 0 && artCentroCusto.ArtigoId > 0 && artCentroCusto.Valor == 0)
                    {
                        var artigo = _context.Artigos.Where(i => i.Id == artCentroCusto.ArtigoId).ToList();
                        artCentroCusto.Valor = artigo[0].PCusto * artCentroCusto.Qtd;
                    }
                    _context.Update(artCentroCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtCentroCustoExists(artCentroCusto.Id))
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
            ViewData["ArtigoId"] = new SelectList(_context.Artigos, "Id", "Nome", artCentroCusto.ArtigoId);
            ViewData["CentroCustoId"] = new SelectList(_context.CentroCustos, "Id", "Nome", artCentroCusto.CentroCustoId);
            return View(artCentroCusto);
        }

        [Logado]
        // GET: ArtCentroCustos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artCentroCusto = await _context.ArtCentroCustos
                .Include(a => a.Artigo)
                .Include(a => a.CentroCusto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (artCentroCusto == null)
            {
                return NotFound();
            }

            return View(artCentroCusto);
        }

        [Logado]
        // POST: ArtCentroCustos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artCentroCusto = await _context.ArtCentroCustos.FindAsync(id);
            _context.ArtCentroCustos.Remove(artCentroCusto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtCentroCustoExists(int id)
        {
            return _context.ArtCentroCustos.Any(e => e.Id == id);
        }
    }
}
