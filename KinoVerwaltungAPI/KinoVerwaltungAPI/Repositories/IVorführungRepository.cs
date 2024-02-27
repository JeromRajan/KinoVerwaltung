using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories
{
    public interface IVorführungRepository
    {
        Task<IEnumerable<Vorführung>> GetAllVorführungenAsync();
        Task<Vorführung> GetVorführungByIdAsync(int vorfuehrungId);
        Task AddVorführungAsync(Vorführung vorfuehrung);
        Task UpdateVorführungAsync(Vorführung vorfuehrung);
        Task DeleteVorführungAsync(int vorfuehrungId);
        Task<IEnumerable<Vorführung>> GetProgrammFürAktuelleWocheAsync();
        Task<IEnumerable<Vorführung>> GetProgrammFürHeuteAsync();
    }
}
