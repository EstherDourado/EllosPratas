using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EllosPratas.Migrations
{
    /// <inheritdoc />
    public partial class RemoverColunaImagemString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagem",
                table: "Produtos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "imagem",
                table: "Produtos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
