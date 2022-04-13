using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class FuncCentroCusto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CentroCusto")]
        public int CentroCustoId { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }

        [Required]
        [ForeignKey("Funcionario")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime Data { get; set; }

        public float Qtd { get; set; }

        public float Valor { get; set; }
    }
}