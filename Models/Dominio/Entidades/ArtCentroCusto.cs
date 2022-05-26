using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class ArtCentroCusto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CentroCusto")]
        public int CentroCustoId { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }

        [Required]
        [ForeignKey("Artigo")]
        public int ArtigoId { get; set; }
        public virtual Artigo Artigo { get; set; }

        [Required]
        public float Qtd { get; set; }
        public float Valor { get; set; }
        public float ValorUnit { get; set; }
        public float VVenda { get; set; }
        public float VVendaUnit { get; set; }
        public string Nomeservico { get; set; }
        public string Observacoes { get; set; }
        public string Uniservico { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
    }
}