using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Services
{
    public interface IKinoRepository
    {
        Task<Kino> GetByIdAsync(int id);
        Task<IEnumerable<Kino>> GetAllAsync();
        Task AddAsync(Kino kino);
        Task UpdateAsync(Kino kino);
        Task DeleteAsync(int id);
        // Spezifische Methoden
        Task<Kino> GetKinoWithSaeleAsync(int kinoId);

        Task AddSaalMitReihenUndSitzenAsync(int kinoId, Saal saal, int anzahlReihen, int anzahlSitzeProReihe);
        Task UpdateSaalAsync(int saalId, Saal aktualisierterSaal);
        Task DeleteSaalAsync(int saalId);
    }
}
