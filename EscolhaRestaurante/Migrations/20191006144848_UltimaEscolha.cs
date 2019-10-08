using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EscolhaRestaurante.Migrations
{
    public partial class UltimaEscolha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEscolha",
                table: "Restaurantes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimaEscolha",
                table: "Restaurantes");
        }
    }
}
