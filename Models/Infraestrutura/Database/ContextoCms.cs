using cms_stock.Models.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace cms_stock.Models.Infraestrutura.Database
{
    public class ContextoCms: DbContext
    {
        public ContextoCms(DbContextOptions<ContextoCms> options): base(options) { }

        public ContextoCms() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            optionsBuilder.UseSqlServer(jAppSettings["ConexaoSql"].ToString());
        }

        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<CentroCusto> CentroCustos { get; set; }
    }
}