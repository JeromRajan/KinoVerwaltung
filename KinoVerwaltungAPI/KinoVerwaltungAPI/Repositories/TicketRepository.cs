using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //Ticket Methoden implementierung
        #region Ticket
        public async Task<Ticket> AddTicketAsync(int benutzerId, int vorführungId, int sitzId, int zahlungsmethodeId)
        {
            // Überprüfen, ob der Benutzer existiert
            var benutzer = await _context.Benutzer.FindAsync(benutzerId);
            if (benutzer == null) throw new Exception("Benutzer nicht gefunden.");

            // Überprüfen, ob die Vorführung existiert
            var vorführung = await _context.Vorführungen.FindAsync(vorführungId);
            if (vorführung == null) throw new Exception("Vorführung nicht gefunden.");

            // Überprüfen, ob die Zahlungsmethode existiert
            var zahlungsmethode = await _context.Zahlungsmethoden.FindAsync(zahlungsmethodeId);
            if (zahlungsmethode == null) throw new Exception("Zahlungsmethode nicht gefunden.");

            // Überprüfen, ob der Sitz existiert, ob er frei ist und ob er in der Vorführung existiert
            var sitz = await _context.Sitze.FindAsync(sitzId);
            if (sitz == null) throw new Exception("Sitz nicht gefunden.");

            //Besetze Sitze anhnad der Ticket.sitzId herausfinden, ob der Sitz in der vorstellung besetzt ist
            //Überprüfen, ob es bereits ein Ticket für diesen Sitz und Vorführung gibt
            var ticketCheck = await _context.Tickets.FirstOrDefaultAsync(t => t.SitzId == sitzId && t.VorführungId == vorführungId);
            if (ticketCheck != null) throw new Exception("Sitz ist bereits besetzt.");

            //if (sitz.IstBesetzt) throw new Exception("Sitz ist bereits besetzt.");

            //Mitgliederkarte des Benutzers abfragen
            var mitgliederkarte = await _context.Mitgliederkarten.FirstOrDefaultAsync(m => m.BenutzerId == benutzerId);
            if (mitgliederkarte == null) throw new Exception("Mitgliederkarte nicht gefunden.");

            //Mitgliederstatus der Mitgliederkarte abfragen
            var mitgliederstatus = await _context.Mitgliederstatus.FirstOrDefaultAsync(m => m.MitgliederstatusId ==  mitgliederkarte.MitgliederstatusId);

            //Bronze: Standard bei Erwerb der Karte. - mitgliederstatusId = 1
            //Silber: Ab 5 Kinobesuche innerhalb eines Kalenderjahres. mitgliederstatusId = 2
            //Gold: Ab 10 Kinobesuche innerhalb eines Kalenderjahres. mitgliederstatusId = 3
            //Bronze: CHF 3.00 Rabatt pro Ticket.
            //Silber: CHF 4.00 Rabatt pro Ticket.
            //Gold: CHF 5.00 Rabatt pro Ticket.
            
            //Rabatt berechnen
            decimal rabatt = 0;
            if (mitgliederstatus.MitgliederstatusId == 1)
            {
                rabatt = 3;
            }
            else if (mitgliederstatus.MitgliederstatusId == 2)
            {
                rabatt = 4;
            }
            else if (mitgliederstatus.MitgliederstatusId == 3)
            {
                rabatt = 5;
            }

            //Generiere eine eindeutige Referenznummer
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // Unix-Zeitstempel für Einzigartigkeit
            var randomPart = new Random().Next(100, 999); // Drei zufällige Ziffern für zusätzliche Einzigartigkeit

            //Überprüfen, ob die Referenznummer bereits existiert
            while (await _context.Tickets.AnyAsync(t => t.ReferenzNummer == $"{timestamp}-{randomPart}"))
            {
                randomPart = new Random().Next(100, 999);
            }

            //Ticket erstellen
            var ticket = new Ticket
            {
                BenutzerId = benutzerId,
                VorführungId = vorführungId,
                SitzId = sitzId,
                Preis = vorführung.Preis - rabatt,
                Status = "Reserviert",
                ZahlungsmethodeId = zahlungsmethodeId,
                ReferenzNummer = $"{timestamp}-{randomPart}"
            };

            if (ticket != null) {
                //Die Anzahl der gekauften Tickets der Mitgliederkarte erhöhen
                mitgliederkarte.AnzahlGekaufterTickets++;
                _context.Mitgliederkarten.Update(mitgliederkarte);
                await _context.SaveChangesAsync();

                //Mitgliederstatus aktualisieren
                if (mitgliederkarte.AnzahlGekaufterTickets >= 5 && mitgliederkarte.AnzahlGekaufterTickets < 10)
                {
                    mitgliederkarte.MitgliederstatusId = 2;
                    _context.Mitgliederkarten.Update(mitgliederkarte);
                    await _context.SaveChangesAsync();
                }
                else if (mitgliederkarte.AnzahlGekaufterTickets >= 10)
                {
                    mitgliederkarte.MitgliederstatusId = 3;
                    _context.Mitgliederkarten.Update(mitgliederkarte);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;

        }
        //Alle Tickets eines Benutzers abfragen
        public async Task<IEnumerable<TicketByUserDto>> GetTicketsByBenutzerIdAsync(int benutzerId)
        {
            //Get all tickets of a user
           var ticktes = await _context.Tickets
                .Where(t => t.BenutzerId == benutzerId)
                .ToListAsync();

            List<TicketByUserDto> ticketByUserDtos = new List<TicketByUserDto>();

            foreach (var ticket in ticktes)
            {
                //Vorführung abfragen
                var vorführung = await _context.Vorführungen.FirstOrDefaultAsync(v => v.VorführungId == ticket.VorführungId);
                //Saal abfragen
                var saal = await _context.Saele.FirstOrDefaultAsync(s => s.SaalId == vorführung.SaalId);
                //Film abfragen
                var film = await _context.Filme.FirstOrDefaultAsync(f => f.FilmId == vorführung.FilmId);
                //Kinoname abfragen
                var kino = await _context.Kinos.FirstOrDefaultAsync(k => k.KinoId == saal.KinoId);
                //Zahlungsmethode abfragen
                var zahlungsmethode = await _context.Zahlungsmethoden.FirstOrDefaultAsync(z => z.ZahlungsmethodeId == ticket.ZahlungsmethodeId);
                //Sitz abfragen
                var sitz = await _context.Sitze.FirstOrDefaultAsync(s => s.SitzId == ticket.SitzId);
                //Sitze Reihe abfragen
                var sitzReihe = await _context.Sitze.FirstOrDefaultAsync(s => s.SitzId == ticket.SitzId);

                //TicketByUserDto erstellen
                var ticketByUserDto = new TicketByUserDto
                {
                    TicketId = ticket.TicketId,
                    SitzNummer = sitz.Nummer,
                    FilmTitel = film.Titel,
                    Vorführungsdatum = vorführung.Datum.ToString("dd.MM.yyyy"),
                    Vorführungszeit = vorführung.StartZeit.ToString("HH:mm"),
                    SaalName = saal.Name,
                    Zahlungsmethode = zahlungsmethode.Name,
                    Kinoname = kino.Name,
                    SitzReihe = sitzReihe.Nummer,
                    ReferenzNummer = ticket.ReferenzNummer
                };
                ticketByUserDtos.Add(ticketByUserDto);
            }


           return ticketByUserDtos;

        }

        #endregion

    }
}
