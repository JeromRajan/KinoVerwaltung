﻿using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Repositories
{
    public class MitgliederkarteRepository : IMitgliederkarteRepository
    {
        private readonly ApplicationDbContext _context;

        public MitgliederkarteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //Implementierung Mitgliederkarte Methoden
        #region Mitgliederkarte
        public async Task<Mitgliederkarte> AddMitgliederkarteAsync()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(); // Unix-Zeitstempel für Einzigartigkeit
            var randomPart = new Random().Next(100, 999); // Drei zufällige Ziffern für zusätzliche Einzigartigkeit

            //Überprüfen, ob die Identifikationsnummer bereits existiert
            while (await _context.Mitgliederkarten.AnyAsync(m => m.IdentifikationsNummer == $"{timestamp}-{randomPart}"))
            {
                randomPart = new Random().Next(100, 999);
            }   

            var mitgliederkarte = new Mitgliederkarte
            {                 
                IdentifikationsNummer = $"{timestamp}-{randomPart}", // Generiert eine eindeutige Identifikationsnummer
                Ablaufdatum = DateTime.Now.AddYears(1), // Setzt das Ablaufdatum auf ein Jahr ab heute
                MitgliederstatusId = 1 // Setzt den Mitgliederstatus auf "Bronze"
            };

            _context.Mitgliederkarten.Add(mitgliederkarte);
            await _context.SaveChangesAsync();
            return mitgliederkarte;
        }

        public async Task AufladenAsync(string identifikationsNummer, decimal betrag)
        {
            var mitgliederkarte = await _context.Mitgliederkarten.FirstOrDefaultAsync(m => m.IdentifikationsNummer == identifikationsNummer);
            if (mitgliederkarte == null) throw new Exception("Mitgliederkarte nicht gefunden.");

            mitgliederkarte.VerfügbareBetrag += betrag;
            await _context.SaveChangesAsync();
        }

        //Mitglieder Betrag abfragen
        public async Task<Decimal> GetMitgliederBetragByBenutzerIdAsync(int benutzerId)
        {
            var mitgliederkarte = await _context.Mitgliederkarten
                .FirstOrDefaultAsync(m => m.BenutzerId == benutzerId);

            return mitgliederkarte.VerfügbareBetrag;
        }

        //Mitgliederkarte abfragen
        public async Task<Mitgliederkarte> GetMitgliederkarteByBenutzerIdAsync(int benutzerId)
        {
            //Mitgliederkarte mit Mitgliederstatus abfragen
            return await _context.Mitgliederkarten
                .Include(m => m.Mitgliederstatus)
                .FirstOrDefaultAsync(m => m.BenutzerId == benutzerId
                               );
        }

        #endregion

        //Implementierung Mitgliederstatus Methoden
        #region Mitgliederstatus
        public async Task<Mitgliederstatus> AddMitgliederstatus(Mitgliederstatus mitgliederstatus)
        {
            _context.Mitgliederstatus.Add(mitgliederstatus);
            await _context.SaveChangesAsync();
            return mitgliederstatus;
        }
        #endregion
    }

}
