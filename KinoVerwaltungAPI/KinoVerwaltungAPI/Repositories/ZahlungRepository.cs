using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Repositories
{
    public class ZahlungRepository : IZahlungsRepository
    {
        private readonly ApplicationDbContext _context;

        public ZahlungRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Zahlungsmethode> AddZahlungsmethodeAsync(Zahlungsmethode zahlungsmethode)
        {
            // Überprüfen, ob die Zahlungsmethode bereits existiert
            var existierendeZahlungsmethode = await _context.Zahlungsmethoden
                .FirstOrDefaultAsync(z => z.Name.ToLower() == zahlungsmethode.Name.ToLower());

            if (existierendeZahlungsmethode != null)
            {
                throw new Exception("Die Zahlungsmethode existiert bereits.");
            }

            _context.Zahlungsmethoden.Add(zahlungsmethode);
            await _context.SaveChangesAsync();
            return zahlungsmethode;
        }
        public async Task<Zahlungsmethode> GetZahlungsmethodeByIdAsync(int id)
        {
            return await _context.Zahlungsmethoden.FindAsync(id);
        }

        public async Task<IEnumerable<Zahlungsmethode>> GetAllZahlungsmethodenAsync()
        {
            return await _context.Zahlungsmethoden.ToListAsync();
        }

    }

}
