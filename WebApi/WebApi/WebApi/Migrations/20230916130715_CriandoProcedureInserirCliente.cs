using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CriandoProcedureInserirCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[InserirCliente]
                @Nome NVARCHAR(255),
                @Email NVARCHAR(255),
                @IdLogotipo NVARCHAR(511),
                @ClienteId INT OUTPUT
            AS
            BEGIN
                INSERT INTO Clientes (Nome, Email, IdLogotipo)
                VALUES (@Nome, @Email, @IdLogotipo)

                SET @ClienteId = SCOPE_IDENTITY()
            END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP PROCEDURE [dbo].[InserirCliente]
            ");
        }
    }
}
