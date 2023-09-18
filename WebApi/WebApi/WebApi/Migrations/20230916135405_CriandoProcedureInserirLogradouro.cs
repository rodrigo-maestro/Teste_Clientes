using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CriandoProcedureInserirLogradouro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[InserirLogradouro]
                @IdCliente INT,
                @Descricao NVARCHAR(511),
                @LogradouroId INT OUTPUT
            AS
            BEGIN
                INSERT INTO Logradouros (ClienteId, Descricao)
                VALUES (@IdCliente, @Descricao)

                SET @LogradouroId = SCOPE_IDENTITY()
            END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP PROCEDURE [dbo].[InserirLogradouro]
            ");
        }
    }
}
