using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class CreateFuncionarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stockatual",
                table: "Artigos",
                newName: "Stockatual");

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 250, nullable: false),
                    Inativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "Stockatual",
                table: "Artigos",
                newName: "stockatual");
        }
    }
}
