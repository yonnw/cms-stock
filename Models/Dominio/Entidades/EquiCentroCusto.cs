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

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}