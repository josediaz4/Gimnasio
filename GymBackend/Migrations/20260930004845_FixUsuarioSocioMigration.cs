using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioSocioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Socios_IdUsuario",
                table: "Socios");

            migrationBuilder.AddColumn<int>(
                name: "IdSocio",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Socios_IdUsuario",
                table: "Socios",
                column: "IdUsuario",
                unique: true,
                filter: "[IdUsuario] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Socios_IdUsuario",
                table: "Socios");

            migrationBuilder.DropColumn(
                name: "IdSocio",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_IdUsuario",
                table: "Socios",
                column: "IdUsuario");
        }
    }
}
