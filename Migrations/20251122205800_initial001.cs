using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoTrack.Migrations
{
    /// <inheritdoc />
    public partial class initial001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Casa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agua",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    CasaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agua", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agua_Casa_CasaId",
                        column: x => x.CasaId,
                        principalTable: "Casa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Luz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    CasaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Luz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Luz_Casa_CasaId",
                        column: x => x.CasaId,
                        principalTable: "Casa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Residuo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    CasaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residuo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residuo_Casa_CasaId",
                        column: x => x.CasaId,
                        principalTable: "Casa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agua_CasaId",
                table: "Agua",
                column: "CasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Luz_CasaId",
                table: "Luz",
                column: "CasaId");

            migrationBuilder.CreateIndex(
                name: "IX_Residuo_CasaId",
                table: "Residuo",
                column: "CasaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agua");

            migrationBuilder.DropTable(
                name: "Luz");

            migrationBuilder.DropTable(
                name: "Residuo");

            migrationBuilder.DropTable(
                name: "Casa");
        }
    }
}
