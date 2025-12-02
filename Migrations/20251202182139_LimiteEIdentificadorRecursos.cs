using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoTrack.Migrations
{
    /// <inheritdoc />
    public partial class LimiteEIdentificadorRecursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Residuo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Limite",
                table: "Residuo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Luz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Limite",
                table: "Luz",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Agua",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Limite",
                table: "Agua",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Residuo");

            migrationBuilder.DropColumn(
                name: "Limite",
                table: "Residuo");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Luz");

            migrationBuilder.DropColumn(
                name: "Limite",
                table: "Luz");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Agua");

            migrationBuilder.DropColumn(
                name: "Limite",
                table: "Agua");
        }
    }
}
