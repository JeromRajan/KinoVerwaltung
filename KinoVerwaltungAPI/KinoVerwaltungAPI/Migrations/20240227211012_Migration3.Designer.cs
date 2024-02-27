﻿// <auto-generated />
using System;
using KinoVerwaltungAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KinoVerwaltungAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240227211012_Migration3")]
    partial class Migration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Adresse", b =>
                {
                    b.Property<int>("AdresseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdresseId"));

                    b.Property<string>("Hausnummer")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Land")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PLZ")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Stadt")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Strasse")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("AdresseId");

                    b.ToTable("Adressen");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Benutzer", b =>
                {
                    b.Property<int>("BenutzerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BenutzerId"));

                    b.Property<int>("AdresseId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MitgliederstatusId")
                        .HasColumnType("int");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passwort")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolleId")
                        .HasColumnType("int");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BenutzerId");

                    b.HasIndex("AdresseId");

                    b.HasIndex("MitgliederstatusId");

                    b.HasIndex("RolleId");

                    b.ToTable("Benutzer");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FilmId"));

                    b.Property<string>("Altersfreigabe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Beschreibung")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dauer")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("SpracheId")
                        .HasColumnType("int");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FilmId");

                    b.HasIndex("GenreId");

                    b.HasIndex("SpracheId");

                    b.ToTable("Filme");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Kino", b =>
                {
                    b.Property<int>("KinoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KinoId"));

                    b.Property<int>("AdressId")
                        .HasColumnType("int");

                    b.Property<int>("AdresseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KinoId");

                    b.HasIndex("AdresseId");

                    b.ToTable("Kinos");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Mitgliederkarte", b =>
                {
                    b.Property<int>("MitgliederkarteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MitgliederkarteId"));

                    b.Property<DateTime?>("Ablaufdatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("BenutzerId")
                        .HasColumnType("int");

                    b.Property<string>("IdentifikationsNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MitgliederstatusId")
                        .HasColumnType("int");

                    b.Property<decimal?>("VerfügbareBetrag")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ZahlungsmethodeId")
                        .HasColumnType("int");

                    b.HasKey("MitgliederkarteId");

                    b.HasIndex("BenutzerId")
                        .IsUnique();

                    b.HasIndex("MitgliederstatusId");

                    b.HasIndex("ZahlungsmethodeId");

                    b.ToTable("Mitgliederkarten");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Mitgliederstatus", b =>
                {
                    b.Property<int>("MitgliederstatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MitgliederstatusId"));

                    b.Property<decimal?>("Rabatt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MitgliederstatusId");

                    b.ToTable("Mitgliederstatus");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Reihe", b =>
                {
                    b.Property<int>("ReiheId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReiheId"));

                    b.Property<int>("Nummer")
                        .HasColumnType("int");

                    b.Property<int>("SaalId")
                        .HasColumnType("int");

                    b.HasKey("ReiheId");

                    b.HasIndex("SaalId");

                    b.ToTable("Reihen");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Rolle", b =>
                {
                    b.Property<int>("RolleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolleId"));

                    b.Property<int?>("BenutzerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RolleId");

                    b.HasIndex("BenutzerId");

                    b.ToTable("Rollen");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Saal", b =>
                {
                    b.Property<int>("SaalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaalId"));

                    b.Property<int>("KinoId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Nummer")
                        .HasColumnType("int");

                    b.HasKey("SaalId");

                    b.HasIndex("KinoId");

                    b.ToTable("Saele");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Sitz", b =>
                {
                    b.Property<int>("SitzId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SitzId"));

                    b.Property<int>("Nummer")
                        .HasColumnType("int");

                    b.Property<int>("ReiheId")
                        .HasColumnType("int");

                    b.HasKey("SitzId");

                    b.HasIndex("ReiheId");

                    b.ToTable("Sitze");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Sprache", b =>
                {
                    b.Property<int>("SpracheId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpracheId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpracheId");

                    b.ToTable("Sprachen");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<int>("BenutzerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Preis")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SitzId")
                        .HasColumnType("int");

                    b.Property<int>("VorführungId")
                        .HasColumnType("int");

                    b.Property<int>("ZahlungsmethodeId")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("BenutzerId");

                    b.HasIndex("SitzId");

                    b.HasIndex("VorführungId");

                    b.HasIndex("ZahlungsmethodeId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Vorführung", b =>
                {
                    b.Property<int>("VorführungId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VorführungId"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EndZeit")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmId")
                        .HasColumnType("int");

                    b.Property<decimal>("Preis")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SaalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartZeit")
                        .HasColumnType("datetime2");

                    b.HasKey("VorführungId");

                    b.HasIndex("FilmId");

                    b.HasIndex("SaalId");

                    b.ToTable("Vorführungen");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Zahlungsmethode", b =>
                {
                    b.Property<int>("ZahlungsmethodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZahlungsmethodeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ZahlungsmethodeId");

                    b.ToTable("Zahlungsmethoden");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Benutzer", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Mitgliederstatus", null)
                        .WithMany("Benutzer")
                        .HasForeignKey("MitgliederstatusId");

                    b.HasOne("KinoVerwaltungAPI.Models.Rolle", "Rolle")
                        .WithMany()
                        .HasForeignKey("RolleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adresse");

                    b.Navigation("Rolle");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Film", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Genre", "Genre")
                        .WithMany("Filme")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Sprache", "Sprache")
                        .WithMany("Filme")
                        .HasForeignKey("SpracheId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Sprache");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Kino", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Adresse", "Adresse")
                        .WithMany("Kinos")
                        .HasForeignKey("AdresseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adresse");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Mitgliederkarte", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Benutzer", "Benutzer")
                        .WithOne("Mitgliederkarte")
                        .HasForeignKey("KinoVerwaltungAPI.Models.Mitgliederkarte", "BenutzerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Mitgliederstatus", "Mitgliederstatus")
                        .WithMany()
                        .HasForeignKey("MitgliederstatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Zahlungsmethode", "Zahlungsmethode")
                        .WithMany("Mitgliederkarten")
                        .HasForeignKey("ZahlungsmethodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Benutzer");

                    b.Navigation("Mitgliederstatus");

                    b.Navigation("Zahlungsmethode");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Reihe", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Saal", "Saal")
                        .WithMany("Reihen")
                        .HasForeignKey("SaalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Saal");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Rolle", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Benutzer", null)
                        .WithMany("Rollen")
                        .HasForeignKey("BenutzerId");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Saal", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Kino", "Kino")
                        .WithMany("Saele")
                        .HasForeignKey("KinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kino");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Sitz", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Reihe", "Reihe")
                        .WithMany("Sitze")
                        .HasForeignKey("ReiheId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reihe");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Ticket", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Benutzer", "Benutzer")
                        .WithMany("Tickets")
                        .HasForeignKey("BenutzerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Sitz", "Sitz")
                        .WithMany()
                        .HasForeignKey("SitzId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Vorführung", "Vorführung")
                        .WithMany("Tickets")
                        .HasForeignKey("VorführungId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Zahlungsmethode", "Zahlungsmethode")
                        .WithMany("Tickets")
                        .HasForeignKey("ZahlungsmethodeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Benutzer");

                    b.Navigation("Sitz");

                    b.Navigation("Vorführung");

                    b.Navigation("Zahlungsmethode");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Vorführung", b =>
                {
                    b.HasOne("KinoVerwaltungAPI.Models.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KinoVerwaltungAPI.Models.Saal", "Saal")
                        .WithMany()
                        .HasForeignKey("SaalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("Saal");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Adresse", b =>
                {
                    b.Navigation("Kinos");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Benutzer", b =>
                {
                    b.Navigation("Mitgliederkarte")
                        .IsRequired();

                    b.Navigation("Rollen");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Genre", b =>
                {
                    b.Navigation("Filme");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Kino", b =>
                {
                    b.Navigation("Saele");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Mitgliederstatus", b =>
                {
                    b.Navigation("Benutzer");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Reihe", b =>
                {
                    b.Navigation("Sitze");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Saal", b =>
                {
                    b.Navigation("Reihen");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Sprache", b =>
                {
                    b.Navigation("Filme");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Vorführung", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("KinoVerwaltungAPI.Models.Zahlungsmethode", b =>
                {
                    b.Navigation("Mitgliederkarten");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
