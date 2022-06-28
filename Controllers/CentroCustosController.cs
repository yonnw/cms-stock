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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using System.Collections;

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
                var centroCustos = _context.CentroCustos.Where(a => a.Nome.Contains(searchString) && a.Fechada == false).ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
            else
            {
                var centroCustos = _context.CentroCustos.Where(a => a.Fechada == false).ToPagedList(pageNumber, pageSize);
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
        public async Task<IActionResult> Concluded(string error, string searchString, int? page)
        {
            ViewBag.error = error;

            var pageNumber = page ?? 1;
            int pageSize = 10;

            if (!String.IsNullOrEmpty(searchString))
            {
                var centroCustos = _context.CentroCustos.Where(a => a.Nome.Contains(searchString) && a.Fechada == true).ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
            else
            {
                var centroCustos = _context.CentroCustos.Where(a => a.Fechada == true).ToPagedList(pageNumber, pageSize);
                return View(centroCustos);
            }
        }

        public void Imprimir(int CCustoid)
        {
            var centroCusto = _context.CentroCustos.Find(CCustoid);
            var artigos = _context.ArtCentroCustos.Include(a => a.Artigo).Where(a => a.CentroCustoId == CCustoid);
            var funcionarios = _context.FuncCentroCustos.Where(a => a.CentroCustoId == CCustoid);
            var equipamentos = _context.EquiCentroCustos.Where(a => a.CentroCustoId == CCustoid);

            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            doc.AddCreationDate();
            string caminho = @"c:\pdf\" + "Orçamento nº" + CCustoid + ".pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            string simg = @"C:\pdf\logo_rm.png";
            Image img = Image.GetInstance(simg);
            img.ScaleAbsolute(50, 50);
            //img.SetAbsolutePosition(10,30);

            doc.Add(img);

            Paragraph titulo = new Paragraph();
            titulo.Font = new Font(Font.FontFamily.COURIER, 12);
            titulo.Alignment = Element.ALIGN_RIGHT;
            titulo.Add("Orçamento nº" + CCustoid + "\n");
            doc.Add(titulo);

            Paragraph paragrafo = new Paragraph("", new Font(Font.NORMAL, 10));

            var nomeObra = "Centro de Custo: " + centroCusto.Nome + "\n";
            var dataInicio = "Data: " + String.Format("{0:dd/MM/yyyy}", centroCusto.DataInicial) + "\n";
            var totalVendas = "Valor: " + String.Format("{0:#,###.00€}", centroCusto.VFinalVenda) + "\n\n";

            paragrafo.Add(nomeObra);
            paragrafo.Add(dataInicio);
            paragrafo.Add(totalVendas);

            doc.Add(paragrafo);

            PdfPTable table = new PdfPTable(4);

            foreach(var item in artigos)
            {
                table.AddCell(item.Artigo.Nome);
                table.AddCell(Convert.ToString(item.Qtd));
                table.AddCell(String.Format("{0:#,###.00€}", item.VVendaUnit));
                table.AddCell(String.Format("{0:#,###.00€}", item.VVenda));        
            }

            doc.Add(table);

            doc.Close();
            Response.Redirect("/CentroCustos/Details/" + CCustoid);
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

            var totalArtigosVenda = _context.ArtCentroCustos.Where(i => i.CentroCusto.Id == centroCusto.Id).Sum(v => v.VVenda);
            var totalFuncionariosVenda = _context.FuncCentroCustos.Where(i => i.CentroCusto.Id == centroCusto.Id).Sum(v => v.VVenda);
            var totalEquipamentosVenda = _context.EquiCentroCustos.Where(i => i.CentroCusto.Id == centroCusto.Id).Sum(v => v.VVenda);

            centroCusto.VFinalVenda = totalArtigosVenda + totalFuncionariosVenda + totalEquipamentosVenda;

            centroCusto.ValorTotal = totalArtigos + totalFuncionarios + totalEquipamentos;
            if (centroCusto.VOrcamento > 0)
            {
                centroCusto.LucroEuros = centroCusto.VOrcamento - centroCusto.ValorTotal;
            }
            else
            {
                centroCusto.LucroEuros = centroCusto.VFinalVenda - centroCusto.ValorTotal;
            }
            _context.SaveChanges();

            ViewBag.totalArtigos = totalArtigos;
            ViewBag.totalFuncionarios = totalFuncionarios;
            ViewBag.totalEquipamentos = totalEquipamentos;

            ViewBag.totalVendaArtigos = totalArtigosVenda;
            ViewBag.totalVendaFuncionarios = totalFuncionariosVenda;
            ViewBag.totalVendaEquipamentos = totalEquipamentosVenda;

            ViewData["Funcionarios"] = _context.FuncCentroCustos.Include(f => f.Funcionario).Where(i => i.CentroCusto.Id == centroCusto.Id).OrderByDescending(d => d.Data).ToList();
            ViewData["Equipamentos"] = _context.EquiCentroCustos.Include(e => e.Equipamento).Where(i => i.CentroCusto.Id == centroCusto.Id).OrderByDescending(d => d.Data).ToList();
            ViewData["Artigos"] = _context.ArtCentroCustos.Include(a => a.Artigo).Where(i => i.CentroCusto.Id == centroCusto.Id).OrderByDescending(d => d.Data).ToList();

            if (centroCusto == null)
            {
                return NotFound();
            }

            return View(centroCusto);
        }


        // GET: CentroCustos/DetailsUser/5
        public async Task<IActionResult> DetailsUser(int? id)
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
            if (centroCusto.VOrcamento > 0)
            {
                centroCusto.LucroEuros = centroCusto.VOrcamento - centroCusto.ValorTotal;
            }
            _context.SaveChanges();

            ViewBag.totalArtigos = totalArtigos;
            ViewBag.totalFuncionarios = totalFuncionarios;
            ViewBag.totalEquipamentos = totalEquipamentos;

            ViewData["Funcionarios"] = _context.FuncCentroCustos.Include(f => f.Funcionario).Where(i => i.CentroCusto.Id == centroCusto.Id).OrderByDescending(d => d.Data).ToList();
            ViewData["Equipamentos"] = _context.EquiCentroCustos.Include(e => e.Equipamento).Where(i => i.CentroCusto.Id == centroCusto.Id).OrderByDescending(d => d.Data).ToList();
            ViewData["Artigos"] = _context.ArtCentroCustos.Include(a => a.Artigo).Where(i => i.CentroCusto.Id == centroCusto.Id).OrderByDescending(d => d.Data).ToList();

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
        public async Task<IActionResult> Create([Bind("Id,Nome,DataInicial,DataFinal,Fechada,Observacao,VOrcamento")] CentroCusto centroCusto)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataInicial,DataFinal,Fechada,Observacao,VOrcamento")] CentroCusto centroCusto)
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
