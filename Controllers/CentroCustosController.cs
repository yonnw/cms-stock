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
            var artigosValorTotal = _context.ArtCentroCustos.Include(a => a.Artigo).Where(a => a.CentroCustoId == CCustoid).Sum(a => a.VVenda);
            var funcionarios = _context.FuncCentroCustos.Where(a => a.CentroCustoId == CCustoid);
            var funcionariosValorTotal = _context.FuncCentroCustos.Where(a => a.CentroCustoId == CCustoid).Sum(a => a.VVenda);
            var equipamentos = _context.EquiCentroCustos.Include(e => e.Equipamento).Where(a => a.CentroCustoId == CCustoid);
            var equipamentosValorTotal = _context.EquiCentroCustos.Include(e => e.Equipamento).Where(a => a.CentroCustoId == CCustoid).Sum(a => a.VVenda);

            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            doc.AddCreationDate();
            string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\wwwroot\pdf\" + "Orçamento nº" + CCustoid + ".pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            //string simg = @"C:\pdf\logo_rm.png";
            string simg = @"wwwroot/images/logo_rm.png";
            Image img = Image.GetInstance(simg);
            img.ScaleAbsolute(50, 50);
            //img.SetAbsolutePosition(10,30);

            doc.Add(img);

            //Fonts
            Font fontN12 = new Font(Font.NORMAL, 12);
            Font fontN10 = new Font(Font.NORMAL, 10);

            Paragraph titulo = new Paragraph();
            titulo.Font = new Font(Font.FontFamily.COURIER, 12);
            titulo.Alignment = Element.ALIGN_RIGHT;
            titulo.Add("Orçamento nº" + CCustoid + "\n\n");
            doc.Add(titulo);

            PdfPTable tblHeader = new PdfPTable(3);

            //relative col widths in proportions
            //tblHeader.WidthPercentage = 90f;
            //int[] tblHeadercellwidth = { 45, 45 };
            //tblHeader.SetWidths(tblHeadercellwidth);
            tblHeader.SetWidthPercentage(new float[] { 250, 100, 250 }, PageSize.A4);

            //Hiding table border
            tblHeader.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell nomeEmpresa = new PdfPCell(new Phrase("RICARDO MOTA - UNIPESSOAL LDA", fontN12));
            nomeEmpresa.Colspan = 2;
            nomeEmpresa.Border = 0;
            nomeEmpresa.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(nomeEmpresa);

            PdfPCell moradaEmpresa = new PdfPCell(new Phrase("Cliente: " + centroCusto.NomeCliente, fontN10));
            moradaEmpresa.Colspan = 1;
            moradaEmpresa.Border = 0;
            moradaEmpresa.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(moradaEmpresa);

            PdfPCell moradaEmpresaTbl = new PdfPCell(new Phrase("Rua do Lavadouro, nº 70", fontN10));
            moradaEmpresaTbl.Colspan = 2;
            moradaEmpresaTbl.Border = 0;
            moradaEmpresaTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(moradaEmpresaTbl);

            PdfPCell dataInicioTbl = new PdfPCell(new Phrase("Obra: " + centroCusto.Nome, fontN10));
            dataInicioTbl.Colspan = 1;
            dataInicioTbl.Border = 0;
            dataInicioTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(dataInicioTbl);

            PdfPCell codigopostalTbl = new PdfPCell(new Phrase("4575-279 Oldrões, Penafiel", fontN10));
            codigopostalTbl.Colspan = 2;
            codigopostalTbl.Border = 0;
            codigopostalTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(codigopostalTbl);

            PdfPCell dataFimTbl = new PdfPCell(new Phrase("Morada: " + centroCusto.Morada, fontN10));
            dataFimTbl.Colspan = 1;
            dataFimTbl.Border = 0;
            dataFimTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(dataFimTbl);

            PdfPCell testelTbl = new PdfPCell(new Phrase("", fontN10));
            testelTbl.Colspan = 2;
            testelTbl.Border = 0;
            testelTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(testelTbl);

            PdfPCell codPostalClienteTbl = new PdfPCell(new Phrase("Código Postal: " + centroCusto.CodPostal, fontN10));
            codPostalClienteTbl.Colspan = 1;
            codPostalClienteTbl.Border = 0;
            codPostalClienteTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeader.AddCell(codPostalClienteTbl);

            doc.Add(tblHeader);

            Paragraph centrodecusto = new Paragraph("", new Font(Font.NORMAL, 10));

            var dataInicio = "Data de Início: " + String.Format("{0:dd/MM/yyyy}", centroCusto.DataInicial) + "\n";
            var dataFinal = "Data de Fim: " + String.Format("{0:dd/MM/yyyy}", centroCusto.DataFinal) + "\n\n";

            centrodecusto.Add(dataInicio);
            centrodecusto.Add(dataFinal);

            doc.Add(centrodecusto);

            PdfPTable tblartigos = new PdfPTable(4);

            //relative col widths in proportions
            tblartigos.WidthPercentage = 90f;
            int[] firstTablecellwidth = { 46, 18, 18, 18 };
            tblartigos.SetWidths(firstTablecellwidth);

            //Hiding table border
            tblartigos.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell tituloTblArtigos = new PdfPCell(new Phrase("Lista de Artigos | Serviços \n", fontN12));
            tituloTblArtigos.Colspan = 4;
            tituloTblArtigos.Border = 0;
            tituloTblArtigos.HorizontalAlignment = 1;
            tblartigos.AddCell(tituloTblArtigos);

            PdfPCell tituloTblNome = new PdfPCell(new Phrase("Nome", fontN10));
            tituloTblNome.Colspan = 1;
            tituloTblNome.Border = 0;
            tituloTblNome.HorizontalAlignment = 1;
            tblartigos.AddCell(tituloTblNome);

            PdfPCell tituloTblQtd = new PdfPCell(new Phrase("Quantidade", fontN10));
            tituloTblQtd.Colspan = 1;
            tituloTblQtd.Border = 0;
            tituloTblQtd.HorizontalAlignment = 1;
            tblartigos.AddCell(tituloTblQtd);

            PdfPCell tituloTblVUnit = new PdfPCell(new Phrase("Valor Unit.", fontN10));
            tituloTblVUnit.Colspan = 1;
            tituloTblVUnit.Border = 0;
            tituloTblVUnit.HorizontalAlignment = 1;
            tblartigos.AddCell(tituloTblVUnit);

            PdfPCell tituloTblVTotal = new PdfPCell(new Phrase("Valor Total", fontN10));
            tituloTblVTotal.Colspan = 1;
            tituloTblVTotal.Border = 0;
            tituloTblVTotal.HorizontalAlignment = 1;
            tblartigos.AddCell(tituloTblVTotal);

            foreach (var item in artigos)
            {
                if (item.ArtigoId != 1048)
                {
                    tblartigos.AddCell(new PdfPCell(new Phrase(item.Artigo.Nome, fontN10)));
                }
                else
                {
                    tblartigos.AddCell(new PdfPCell(new Phrase(item.Nomeservico, fontN10)));
                }
                var convertItemQtd = Convert.ToString(item.Qtd);
                tblartigos.AddCell(new PdfPCell(new Phrase(convertItemQtd, fontN10)));

                var convertItemVVendaUnit = String.Format("{0:#,###.00€}", item.VVendaUnit);
                tblartigos.AddCell(new PdfPCell(new Phrase(convertItemVVendaUnit, fontN10)));

                var convertVVenda = String.Format("{0:#,###.00€}", item.VVenda);
                tblartigos.AddCell(new PdfPCell(new Phrase(convertVVenda, fontN10)));
            }

            doc.Add(tblartigos);

            PdfPTable tblequipamentos = new PdfPTable(4);

            //relative col widths in proportions
            tblequipamentos.WidthPercentage = 90f;
            //int[] firstTablecellwidth = { 46, 18, 18, 18 };
            tblequipamentos.SetWidths(firstTablecellwidth);

            //Hiding table border
            tblequipamentos.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell tituloTblEquipamentos = new PdfPCell(new Phrase("Lista de Equipamentos \n", fontN12));
            tituloTblEquipamentos.Colspan = 4;
            tituloTblEquipamentos.Border = 0;
            tituloTblEquipamentos.HorizontalAlignment = 1;
            tblequipamentos.AddCell(tituloTblEquipamentos);

            PdfPCell equipamentosTblNome = new PdfPCell(new Phrase("Nome", fontN10));
            equipamentosTblNome.Colspan = 1;
            equipamentosTblNome.Border = 0;
            equipamentosTblNome.HorizontalAlignment = 1;
            tblequipamentos.AddCell(equipamentosTblNome);

            PdfPCell equipamentosTblQtd = new PdfPCell(new Phrase("Quantidade", fontN10));
            equipamentosTblQtd.Colspan = 1;
            equipamentosTblQtd.Border = 0;
            equipamentosTblQtd.HorizontalAlignment = 1;
            tblequipamentos.AddCell(equipamentosTblQtd);

            PdfPCell equipamentosTblVUnit = new PdfPCell(new Phrase("Valor Unit.", fontN10));
            equipamentosTblVUnit.Colspan = 1;
            equipamentosTblVUnit.Border = 0;
            equipamentosTblVUnit.HorizontalAlignment = 1;
            tblequipamentos.AddCell(equipamentosTblVUnit);

            PdfPCell equipamentosTblVTotal = new PdfPCell(new Phrase("Valor Total", fontN10));
            equipamentosTblVTotal.Colspan = 1;
            equipamentosTblVTotal.Border = 0;
            equipamentosTblVTotal.HorizontalAlignment = 1;
            tblequipamentos.AddCell(equipamentosTblVTotal);

            foreach (var item in equipamentos)
            {

                tblequipamentos.AddCell(new PdfPCell(new Phrase(item.Equipamento.Nome, fontN10)));

                var convertItemQtd = Convert.ToString(item.Qtd);
                tblequipamentos.AddCell(new PdfPCell(new Phrase(convertItemQtd, fontN10)));

                var convertItemVVendaUnit = String.Format("{0:#,###.00€}", item.VVendaUnit);
                tblequipamentos.AddCell(new PdfPCell(new Phrase(convertItemVVendaUnit, fontN10)));

                var convertVVenda = String.Format("{0:#,###.00€}", item.VVenda);
                tblequipamentos.AddCell(new PdfPCell(new Phrase(convertVVenda, fontN10)));
            }

            doc.Add(tblequipamentos);

            Paragraph totalDocumento = new Paragraph("\n\n\n", new Font(Font.NORMAL, 10));
            totalDocumento.Alignment = Element.ALIGN_RIGHT;

            var tituloValores = "Totais do Orçamento: \n\n";
            var totalMaoobra = "Mão de Obra: " + String.Format("{0:#,###.00€}", funcionariosValorTotal) + "\n";
            var totalEquipamentos = "Equipamentos: " + String.Format("{0:#,###.00€}", equipamentosValorTotal) + "\n";
            var totalArtigos = "Artigos e Serviços: " + String.Format("{0:#,###.00€}", artigosValorTotal) + "\n\n";
            var totalVendas = "Total Orçamento: " + String.Format("{0:#,###.00€}", centroCusto.VFinalVenda) + "\n\n";

            totalDocumento.Add(tituloValores);
            totalDocumento.Add(totalMaoobra);
            totalDocumento.Add(totalEquipamentos);
            totalDocumento.Add(totalArtigos);
            totalDocumento.Add(totalVendas);

            doc.Add(totalDocumento);

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
        public async Task<IActionResult> Create([Bind("Id,Nome,DataInicial,DataFinal,Fechada,Observacao,VOrcamento,CodPostal,Morada,NomeCliente")] CentroCusto centroCusto)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataInicial,DataFinal,Fechada,Observacao,VOrcamento,CodPostal,Morada,NomeCliente")] CentroCusto centroCusto)
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
