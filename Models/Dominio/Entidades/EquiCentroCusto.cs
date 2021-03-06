using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class EquiCentroCusto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CentroCusto")]
        public int CentroCustoId { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }

        [Required]
        [ForeignKey("Equipamento")]
        public int EquipamentoId { get; set; }
        public virtual Equipamento Equipamento { get; set; }

        public float Qtd { get; set; }

        public float Valor { get; set; }
        public float ValorUnit { get; set; }
        public float VVenda { get; set; }
        public float VVendaUnit { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
    }
}