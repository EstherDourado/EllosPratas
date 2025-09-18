using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EllosPratas.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoQuantidadeProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantidade",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantidade",
                table: "Produtos");
        }
    }
}
