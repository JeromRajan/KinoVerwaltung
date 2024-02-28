using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Dtos;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface IKinoRepository
    {
        //Kino-Methoden
        #region Kino
        Task<Kino> GetByIdAsync(int id);
        Task<IEnumerable<Kino>> GetAllAsync();
        Task AddAsync(Kino kino);
        Task UpdateAsync(Kino kino);
        Task DeleteAsync(int id);

        #endregion

        // Spezifische Methoden für Säle
        #region Säle
        Task AddSaalMitReihenUndSitzenAsync(int kinoId, Saal saal, int anzahlReihen, int anzahlSitzeProReihe);
        Task UpdateSaalAsync(int saalId, Saal aktualisierterSaal);
        Task DeleteSaalAsync(int saalId);

        Task UpdateSaalMitReihenUndSitzenAsync(int saalId, SaalDto saalDto);
        Task<KinoMitSaelenDto> GetKinoMitSaelenAsync(int kinoId);
        #endregion
    }
}
