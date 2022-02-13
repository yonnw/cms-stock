using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class AddPrecoCustoUnidadeArtigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PCusto",
                table: "Artigos",
                maxLength: 12,
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Unidade",
                table: "Artigos",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PCusto",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "Unidade",
                table: "Artigos");
        }
    }
}
