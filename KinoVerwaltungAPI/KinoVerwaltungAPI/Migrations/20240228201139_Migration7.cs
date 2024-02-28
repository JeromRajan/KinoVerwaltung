using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinoVerwaltungAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benutzer_Mitgliederstatus_MitgliederstatusId",
                table: "Benutzer");

            migrationBuilder.DropIndex(
                name: "IX_Benutzer_MitgliederstatusId",
                table: "Benutzer");

            migrationBuilder.DropColumn(
                name: "MitgliederstatusId",
                table: "Benutzer");

            migrationBuilder.AddColumn<int>(
                name: "AnzahlGekaufterTickets",
                table: "Mitgliederkarten",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnzahlGekaufterTickets",
                table: "Mitgliederkarten");

            migrationBuilder.AddColumn<int>(
                name: "MitgliederstatusId",
                table: "Benutzer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Benutzer_MitgliederstatusId",
                table: "Benutzer",
                column: "MitgliederstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benutzer_Mitgliederstatus_MitgliederstatusId",
                table: "Benutzer",
                column: "MitgliederstatusId",
                principalTable: "Mitgliederstatus",
                principalColumn: "MitgliederstatusId");
        }
    }
}
