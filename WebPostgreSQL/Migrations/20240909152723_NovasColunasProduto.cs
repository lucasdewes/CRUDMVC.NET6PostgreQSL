using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class NovasColunasProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantidade",
                table: "Produto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedida",
                table: "Produto",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "UnidadeMedida",
                table: "Produto");
        }
    }
}
