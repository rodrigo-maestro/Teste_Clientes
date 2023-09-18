using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class VinculoLogradouro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Logradouros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logradouros_ClienteId",
                table: "Logradouros",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logradouros_Clientes_ClienteId",
                table: "Logradouros",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logradouros_Clientes_ClienteId",
                table: "Logradouros");

            migrationBuilder.DropIndex(
                name: "IX_Logradouros_ClienteId",
                table: "Logradouros");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Logradouros");
        }
    }
}
