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

        //Zahlungsmethode implementieren
        #region Zahlungsmethode
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
        #endregion

        //Bezahlen implementieren
        #region Bezahlen
        //Reservierte Ticket mit Mitgliederkarte bezahlen
        public async Task<bool> ReservierungBezahlenMitMitgliederkarteAsync(string referenzNummer, string identifikationsNummer)
        {
            // Überprüfen, ob die Reservierung existiert
            var reservierung = await _context.Tickets
                .Where(t => t.ReferenzNummer == referenzNummer && t.Status == "Reserviert")
                .FirstOrDefaultAsync();

           // Überprüfen, ob die Mitgliederkarte existiert
            var mitgliederkarte = await _context.Mitgliederkarten
                .Where(m => m.IdentifikationsNummer == identifikationsNummer)
                .FirstOrDefaultAsync();

            if (reservierung == null || mitgliederkarte == null)
            {
                throw new Exception("Reservierung oder Mitgliederkarte existiert nicht.");
            }

            if (reservierung.Status == "Bezahlt" || reservierung.Status == "Abgeschlossen")
            {
                throw new Exception("Reservierung wurde bereits bezahlt.");
            }

            if (mitgliederkarte.VerfügbareBetrag < reservierung.Preis)
            {
                throw new Exception("Guthaben der Mitgliederkarte reicht nicht aus.");
            }

            mitgliederkarte.VerfügbareBetrag -= reservierung.Preis;
            reservierung.Status = "Bezahlt";

            await _context.SaveChangesAsync();
            return true;
        }

        //Reservierte Ticket bar bezahlen
        public async Task<bool> ReservierungBezahlenBarAsync(string referenzNummer)
        {
            var reservierung = await _context.Tickets
                .Where(t => t.ReferenzNummer == referenzNummer && t.Status == "Reserviert")
                .FirstOrDefaultAsync();

            if (reservierung == null)
            {
                throw new Exception("Reservierung existiert nicht.");
            }

            if (reservierung.Status == "Bezahlt" || reservierung.Status == "Abgeschlossen")
            {
                throw new Exception("Reservierung wurde bereits bezahlt.");
            }

            reservierung.Status = "Abgeschlossen";
            await _context.SaveChangesAsync();
            return true;
        }


        #endregion

    }

}
