using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KinoVerwaltungAPI.Repositories
{
    public class BenutzerRepository : IBenutzerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Benutzer> _passwordHasher;

        public BenutzerRepository(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Benutzer>();
        }

        //Implementierung der Methoden des Interfaces IBenutzerRepository
        #region Benutzer
        public async Task<Benutzer> RegistrierenAsync(Benutzer benutzer, string passwort, Adresse adresse, string MitgliederkarteIdentifikationsNummer)
        {
            //überprüfen, ob die Mitgliederkarte existiert und noch nicht verwendet wurde
            if (!string.IsNullOrEmpty(MitgliederkarteIdentifikationsNummer))
            {
                var mitgliederkarte = await _context.Mitgliederkarten
                    .FirstOrDefaultAsync(m => m.IdentifikationsNummer == MitgliederkarteIdentifikationsNummer);

                if (mitgliederkarte == null)
                {
                    throw new Exception("Die Mitgliederkarte existiert nicht.");
                }
                if (mitgliederkarte.BenutzerId != null)
                {
                    throw new Exception("Die Mitgliederkarte wurde bereits verwendet.");
                }                
            }

            // Überprüfen, ob eine E-Mail-Adresse bereits existiert
            var existierenderBenutzer = await _context.Benutzer
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == benutzer.Email.ToLower());

            if (existierenderBenutzer != null)
            {              
                throw new Exception("Die E-Mail-Adresse wird bereits verwendet.");
            }

            // Passwort hashen
            var passwordHasher = new PasswordHasher<Benutzer>();
            benutzer.Passwort = passwordHasher.HashPassword(benutzer, passwort);

            // Überprüfen, ob eine ähnliche Adresse bereits existiert
            var vorhandeneAdresse = await _context.Adressen
                .AsNoTracking()
                .FirstOrDefaultAsync(a =>
                    a.Strasse == adresse.Strasse &&
                    a.Hausnummer == adresse.Hausnummer &&
                    a.PLZ == adresse.PLZ &&
                    a.Stadt == adresse.Stadt &&
                    a.Land == adresse.Land);

            if (vorhandeneAdresse != null)
            {
                // Verwenden Sie die vorhandene AdresseId, wenn die Adresse bereits existiert
                benutzer.AdresseId = vorhandeneAdresse.AdresseId;
                benutzer.Adresse = null;
            }
            else
            {
                // Fügen Sie die neue Adresse hinzu, wenn sie nicht existiert
                _context.Adressen.Add(adresse);
                await _context.SaveChangesAsync(); // AdresseId wird generiert
                benutzer.AdresseId = adresse.AdresseId;
            }
            
            // Benutzer hinzufügen
            _context.Benutzer.Add(benutzer);
            await _context.SaveChangesAsync();

            // Mitgliederkarte zuweisen
            if (!string.IsNullOrEmpty(MitgliederkarteIdentifikationsNummer))
            {
                var mitgliederkarte = await _context.Mitgliederkarten
                    .FirstOrDefaultAsync(m => m.IdentifikationsNummer == MitgliederkarteIdentifikationsNummer);
                mitgliederkarte.BenutzerId = benutzer.BenutzerId;
                await _context.SaveChangesAsync();
            }

            return benutzer;
        }

        public async Task<Benutzer> LoginAsync(string email, string passwort)
        {
            var benutzer = await _context.Benutzer
                .Include(u => u.Adresse)
                .Include(u => u.Rolle)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (benutzer != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(benutzer, benutzer.Passwort, passwort);
                if (result == PasswordVerificationResult.Success)
                {
                    return benutzer;
                }
            }
            return null;
        }

        public async Task<Benutzer> GetBenutzerDatenAsync(int benutzerId)
        {
            var benutzer = await _context.Benutzer.FindAsync(benutzerId);
            return benutzer;
        }

        public async Task UpdateBenutzerAsync(Benutzer benutzer, AdresseDto neueAdresseDto)
        {
            // Laden der aktuellen Adresse des Benutzers
            var aktuelleAdresse = await _context.Adressen.FindAsync(benutzer.AdresseId);

            if (aktuelleAdresse != null)
            {
                // Überprüfen, ob die aktuelle Adresse ausschließlich diesem Benutzer zugeordnet ist
                bool adresseIstExklusiv = await _context.Benutzer.CountAsync(b => b.AdresseId == aktuelleAdresse.AdresseId) == 1;

                if (adresseIstExklusiv)
                {
                    // Die Adresse aktualisieren, wenn sie exklusiv diesem Benutzer zugeordnet ist
                    aktuelleAdresse.Strasse = neueAdresseDto.Strasse;
                    aktuelleAdresse.Hausnummer = neueAdresseDto.Hausnummer;
                    aktuelleAdresse.PLZ = neueAdresseDto.PLZ;
                    aktuelleAdresse.Stadt = neueAdresseDto.Stadt;
                    aktuelleAdresse.Land = neueAdresseDto.Land;
                }
                else
                {
                    // Optional: Erstellen einer neuen Adresse, wenn die aktuelle Adresse nicht exklusiv ist
                    var neueAdresse = new Adresse
                    {
                        Strasse = neueAdresseDto.Strasse,
                        Hausnummer = neueAdresseDto.Hausnummer,
                        PLZ = neueAdresseDto.PLZ,
                        Stadt = neueAdresseDto.Stadt,
                        Land = neueAdresseDto.Land
                    };
                    _context.Adressen.Add(neueAdresse);
                    await _context.SaveChangesAsync();

                    // Aktualisieren der AdresseId des Benutzers auf die neue Adresse
                    benutzer.AdresseId = neueAdresse.AdresseId;
                }
            }
            
            // Aktualisieren des Benutzers
            _context.Benutzer.Update(benutzer);
            await _context.SaveChangesAsync();
        }
        #endregion

        // Implementierung der Methode für Rollen
        #region Rollen
        public async Task<Rolle> AddRolleAsync(Rolle rolle)
        {
            _context.Rollen.Add(rolle);
            await _context.SaveChangesAsync();
            return rolle;
        }
        #endregion

    }

}
