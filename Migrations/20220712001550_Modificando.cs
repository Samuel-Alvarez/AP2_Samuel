using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parcial2.Migrations
{
    public partial class Modificando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Verduras",
                columns: table => new
                {
                    VerduraId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verduras", x => x.VerduraId);
                });

            migrationBuilder.CreateTable(
                name: "Vitaminas",
                columns: table => new
                {
                    VitaminaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Existencia = table.Column<double>(type: "REAL", nullable: false),
                    UnidadMedida = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitaminas", x => x.VitaminaId);
                });

            migrationBuilder.CreateTable(
                name: "VerdurasDetalle",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VerduraId = table.Column<int>(type: "INTEGER", nullable: false),
                    VitaminaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<double>(type: "REAL", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerdurasDetalle", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_VerdurasDetalle_Verduras_VerduraId",
                        column: x => x.VerduraId,
                        principalTable: "Verduras",
                        principalColumn: "VerduraId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vitaminas",
                columns: new[] { "VitaminaId", "Descripcion", "Existencia", "UnidadMedida" },
                values: new object[] { 1, "Vitamina C", 0.0, "(mg)" });

            migrationBuilder.InsertData(
                table: "Vitaminas",
                columns: new[] { "VitaminaId", "Descripcion", "Existencia", "UnidadMedida" },
                values: new object[] { 2, "Betaína", 0.0, "(mg)" });

            migrationBuilder.InsertData(
                table: "Vitaminas",
                columns: new[] { "VitaminaId", "Descripcion", "Existencia", "UnidadMedida" },
                values: new object[] { 3, "Vitamina K", 0.0, "(mcg)" });

            migrationBuilder.InsertData(
                table: "Vitaminas",
                columns: new[] { "VitaminaId", "Descripcion", "Existencia", "UnidadMedida" },
                values: new object[] { 4, "Vitamina A ", 0.0, "(mcg RAE)" });

            migrationBuilder.InsertData(
                table: "Vitaminas",
                columns: new[] { "VitaminaId", "Descripcion", "Existencia", "UnidadMedida" },
                values: new object[] { 5, "Vitamina E", 0.0, "(mg)" });

            migrationBuilder.InsertData(
                table: "Vitaminas",
                columns: new[] { "VitaminaId", "Descripcion", "Existencia", "UnidadMedida" },
                values: new object[] { 6, "Ácido fólico(B9)", 0.0, "(mg)" });

            migrationBuilder.CreateIndex(
                name: "IX_VerdurasDetalle_VerduraId",
                table: "VerdurasDetalle",
                column: "VerduraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerdurasDetalle");

            migrationBuilder.DropTable(
                name: "Vitaminas");

            migrationBuilder.DropTable(
                name: "Verduras");
        }
    }
}
