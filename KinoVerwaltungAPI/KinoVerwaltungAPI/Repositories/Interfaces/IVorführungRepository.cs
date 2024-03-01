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

        //Get Programm für heute abhängig von Kino und Saal
        Task<IEnumerable<VorführungDto>> GetProgrammFürHeuteAsync(int kinoId, int saalId);

        //Get Programm für aktuelle Woche abhängig von Kino und Saal
        Task<IEnumerable<VorführungDto>> GetProgrammFürAktuelleWocheAsync(int kinoId, int saalId);

        //Get Alle gültigen Vorführungen für einen kino und Saal
        Task<IEnumerable<VorführungDto>> GetVorführungenFürKinoUndSaalAsync(int kinoId, int saalId);

        Task<VorführungSaalDto> GetVorführungSitzeAsync(int vorführungId);
    }
}
