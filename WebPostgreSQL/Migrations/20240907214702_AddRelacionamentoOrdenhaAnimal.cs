using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPostgreSQL.Migrations
{
    public partial class AddRelacionamentoOrdenhaAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdenhaAnimais",
                columns: table => new
                {
                    OrdenhaId = table.Column<int>(type: "integer", nullable: false),
                    AnimalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenhaAnimais", x => new { x.OrdenhaId, x.AnimalId });
                    table.ForeignKey(
                        name: "FK_OrdenhaAnimais_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenhaAnimais_RegistroOrdenha_OrdenhaId",
                        column: x => x.OrdenhaId,
                        principalTable: "RegistroOrdenha",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdenhaAnimais_AnimalId",
                table: "OrdenhaAnimais",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenhaAnimais");
        }
    }
}
