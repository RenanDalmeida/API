using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Core_Code_First.Migrations
{
    public partial class AlterTablePedidoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "PedidosProdutos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "PedidosProdutos");
        }
    }
}
