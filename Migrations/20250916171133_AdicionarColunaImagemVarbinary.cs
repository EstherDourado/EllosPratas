using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EllosPratas.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarColunaImagemVarbinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "imagem",
                table: "Produtos",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagem",
                table: "Produtos");
        }
    }
}
