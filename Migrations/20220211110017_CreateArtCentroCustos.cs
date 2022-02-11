using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class CreateArtCentroCustos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Artigos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "ArtCentroCustos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentroCustoId = table.Column<int>(nullable: false),
                    ArtigoId = table.Column<int>(nullable: false),
                    Qtd = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtCentroCustos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtCentroCustos_Artigos_ArtigoId",
                        column: x => x.ArtigoId,
                        principalTable: "Artigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtCentroCustos_CentroCustos_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCustos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtCentroCustos_ArtigoId",
                table: "ArtCentroCustos",
                column: "ArtigoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtCentroCustos_CentroCustoId",
                table: "ArtCentroCustos",
                column: "CentroCustoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtCentroCustos");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Artigos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
