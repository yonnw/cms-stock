using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class ValorVendaTabelasMov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "VVenda",
                table: "FuncCentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VVenda",
                table: "EquiCentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VVenda",
                table: "ArtCentroCustos",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VVenda",
                table: "FuncCentroCustos");

            migrationBuilder.DropColumn(
                name: "VVenda",
                table: "EquiCentroCustos");

            migrationBuilder.DropColumn(
                name: "VVenda",
                table: "ArtCentroCustos");
        }
    }
}
