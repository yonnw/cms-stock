using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class Obra
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }

        public float ValorTotal { get; set; }

        public bool Fechada { get; set; }

        [Column(TypeName = "text")]
        public string Observacao { get; set; }
    }
}