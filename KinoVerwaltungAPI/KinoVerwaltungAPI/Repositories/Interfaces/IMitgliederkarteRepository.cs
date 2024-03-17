using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface IMitgliederkarteRepository
    {
        Task<Mitgliederkarte> AddMitgliederkarteAsync();
        Task AufladenAsync(string identifikationsNummer, decimal betrag);

        Task<Mitgliederstatus> AddMitgliederstatus(Mitgliederstatus mitgliederstatus);

        Task<Decimal> GetMitgliederBetragByBenutzerIdAsync(int benutzerId);

        Task<Mitgliederkarte> GetMitgliederkarteByBenutzerIdAsync(int benutzerId);
    }
}
