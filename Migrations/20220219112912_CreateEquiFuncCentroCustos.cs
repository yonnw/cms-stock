using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class CreateEquiFuncCentroCustos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Administradores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "EquiCentroCustos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentroCustoId = table.Column<int>(nullable: false),
                    EquipamentoId = table.Column<int>(nullable: false),
                    Qtd = table.Column<float>(nullable: false),
                    Valor = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquiCentroCustos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquiCentroCustos_CentroCustos_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCustos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquiCentroCustos_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FuncCentroCustos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentroCustoId = table.Column<int>(nullable: false),
                    FuncionarioId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Qtd = table.Column<float>(nullable: false),
                    Valor = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncCentroCustos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncCentroCustos_CentroCustos_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCustos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncCentroCustos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquiCentroCustos_CentroCustoId",
                table: "EquiCentroCustos",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquiCentroCustos_EquipamentoId",
                table: "EquiCentroCustos",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncCentroCustos_CentroCustoId",
                table: "FuncCentroCustos",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncCentroCustos_FuncionarioId",
                table: "FuncCentroCustos",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquiCentroCustos");

            migrationBuilder.DropTable(
                name: "FuncCentroCustos");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Administradores");
        }
    }
}
