using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymBackend.Migrations
{
    /// <inheritdoc />
    public partial class QuitarCascadaDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Socios_IdSocio",
                table: "Asistencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_PlanesPago_IdPlan",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Socios_IdSocio",
                table: "Pagos");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Socios_IdSocio",
                table: "Asistencias",
                column: "IdSocio",
                principalTable: "Socios",
                principalColumn: "IdSocio",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_PlanesPago_IdPlan",
                table: "Pagos",
                column: "IdPlan",
                principalTable: "PlanesPago",
                principalColumn: "IdPlan",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Socios_IdSocio",
                table: "Pagos",
                column: "IdSocio",
                principalTable: "Socios",
                principalColumn: "IdSocio",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_Socios_IdSocio",
                table: "Asistencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_PlanesPago_IdPlan",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Socios_IdSocio",
                table: "Pagos");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_Socios_IdSocio",
                table: "Asistencias",
                column: "IdSocio",
                principalTable: "Socios",
                principalColumn: "IdSocio",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_PlanesPago_IdPlan",
                table: "Pagos",
                column: "IdPlan",
                principalTable: "PlanesPago",
                principalColumn: "IdPlan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Socios_IdSocio",
                table: "Pagos",
                column: "IdSocio",
                principalTable: "Socios",
                principalColumn: "IdSocio",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
