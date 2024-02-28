using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface IFilmRepository
    {
        //Film-Methoden
        #region Film
        Task<IEnumerable<FilmDTO>> GetAllFilmeAsync();
        Task<Film> GetFilmByIdAsync(int filmId);
        Task AddFilmAsync(Film film);
        Task UpdateFilmAsync(Film film);
        Task DeleteFilmAsync(int filmId);
        #endregion

        // Genre-Methoden
        #region Genre
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task AddGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int genreId);
        #endregion


        // Sprachen-Methoden
        #region Sprachen
        Task<IEnumerable<Sprache>> GetAllSprachenAsync();
        Task AddSpracheAsync(Sprache sprache);
        Task UpdateSpracheAsync(Sprache sprache);
        Task DeleteSpracheAsync(int spracheId);

        #endregion
    }
}
