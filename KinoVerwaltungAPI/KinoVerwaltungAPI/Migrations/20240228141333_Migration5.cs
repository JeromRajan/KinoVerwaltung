using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinoVerwaltungAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mitgliederkarten_Zahlungsmethoden_ZahlungsmethodeId",
                table: "Mitgliederkarten");

            migrationBuilder.DropIndex(
                name: "IX_Mitgliederkarten_BenutzerId",
                table: "Mitgliederkarten");

            migrationBuilder.AlterColumn<int>(
                name: "ZahlungsmethodeId",
                table: "Mitgliederkarten",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BenutzerId",
                table: "Mitgliederkarten",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Mitgliederkarten_BenutzerId",
                table: "Mitgliederkarten",
                column: "BenutzerId",
                unique: true,
                filter: "[BenutzerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Mitgliederkarten_Zahlungsmethoden_ZahlungsmethodeId",
                table: "Mitgliederkarten",
                column: "ZahlungsmethodeId",
                principalTable: "Zahlungsmethoden",
                principalColumn: "ZahlungsmethodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mitgliederkarten_Zahlungsmethoden_ZahlungsmethodeId",
                table: "Mitgliederkarten");

            migrationBuilder.DropIndex(
                name: "IX_Mitgliederkarten_BenutzerId",
                table: "Mitgliederkarten");

            migrationBuilder.AlterColumn<int>(
                name: "ZahlungsmethodeId",
                table: "Mitgliederkarten",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BenutzerId",
                table: "Mitgliederkarten",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mitgliederkarten_BenutzerId",
                table: "Mitgliederkarten",
                column: "BenutzerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mitgliederkarten_Zahlungsmethoden_ZahlungsmethodeId",
                table: "Mitgliederkarten",
                column: "ZahlungsmethodeId",
                principalTable: "Zahlungsmethoden",
                principalColumn: "ZahlungsmethodeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
