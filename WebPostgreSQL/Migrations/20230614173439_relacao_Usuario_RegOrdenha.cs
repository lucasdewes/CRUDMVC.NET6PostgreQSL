using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPostgreSQL.Migrations
{
    public partial class relacao_Usuario_RegOrdenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataOrdenha",
                table: "RegistroOrdenha",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "RegistroOrdenha",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistroOrdenha_UsuarioId",
                table: "RegistroOrdenha",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroOrdenha_Usuarios_UsuarioId",
                table: "RegistroOrdenha",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroOrdenha_Usuarios_UsuarioId",
                table: "RegistroOrdenha");

            migrationBuilder.DropIndex(
                name: "IX_RegistroOrdenha_UsuarioId",
                table: "RegistroOrdenha");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "RegistroOrdenha");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataOrdenha",
                table: "RegistroOrdenha",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
