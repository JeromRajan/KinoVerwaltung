using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface IZahlungsRepository
    {
        Task<Zahlungsmethode> AddZahlungsmethodeAsync(Zahlungsmethode zahlungsmethode);
        Task<Zahlungsmethode> GetZahlungsmethodeByIdAsync(int id);
        Task<IEnumerable<Zahlungsmethode>> GetAllZahlungsmethodenAsync();

        //Resevierte Ticket mit Mitgliederkarte bezahlen
        Task<bool> ReservierungBezahlenMitMitgliederkarteAsync(string referenzNummer, string identifikationsNummer);

        //Reservierte Ticket bar bezahlen
        Task<bool> ReservierungBezahlenBarAsync(string referenzNummer);
    }
}
