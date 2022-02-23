using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace cms_stock.Models.Dominio.Entidades
{
    public class Funcionario
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Nome { get; set; }

        [Required]
        public bool Inativo { get; set; }

        [Required]
        public float Valordia { get; set; }
    }
}