﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cms_stock.Models.Infraestrutura.Database;

namespace cms_stock.Migrations
{
    [DbContext(typeof(ContextoCms))]
    partial class ContextoCmsModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.ArtCentroCusto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtigoId")
                        .HasColumnType("int");

                    b.Property<int>("CentroCustoId")
                        .HasColumnType("int");

                    b.Property<float>("Qtd")
                        .HasColumnType("real");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ArtigoId");

                    b.HasIndex("CentroCustoId");

                    b.ToTable("ArtCentroCustos");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.Artigo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Inativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Observacao")
                        .HasColumnType("text");

                    b.Property<float>("PCusto")
                        .HasColumnType("real");

                    b.Property<string>("Referencia")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("Stockatual")
                        .HasColumnType("int");

                    b.Property<string>("Unidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Artigos");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.CentroCusto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Fechada")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Observacao")
                        .HasColumnType("text");

                    b.Property<float>("ValorTotal")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("CentroCustos");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.EquiCentroCusto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CentroCustoId")
                        .HasColumnType("int");

                    b.Property<int>("EquipamentoId")
                        .HasColumnType("int");

                    b.Property<float>("Qtd")
                        .HasColumnType("real");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CentroCustoId");

                    b.HasIndex("EquipamentoId");

                    b.ToTable("EquiCentroCustos");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.Equipamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Inativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<float>("PCusto")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Equipamentos");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.FuncCentroCusto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CentroCustoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("int");

                    b.Property<float>("Qtd")
                        .HasColumnType("real");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CentroCustoId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("FuncCentroCustos");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Inativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<float>("Valordia")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.Pagina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AreaRestrita")
                        .HasColumnType("bit");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Home")
                        .HasColumnType("bit");

                    b.Property<bool>("Login")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Ordem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Paginas");
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.ArtCentroCusto", b =>
                {
                    b.HasOne("cms_stock.Models.Dominio.Entidades.Artigo", "Artigo")
                        .WithMany()
                        .HasForeignKey("ArtigoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cms_stock.Models.Dominio.Entidades.CentroCusto", "CentroCusto")
                        .WithMany()
                        .HasForeignKey("CentroCustoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.EquiCentroCusto", b =>
                {
                    b.HasOne("cms_stock.Models.Dominio.Entidades.CentroCusto", "CentroCusto")
                        .WithMany()
                        .HasForeignKey("CentroCustoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cms_stock.Models.Dominio.Entidades.Equipamento", "Equipamento")
                        .WithMany()
                        .HasForeignKey("EquipamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms_stock.Models.Dominio.Entidades.FuncCentroCusto", b =>
                {
                    b.HasOne("cms_stock.Models.Dominio.Entidades.CentroCusto", "CentroCusto")
                        .WithMany()
                        .HasForeignKey("CentroCustoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cms_stock.Models.Dominio.Entidades.Funcionario", "Funcionario")
                        .WithMany()
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
