using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KinoVerwaltungAPI.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementierung der Film-Methoden
        #region Film

        public async Task<IEnumerable<FilmDTO>> GetAllFilmeAsync()
        {
            return await _context.Filme.Select(film => new FilmDTO
            {
                FilmId = film.FilmId,
                Titel = film.Titel,
                Dauer = film.Dauer,
                Beschreibung = film.Beschreibung,
                Altersfreigabe = film.Altersfreigabe,
                GenreName = film.Genre.Name,
                SpracheName = film.Sprache.Name
            }).ToListAsync();
        }

        public async Task<Film> GetFilmByIdAsync(int filmId)
        {
            return await _context.Filme
                .Include(film => film.Genre)
                .Include(film => film.Sprache)
                .FirstOrDefaultAsync(film => film.FilmId == filmId);
        }

        public async Task AddFilmAsync(Film film)
        {
            _context.Filme.Add(film);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFilmAsync(Film film)
        {
            _context.Filme.Update(film);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFilmAsync(int filmId)
        {
            var film = await _context.Filme.FindAsync(filmId);
            //Überprüfen ob der Film existiert
            if (film == null)
            {
                throw new Exception("Film nicht gefunden.");
            }

            // Überprüfen, ob der Film zugehörige Vorführungen hat
            var hatVorfuehrungen = await _context.Vorführungen.AnyAsync(v => v.FilmId == filmId);
            if (hatVorfuehrungen)
            {
                throw new Exception("Der Film kann nicht gelöscht werden, da zugehörige Vorführungen existieren.");
            }

            _context.Filme.Remove(film);
            await _context.SaveChangesAsync();
        }

        #endregion

        // Implementierung der Genre-Methoden
        #region Genre
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task AddGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        // Implementierung der Sprachen-Methoden
        #region Sprachen
        public async Task<IEnumerable<Sprache>> GetAllSprachenAsync()
        {
            return await _context.Sprachen.ToListAsync();
        }

        public async Task AddSpracheAsync(Sprache sprache)
        {
            _context.Sprachen.Add(sprache);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpracheAsync(Sprache sprache)
        {
            _context.Sprachen.Update(sprache);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpracheAsync(int spracheId)
        {
            var sprache = await _context.Sprachen.FindAsync(spracheId);
            if (sprache != null)
            {
                _context.Sprachen.Remove(sprache);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
