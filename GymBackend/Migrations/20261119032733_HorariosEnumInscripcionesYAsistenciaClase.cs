using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackend.Migrations
{
    /// <inheritdoc />
    public partial class HorariosEnumInscripcionesYAsistenciaClase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "Horarios",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdHorario",
                table: "Asistencias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    IdInscripcion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSocio = table.Column<int>(type: "int", nullable: false),
                    IdHorario = table.Column<int>(type: "int", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.IdInscripcion);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Horarios_IdHorario",
                        column: x => x.IdHorario,
                        principalTable: "Horarios",
                        principalColumn: "IdHorario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Socios_IdSocio",
                        column: x => x.IdSocio,
                        principalTable: "Socios",
                        principalColumn: "IdSocio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdHorario",
                table: "Asistencias",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdHorario",
                table: "Inscripciones",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdSocio_IdHorario",
                table: "Inscripciones",
                columns: new[] { "IdSocio", "IdHorario" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Horarios_IdHorario",
                table: "Asistencias",
                column: "IdHorario",
                principalTable: "Horarios",
                principalColumn: "IdHorario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Horarios_IdHorario",
                table: "Asistencias");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Asistencias_IdHorario",
                table: "Asistencias");

            migrationBuilder.DropColumn(
                name: "IdHorario",
                table: "Asistencias");

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "Horarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
