using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class CreateArtigos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artigos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Referencia = table.Column<string>(maxLength: 50, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    stockatual = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artigos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artigos");
        }
    }
}
