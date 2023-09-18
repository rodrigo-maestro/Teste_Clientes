using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CriandoProceduresAlterarExcluirClienteLogradouro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[AlterarCliente]
                @Id INT,
                @Nome NVARCHAR(255),
                @Email NVARCHAR(255),
                @IdLogotipo NVARCHAR(511)
            AS
            BEGIN
                UPDATE Clientes
                SET
                    Nome = @Nome,
                    Email = @Email,
                    IdLogotipo = @IdLogotipo
                WHERE Id = @Id
            END

            GO

            CREATE PROCEDURE [dbo].[AlterarLogradouro]
                @Id INT,
                @Descricao NVARCHAR(511)
            AS
            BEGIN
                UPDATE Logradouros
                SET
                    Descricao = @Descricao
                WHERE Id = @Id
            END

            GO

            CREATE PROCEDURE [dbo].[ExcluirCliente]
                @Id INT
            AS
            BEGIN
                DELETE FROM Logradouros
                WHERE ClienteId = @Id

                DELETE FROM Clientes
                WHERE Id = @Id
            END

            GO

            CREATE PROCEDURE [dbo].[ExcluirLogradouro]
                @Id INT
            AS
            BEGIN
                DELETE FROM Logradouros
                WHERE Id = @Id
            END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF OBJECT_ID('dbo.AlterarCliente', 'P') IS NOT NULL
            BEGIN
                DROP PROCEDURE [dbo].[AlterarCliente]
            END

            GO

            IF OBJECT_ID('dbo.AlterarLogradouro', 'P') IS NOT NULL
            BEGIN
                DROP PROCEDURE [dbo].[AlterarLogradouro]
            END
            
            GO

            IF OBJECT_ID('dbo.ExcluirCliente', 'P') IS NOT NULL
            BEGIN
                DROP PROCEDURE [dbo].[ExcluirCliente]
            END

            GO

            IF OBJECT_ID('dbo.ExcluirLogradouro', 'P') IS NOT NULL
            BEGIN
                DROP PROCEDURE [dbo].[ExcluirLogradouro]
            END");
        }
    }
}
