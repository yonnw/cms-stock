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

        public float Qtd { get; set; }
    }
}