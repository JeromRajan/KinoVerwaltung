using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories.Interfaces
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

        // Echzeit Informationen über verfügbare Sitze
        Task<VorführungSaalDto> GetVorführungSitzeAsync(int vorführungId);
    }
}
