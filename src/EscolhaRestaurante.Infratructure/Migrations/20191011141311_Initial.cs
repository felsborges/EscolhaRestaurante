using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolhaRestaurante.Infratructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurante",
                columns: table => new
                {
                    RestauranteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    UltimaEscolha = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurante", x => x.RestauranteID);
                });

            migrationBuilder.CreateTable(
                name: "Voto",
                columns: table => new
                {
                    VotoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataVoto = table.Column<DateTime>(type: "DateTime", nullable: false),
                    RestauranteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voto", x => x.VotoID);
                    table.ForeignKey(
                        name: "FK_Voto_Restaurante_RestauranteID",
                        column: x => x.RestauranteID,
                        principalTable: "Restaurante",
                        principalColumn: "RestauranteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voto_RestauranteID",
                table: "Voto",
                column: "RestauranteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voto");

            migrationBuilder.DropTable(
                name: "Restaurante");
        }
    }
}
