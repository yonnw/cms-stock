using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class Artigo
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Referencia { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public bool Inativo { get; set; }

        [Required]
        public int Stockatual { get; set; }

        [Column(TypeName = "text")]
        public string Observacao { get; set; }
    }
}