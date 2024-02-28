using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicketAsync(int benutzerId, int vorführungId, int sitzId, int zahlungsmethodeId);
        Task<IEnumerable<TicketByUserDto>> GetTicketsByBenutzerIdAsync(int benutzerId);

        //Ticket resevierung im Kino bestätigen
        Task<Ticket> ConfirmTicketAsync(string referenzNummer);

        //Ticket in Kino erstellen, dh die zahlungsmethodeId  ist immer 1 (Barzahlung) und das Ticket ist sofort bestätigt
        Task<Ticket> AddTicketInKinoAsync(int benutzerId, int vorführungId, int sitzId);



    }
}
