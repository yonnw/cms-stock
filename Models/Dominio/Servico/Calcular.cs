using cms_stock.Models.Dominio.Entidades;
using cms_stock.Models.Infraestrutura.Database;
using System.Linq;

namespace cms_stock.Models.Dominio.Servico
{
    public class Calcular 
    {
        private readonly ContextoCms _context;

        public Calcular(ContextoCms context)
        {
            _context = context;
        }

        public static float CalcularArt (ArtCentroCusto artCentroCusto)
        {
            var _context = new ContextoCms();
            var artigo = _context.Artigos.Where(i => i.Id == artCentroCusto.ArtigoId).ToList();
            artCentroCusto.Valor = artigo[0].PCusto * artCentroCusto.Qtd;
            return artCentroCusto.Valor;
        }

        public static float CalcularEqui (EquiCentroCusto equiCentroCusto)
        {
            var _context = new ContextoCms();
            var equipamento = _context.Equipamentos.Where(i => i.Id == equiCentroCusto.EquipamentoId).ToList();
            equiCentroCusto.Valor = equipamento[0].PCusto * equiCentroCusto.Qtd;
            return equiCentroCusto.Valor;
        }

        public static float CalcularFunc (FuncCentroCusto funcCentroCusto)
        {
            var _context = new ContextoCms();
            var funcionario = _context.Funcionarios.Where(i => i.Id == funcCentroCusto.FuncionarioId).ToList();
            funcCentroCusto.Valor = funcionario[0].Valordia * funcCentroCusto.Qtd;
            return funcCentroCusto.Valor;
        }
    }
}
