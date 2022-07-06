using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class addFielsToCentroCustoLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodPostal",
                table: "CentroCustos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "CentroCustos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeCliente",
                table: "CentroCustos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodPostal",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "Morada",
                table: "CentroCustos");

            migrationBuilder.DropColumn(
                name: "NomeCliente",
                table: "CentroCustos");
        }
    }
}
