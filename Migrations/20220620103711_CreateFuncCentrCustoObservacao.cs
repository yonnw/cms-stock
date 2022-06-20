using Microsoft.EntityFrameworkCore.Migrations;

namespace cms_stock.Migrations
{
    public partial class CreateFuncCentrCustoObservacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "FuncCentroCustos",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "FuncCentroCustos");
        }
    }
}
