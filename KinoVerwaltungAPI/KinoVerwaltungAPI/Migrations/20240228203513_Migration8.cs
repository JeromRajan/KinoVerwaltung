using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinoVerwaltungAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IstBesetzt",
                table: "Sitze");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IstBesetzt",
                table: "Sitze",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
