using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Repositories
{
    public class VorführungRepository : IVorführungRepository
    {
        private readonly ApplicationDbContext _context;

        public VorführungRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementierung der Methoden für Vorführungen
        #region Vorführungen
        public async Task<IEnumerable<Vorführung>> GetAllVorführungenAsync()
        {
            return await _context.Vorführungen.ToListAsync();
        }

        public async Task<Vorführung> GetVorführungByIdAsync(int vorführungId)
        {
            return await _context.Vorführungen.FindAsync(vorführungId);
        }

        public async Task AddVorführungAsync(Vorführung vorführung)
        {
            //Film holen
            var film = await _context.Filme.FindAsync(vorführung.FilmId);

            //Endzeit nach der Film Dauer setzen
            vorführung.EndZeit = vorführung.StartZeit.AddMinutes(film.Dauer);

            // Überprüfen Sie, ob bereits eine Vorführung im gewählten Saal zur gewünschten Zeit existiert
            var konfliktVorfuehrung = await _context.Vorführungen
                .AnyAsync(v => v.SaalId == vorführung.SaalId &&
                               ((v.StartZeit < vorführung.EndZeit && v.EndZeit > vorführung.StartZeit)));

            if (konfliktVorfuehrung)
            {
                // Wirf eine Ausnahme oder gebe einen Fehler zurück
                throw new Exception("Der Saal ist zur gewählten Start- und Endzeit nicht verfügbar.");
            }
            else
            {
                _context.Vorführungen.Add(vorführung);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateVorführungAsync(Vorführung vorführung)
        {
            //Film holen
            var film = await _context.Filme.FindAsync(vorführung.FilmId);
            if (film == null)
            {
                throw new Exception("Film nicht gefunden.");
            }

            //Endzeit nach der Film Dauer setzen
            vorführung.EndZeit = vorführung.StartZeit.AddMinutes(film.Dauer);


            // Überprüfen Sie, ob bereits eine Vorführung im gewählten Saal zur gewünschten Zeit existiert
            var konfliktVorfuehrung = await _context.Vorführungen
                .AnyAsync(v => v.SaalId == vorführung.SaalId &&
                               ((v.StartZeit < vorführung.EndZeit && v.EndZeit > vorführung.StartZeit)));
            if (konfliktVorfuehrung)
            {
                // Wirf eine Ausnahme oder gebe einen Fehler zurück
                throw new Exception("Der Saal ist zur gewählten Start- und Endzeit nicht verfügbar.");
            }

            _context.Vorführungen.Update(vorführung);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVorführungAsync(int vorführungId)
        {
            var vorführung = await _context.Vorführungen.FindAsync(vorführungId);
            if (vorführung != null)
            {
                _context.Vorführungen.Remove(vorführung);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VorführungDto>> GetVorführungenFürKinoUndSaalAsync(int kinoId, int saalId)
        {
            List<VorführungDto> vorführungenDto = new List<VorführungDto>();
            // Lade alle Vorführungen
            var vorführungen = await _context.Vorführungen
                .Include(v => v.Film)
                .Where(v => v.SaalId == saalId && v.Saal.KinoId == kinoId)
                .ToListAsync();
            
            if (vorführungen == null)
            {
            throw new Exception("Keine Vorführungen gefunden.");
            }

            foreach (var vorführung in vorführungen)
            {
                //Film holen
                var film = await _context.Filme.FindAsync(vorführung.FilmId);

                //Genre holen
                var genre = await _context.Genres.FindAsync(film.GenreId);

                //Sprache holen
                var sprache = await _context.Sprachen.FindAsync(film.SpracheId);

                //Saal holen
                var saal = await _context.Saele.FindAsync(vorführung.SaalId);

                //Reihen holen
                var reihen = await _context.Reihen.Where(r => r.SaalId == saal.SaalId).ToListAsync();

                int anzahlSitzplaetze = 0;
                int anzahlReservierungen = 0;
                int anzahlFreieSitzplaetze = 0;

                //Sitze holen
                foreach (var reihe in reihen)
                {
                    var sitze = await _context.Sitze.Where(s => s.ReiheId == reihe.ReiheId).ToListAsync();
                    anzahlSitzplaetze += sitze.Count;
                }

                //Reservierungen holen
                var reservierungen = await _context.Tickets.Where(t => t.VorführungId == vorführung.VorführungId).ToListAsync();
                anzahlReservierungen = reservierungen.Count;

                //Freie Sitzplätze berechnen
                anzahlFreieSitzplaetze = anzahlSitzplaetze - anzahlReservierungen;

               //VorführungDto hinzufügen
               var vorführungDto = new VorführungDto()
               {
                    VorführungId = vorführung.VorführungId,
                    SaalId = vorführung.SaalId,
                    Datum = vorführung.StartZeit.Date,
                    StartZeit = vorführung.StartZeit,
                    Preis = vorführung.Preis,
                    FilmId = vorführung.FilmId,
                    FilmTitel = film.Titel,
                    FilmBeschreibung = film.Beschreibung,
                    FilmGenre = genre.Name,
                    FilmDauer = film.Dauer,
                    FilmFSK = film.Altersfreigabe,
                    FilmSprache = sprache.Name,
                    AnzahlSitzplaetze = anzahlSitzplaetze,
                    AnzahlReservierungen = anzahlReservierungen,
                    AnzahlFreieSitzplaetze = anzahlFreieSitzplaetze
                };
                vorführungenDto.Add(vorführungDto);
            }

            return vorführungenDto;
        }

        #endregion

        // Implementierung der Methoden für das Programm
        #region Programm

        public async Task<IEnumerable<Vorführung>> GetProgrammFürAktuelleWocheAsync()
        {
            var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);
            return await _context.Vorführungen
                .Where(v => v.StartZeit >= startOfWeek && v.StartZeit < endOfWeek)
            .ToListAsync();
        }

        public async Task<IEnumerable<Vorführung>> GetProgrammFürHeuteAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return await _context.Vorführungen
                .Where(v => v.StartZeit >= today && v.StartZeit < tomorrow)
                .ToListAsync();
        }

        public async Task<IEnumerable<VorführungDto>> GetProgrammFürHeuteAsync(int kinoId, int saalId)
        {
        
            List<VorführungDto> vorführungenDto = new List<VorführungDto>();

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            // Lade alle Vorführungen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.StartZeit >= today && v.StartZeit < tomorrow && v.SaalId == saalId && v.Saal.KinoId == kinoId)
                .ToListAsync();

            if (vorführungen == null)
            {
                throw new Exception("Keine Vorführungen gefunden.");
            }

            foreach (var vorführung in vorführungen)
            {
                //Film holen
                var film = await _context.Filme.FindAsync(vorführung.FilmId);

                //Genre holen
                var genre = await _context.Genres.FindAsync(film.GenreId);

                //Sprache holen
                var sprache = await _context.Sprachen.FindAsync(film.SpracheId);


                //Saal holen
                var saal = await _context.Saele.FindAsync(vorführung.SaalId);

                //Reihen holen
                var reihen = await _context.Reihen.Where(r => r.SaalId == saal.SaalId).ToListAsync();

                int anzahlSitzplaetze = 0;
                int anzahlReservierungen = 0;
                int anzahlFreieSitzplaetze = 0;

                //Sitze holen
                foreach (var reihe in reihen)
                {
                    var sitze = await _context.Sitze.Where(s => s.ReiheId == reihe.ReiheId).ToListAsync();
                    anzahlSitzplaetze += sitze.Count;
                }

                //Reservierungen holen
                var reservierungen = await _context.Tickets.Where(t => t.VorführungId == vorführung.VorführungId).ToListAsync();
                anzahlReservierungen = reservierungen.Count;

                //Freie Sitzplätze berechnen
                anzahlFreieSitzplaetze = anzahlSitzplaetze - anzahlReservierungen;

                //VorführungDto hinzufügen
                var vorführungDto = new VorführungDto()
                {
                    VorführungId = vorführung.VorführungId,
                    SaalId = vorführung.SaalId,
                    Datum = vorführung.StartZeit.Date,
                    StartZeit = vorführung.StartZeit,
                    Preis = vorführung.Preis,
                    FilmTitel = film.Titel,
                    FilmBeschreibung = film.Beschreibung,
                    FilmGenre = genre.Name,
                    FilmDauer = film.Dauer,
                    FilmFSK = film.Altersfreigabe,
                    FilmSprache = sprache.Name,
                    AnzahlSitzplaetze = anzahlSitzplaetze,
                    AnzahlReservierungen = anzahlReservierungen,
                    AnzahlFreieSitzplaetze = anzahlFreieSitzplaetze
                };
                vorführungenDto.Add(vorführungDto);
            }

            return vorführungenDto;
        }

        public async Task<IEnumerable<VorführungDto>> GetProgrammFürAktuelleWocheAsync(int kinoId, int saalId)
        {
            
            List<VorführungDto> vorführungenDto = new List<VorführungDto>();

            var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);

            // Lade alle Vorführungen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.StartZeit >= startOfWeek && v.StartZeit < endOfWeek && v.SaalId == saalId && v.Saal.KinoId == kinoId)
                .ToListAsync();

            if (vorführungen == null)
            {
                throw new Exception("Keine Vorführungen gefunden.");
            }

            foreach (var vorführung in vorführungen)
            {
                //Film holen
                var film = await _context.Filme.FindAsync(vorführung.FilmId);

                //Genre holen
                var genre = await _context.Genres.FindAsync(film.GenreId);

                //Sprache holen
                var sprache = await _context.Sprachen.FindAsync(film.SpracheId);

                //Saal holen
                var saal = await _context.Saele.FindAsync(vorführung.SaalId);

                //Reihen holen
                var reihen = await _context.Reihen.Where(r => r.SaalId == saal.SaalId).ToListAsync();

                int anzahlSitzplaetze = 0;
                int anzahlReservierungen = 0;
                int anzahlFreieSitzplaetze = 0;

                //Sitze holen
                foreach (var reihe in reihen)
                {
                    var sitze = await _context.Sitze.Where(s => s.ReiheId == reihe.ReiheId).ToListAsync();
                    anzahlSitzplaetze += sitze.Count;
                }

                //Reservierungen holen
                var reservierungen = await _context.Tickets.Where(t => t.VorführungId == vorführung.VorführungId).ToListAsync();
                anzahlReservierungen = reservierungen.Count;

                //Freie Sitzplätze berechnen
                anzahlFreieSitzplaetze = anzahlSitzplaetze - anzahlReservierungen;

                //VorführungDto hinzufügen
                var vorführungDto = new VorführungDto()
                {
                    VorführungId = vorführung.VorführungId,
                    SaalId = vorführung.SaalId,
                    Datum = vorführung.StartZeit.Date,
                    StartZeit = vorführung.StartZeit,
                    Preis = vorführung.Preis,
                    FilmTitel = film.Titel,
                    FilmBeschreibung = film.Beschreibung,
                    FilmGenre = genre.Name,
                    FilmDauer = film.Dauer,
                    FilmFSK = film.Altersfreigabe,
                    FilmSprache = sprache.Name,
                    AnzahlSitzplaetze = anzahlSitzplaetze,
                    AnzahlReservierungen = anzahlReservierungen,
                    AnzahlFreieSitzplaetze = anzahlFreieSitzplaetze
                };
                vorführungenDto.Add(vorführungDto);
            }

            return vorführungenDto;
        }

        #endregion

        // Implementierung der Methode Sitzeverfügbarkeit
        #region Sitzeverfügbarkeit

        public async Task<VorführungSaalDto> GetVorführungSitzeAsync(int vorführungId)
        {
            // Lade den Saal und die Reihen und die Sitze, gib jede Reihe mit ihren Sitzen zurück, validiere die Verfügbarkeit der Sitze nach dem Status der Tickets
            var vorführung = await _context.Vorführungen
                .Include(v => v.Saal)
                .Include(v => v.Saal.Reihen)
                .ThenInclude(r => r.Sitze)
                .FirstOrDefaultAsync(v => v.VorführungId == vorführungId);
            if (vorführung == null)
            {
                throw new Exception("Vorführung nicht gefunden.");
            }

            var vorführungSaalDto = new VorführungSaalDto()
            {
                VorführungId = vorführung.VorführungId,
                SaalId = vorführung.SaalId,
                Reihen = new List<VorführungSaalReiheDto>()
            };

            foreach (var reihe in vorführung.Saal.Reihen)
            {
                var reiheDto = new VorführungSaalReiheDto()
                {
                    ReiheId = reihe.ReiheId,
                    ReiheNummer = reihe.Nummer,
                    Sitze = new List<VorführungSaalReiheSitzDto>()
                };

                foreach (var sitz in reihe.Sitze)
                {
                    // Überprüfen, ob der Sitz in der Vorführung besetzt ist
                    var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.SitzId == sitz.SitzId && t.VorführungId == vorführungId);


                    var sitzDto = new VorführungSaalReiheSitzDto()
                    {
                        SitzId = sitz.SitzId,
                        SitzNummer = sitz.Nummer,
                        IstBesetzt = ticket != null
                    };
                    reiheDto.Sitze.Add(sitzDto);
                }
                vorführungSaalDto.Reihen.Add(reiheDto);
            }
            return vorführungSaalDto;
        }
        #endregion
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
