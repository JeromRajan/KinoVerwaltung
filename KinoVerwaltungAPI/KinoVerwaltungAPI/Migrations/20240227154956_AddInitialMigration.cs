using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinoVerwaltungAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adressen",
                columns: table => new
                {
                    AdresseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Strasse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Hausnummer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PLZ = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Stadt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Land = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adressen", x => x.AdresseId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Mitgliederstatus",
                columns: table => new
                {
                    MitgliederstatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rabatt = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitgliederstatus", x => x.MitgliederstatusId);
                });

            migrationBuilder.CreateTable(
                name: "Sprachen",
                columns: table => new
                {
                    SpracheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprachen", x => x.SpracheId);
                });

            migrationBuilder.CreateTable(
                name: "Zahlungsmethoden",
                columns: table => new
                {
                    ZahlungsmethodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahlungsmethoden", x => x.ZahlungsmethodeId);
                });

            migrationBuilder.CreateTable(
                name: "Kinos",
                columns: table => new
                {
                    KinoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdressId = table.Column<int>(type: "int", nullable: false),
                    AdresseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinos", x => x.KinoId);
                    table.ForeignKey(
                        name: "FK_Kinos_Adressen_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adressen",
                        principalColumn: "AdresseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dauer = table.Column<int>(type: "int", nullable: false),
                    Beschreibung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Altersfreigabe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    SpracheId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.FilmId);
                    table.ForeignKey(
                        name: "FK_Filme_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filme_Sprachen_SpracheId",
                        column: x => x.SpracheId,
                        principalTable: "Sprachen",
                        principalColumn: "SpracheId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Saele",
                columns: table => new
                {
                    SaalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nummer = table.Column<int>(type: "int", nullable: true),
                    KinoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saele", x => x.SaalId);
                    table.ForeignKey(
                        name: "FK_Saele_Kinos_KinoId",
                        column: x => x.KinoId,
                        principalTable: "Kinos",
                        principalColumn: "KinoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reihen",
                columns: table => new
                {
                    ReiheId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nummer = table.Column<int>(type: "int", nullable: false),
                    SaalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reihen", x => x.ReiheId);
                    table.ForeignKey(
                        name: "FK_Reihen_Saele_SaalId",
                        column: x => x.SaalId,
                        principalTable: "Saele",
                        principalColumn: "SaalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vorführungen",
                columns: table => new
                {
                    VorführungId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Preis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartZeit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndZeit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SaalId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vorführungen", x => x.VorführungId);
                    table.ForeignKey(
                        name: "FK_Vorführungen_Filme_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Filme",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vorführungen_Saele_SaalId",
                        column: x => x.SaalId,
                        principalTable: "Saele",
                        principalColumn: "SaalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sitze",
                columns: table => new
                {
                    SitzId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nummer = table.Column<int>(type: "int", nullable: true),
                    ReiheId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sitze", x => x.SitzId);
                    table.ForeignKey(
                        name: "FK_Sitze_Reihen_ReiheId",
                        column: x => x.ReiheId,
                        principalTable: "Reihen",
                        principalColumn: "ReiheId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Benutzer",
                columns: table => new
                {
                    BenutzerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passwort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresseId = table.Column<int>(type: "int", nullable: false),
                    RolleId = table.Column<int>(type: "int", nullable: false),
                    MitgliederstatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benutzer", x => x.BenutzerId);
                    table.ForeignKey(
                        name: "FK_Benutzer_Adressen_AdresseId",
                        column: x => x.AdresseId,
                        principalTable: "Adressen",
                        principalColumn: "AdresseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Benutzer_Mitgliederstatus_MitgliederstatusId",
                        column: x => x.MitgliederstatusId,
                        principalTable: "Mitgliederstatus",
                        principalColumn: "MitgliederstatusId");
                });

            migrationBuilder.CreateTable(
                name: "Mitgliederkarten",
                columns: table => new
                {
                    MitgliederkarteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VerfügbareBetrag = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Ablaufdatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdentifikationsNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenutzerId = table.Column<int>(type: "int", nullable: false),
                    MitgliederstatusId = table.Column<int>(type: "int", nullable: false),
                    ZahlungsmethodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mitgliederkarten", x => x.MitgliederkarteId);
                    table.ForeignKey(
                        name: "FK_Mitgliederkarten_Benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "BenutzerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mitgliederkarten_Mitgliederstatus_MitgliederstatusId",
                        column: x => x.MitgliederstatusId,
                        principalTable: "Mitgliederstatus",
                        principalColumn: "MitgliederstatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mitgliederkarten_Zahlungsmethoden_ZahlungsmethodeId",
                        column: x => x.ZahlungsmethodeId,
                        principalTable: "Zahlungsmethoden",
                        principalColumn: "ZahlungsmethodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rollen",
                columns: table => new
                {
                    RolleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BenutzerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rollen", x => x.RolleId);
                    table.ForeignKey(
                        name: "FK_Rollen_Benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "BenutzerId");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preis = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VorführungId = table.Column<int>(type: "int", nullable: false),
                    SitzId = table.Column<int>(type: "int", nullable: false),
                    BenutzerId = table.Column<int>(type: "int", nullable: false),
                    ZahlungsmethodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "BenutzerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Sitze_SitzId",
                        column: x => x.SitzId,
                        principalTable: "Sitze",
                        principalColumn: "SitzId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Vorführungen_VorführungId",
                        column: x => x.VorführungId,
                        principalTable: "Vorführungen",
                        principalColumn: "VorführungId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Zahlungsmethoden_ZahlungsmethodeId",
                        column: x => x.ZahlungsmethodeId,
                        principalTable: "Zahlungsmethoden",
                        principalColumn: "ZahlungsmethodeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benutzer_AdresseId",
                table: "Benutzer",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Benutzer_MitgliederstatusId",
                table: "Benutzer",
                column: "MitgliederstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Benutzer_RolleId",
                table: "Benutzer",
                column: "RolleId");

            migrationBuilder.CreateIndex(
                name: "IX_Filme_GenreId",
                table: "Filme",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Filme_SpracheId",
                table: "Filme",
                column: "SpracheId");

            migrationBuilder.CreateIndex(
                name: "IX_Kinos_AdresseId",
                table: "Kinos",
                column: "AdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_Mitgliederkarten_BenutzerId",
                table: "Mitgliederkarten",
                column: "BenutzerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mitgliederkarten_MitgliederstatusId",
                table: "Mitgliederkarten",
                column: "MitgliederstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Mitgliederkarten_ZahlungsmethodeId",
                table: "Mitgliederkarten",
                column: "ZahlungsmethodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reihen_SaalId",
                table: "Reihen",
                column: "SaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rollen_BenutzerId",
                table: "Rollen",
                column: "BenutzerId");

            migrationBuilder.CreateIndex(
                name: "IX_Saele_KinoId",
                table: "Saele",
                column: "KinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sitze_ReiheId",
                table: "Sitze",
                column: "ReiheId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BenutzerId",
                table: "Tickets",
                column: "BenutzerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SitzId",
                table: "Tickets",
                column: "SitzId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VorführungId",
                table: "Tickets",
                column: "VorführungId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ZahlungsmethodeId",
                table: "Tickets",
                column: "ZahlungsmethodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorführungen_FilmId",
                table: "Vorführungen",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Vorführungen_SaalId",
                table: "Vorführungen",
                column: "SaalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Benutzer_Rollen_RolleId",
                table: "Benutzer",
                column: "RolleId",
                principalTable: "Rollen",
                principalColumn: "RolleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benutzer_Adressen_AdresseId",
                table: "Benutzer");

            migrationBuilder.DropForeignKey(
                name: "FK_Benutzer_Mitgliederstatus_MitgliederstatusId",
                table: "Benutzer");

            migrationBuilder.DropForeignKey(
                name: "FK_Benutzer_Rollen_RolleId",
                table: "Benutzer");

            migrationBuilder.DropTable(
                name: "Mitgliederkarten");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Sitze");

            migrationBuilder.DropTable(
                name: "Vorführungen");

            migrationBuilder.DropTable(
                name: "Zahlungsmethoden");

            migrationBuilder.DropTable(
                name: "Reihen");

            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Saele");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Sprachen");

            migrationBuilder.DropTable(
                name: "Kinos");

            migrationBuilder.DropTable(
                name: "Adressen");

            migrationBuilder.DropTable(
                name: "Mitgliederstatus");

            migrationBuilder.DropTable(
                name: "Rollen");

            migrationBuilder.DropTable(
                name: "Benutzer");
        }
    }
}
