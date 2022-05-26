using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class ValorVendaUnitMov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "VVendaUnit",
                table: "FuncCentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VVendaUnit",
                table: "EquiCentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "VVendaUnit",
                table: "ArtCentroCustos",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VVendaUnit",
                table: "FuncCentroCustos");

            migrationBuilder.DropColumn(
                name: "VVendaUnit",
                table: "EquiCentroCustos");

            migrationBuilder.DropColumn(
                name: "VVendaUnit",
                table: "ArtCentroCustos");
        }
    }
}
