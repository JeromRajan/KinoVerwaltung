using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicketAsync(int benutzerId, int vorführungId, int sitzId, int zahlungsmethodeId);
        Task<IEnumerable<TicketByUserDto>> GetTicketsByBenutzerIdAsync(int benutzerId);

    }
}
