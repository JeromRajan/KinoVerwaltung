using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories
{
    public interface IZahlungsmethodeRepository
    {
        Task<Zahlungsmethode> AddZahlungsmethodeAsync(Zahlungsmethode zahlungsmethode);
        Task<Zahlungsmethode> GetZahlungsmethodeByIdAsync(int id);
        Task<IEnumerable<Zahlungsmethode>> GetAllZahlungsmethodenAsync();
    }
}
