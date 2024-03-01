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
                    FilmSprache = sprache.Name
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
                    FilmSprache = sprache.Name
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
                    FilmSprache = sprache.Name
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
