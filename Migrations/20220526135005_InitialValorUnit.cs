using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class InitialValorUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ValorUnit",
                table: "FuncCentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ValorUnit",
                table: "EquiCentroCustos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ValorUnit",
                table: "ArtCentroCustos",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorUnit",
                table: "FuncCentroCustos");

            migrationBuilder.DropColumn(
                name: "ValorUnit",
                table: "EquiCentroCustos");

            migrationBuilder.DropColumn(
                name: "ValorUnit",
                table: "ArtCentroCustos");
        }
    }
}
