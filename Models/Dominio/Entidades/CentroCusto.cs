using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class CentroCusto
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicial { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataFinal { get; set; }

        public float ValorTotal { get; set; }

        public bool Fechada { get; set; }

        [Column(TypeName = "text")]
        public string Observacao { get; set; }

        public float VFinalVenda { get; set; }

        public string NomeCompleto { get; set; }

        public string Referencia { get; set; }

        public float LucroEuros { get; set; }

        public float VOrcamento { get; set; }

        public string NomeCliente { get; set; }

        public string Morada { get; set; }

        public string CodPostal { get; set; }
    }
}