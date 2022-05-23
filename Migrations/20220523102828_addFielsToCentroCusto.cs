using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class addFielsToCentroCusto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Valordia",
                table: "Equipamentos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LucroEuros",
                table: "CentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "CentroCustos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referencia",
                table: "CentroCustos",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "VFinalVenda",
                table: "CentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VOrcamento",
                table: "CentroCustos",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valordia",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "LucroEuros",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "Referencia",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "VFinalVenda",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "VOrcamento",
                table: "CentroCustos");
        }
    }
}
