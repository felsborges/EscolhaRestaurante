using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolhaRestaurante.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurantes",
                columns: table => new
                {
                    RestauranteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurantes", x => x.RestauranteID);
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    VotoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DataVoto = table.Column<DateTime>(nullable: false),
                    RestauranteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.VotoID);
                    table.ForeignKey(
                        name: "FK_Votos_Restaurantes_RestauranteID",
                        column: x => x.RestauranteID,
                        principalTable: "Restaurantes",
                        principalColumn: "RestauranteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votos_RestauranteID",
                table: "Votos",
                column: "RestauranteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "Restaurantes");
        }
    }
}
