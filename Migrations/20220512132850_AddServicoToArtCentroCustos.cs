using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class AddServicoToArtCentroCustos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nomeservico",
                table: "ArtCentroCustos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "ArtCentroCustos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uniservico",
                table: "ArtCentroCustos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nomeservico",
                table: "ArtCentroCustos");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "ArtCentroCustos");

            migrationBuilder.DropColumn(
                name: "Uniservico",
                table: "ArtCentroCustos");
        }
    }
}
