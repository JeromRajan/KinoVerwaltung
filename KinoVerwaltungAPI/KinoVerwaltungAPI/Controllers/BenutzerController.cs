using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KinoVerwaltungAPI.Models; 
using KinoVerwaltungAPI.Repositories;
using KinoVerwaltungAPI.Dtos;

namespace KinoVerwaltungAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BenutzerController : ControllerBase
    {
        private readonly IBenutzerRepository _benutzerRepository;

        public BenutzerController(IBenutzerRepository benutzerRepository)
        {
            _benutzerRepository = benutzerRepository;
        }

        //Benutzer-Endpunkte
        #region Benutzer
        [HttpPost("registrieren")]
        public async Task<IActionResult> Registrieren([FromBody] BenutzerRegistrierungDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adresse = new Adresse
            {
                Strasse = dto.Adresse.Strasse,
                Hausnummer = dto.Adresse.Hausnummer,
                PLZ = dto.Adresse.PLZ,
                Stadt = dto.Adresse.Stadt,
                Land = dto.Adresse.Land
            };

            var benutzer = new Benutzer
            {
                Email = dto.Email,
                Vorname = dto.Vorname,
                Nachname = dto.Nachname,
                Telefon = dto.Telefon,
                RolleId = dto.RolleId,
                Adresse = adresse
            };

            try
            {
                await _benutzerRepository.RegistrierenAsync(benutzer, dto.Passwort, adresse, dto.MitgliederkarteIdentifikationsNummer );
                // Passwort und AdresseId sollten nicht zurückgegeben werden
                return CreatedAtAction(nameof(Registrieren), new { id = benutzer.BenutzerId }, benutzer);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Die Mitgliederkarte existiert nicht." || ex.Message == "Die Mitgliederkarte wurde bereits verwendet." || ex.Message == "Die E-Mail-Adresse wird bereits verwendet.")
                {
                    return BadRequest(ex.Message);
                }
       
                return BadRequest("Ein unbekannter Fehler ist aufgetreten.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] BenutzerLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var benutzer = await _benutzerRepository.LoginAsync(dto.Email, dto.Passwort);

            if (benutzer == null)
            {
                return Unauthorized("Ungültige Anmeldeinformationen");
            }

            // PasswortHash oder andere sensible Informationen sollten nicht zurückgegeben werden
            benutzer.Passwort = null;
            return Ok(benutzer);
        }

        [HttpPut("aktualisieren/{benutzerId}")]
        public async Task<IActionResult> UpdateBenutzer(int benutzerId, [FromBody] BenutzerUpdateDto dto)
        {
            var benutzer = await _benutzerRepository.GetBenutzerDatenAsync(benutzerId);

            if (benutzer == null)
            {
                return NotFound();
            }

            // Aktualisieren der Benutzerdaten
            benutzer.Vorname = dto.Vorname;
            benutzer.Nachname = dto.Nachname;
            benutzer.Telefon = dto.Telefon;

            // Aktualisieren der Adresse
            var adresse = new AdresseDto
            {
                Strasse = dto.Adresse.Strasse,
                Hausnummer = dto.Adresse.Hausnummer,
                PLZ = dto.Adresse.PLZ,
                Stadt = dto.Adresse.Stadt,
                Land = dto.Adresse.Land
            };

            try
            {
                await _benutzerRepository.UpdateBenutzerAsync(benutzer, adresse);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        //Rollen-Endpunkte
        #region Rollen
        [HttpPost("rollen")]
        public async Task<IActionResult> AddRolle([FromBody] Rolle rolle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _benutzerRepository.AddRolleAsync(rolle);
            return Ok(rolle);
        }
        #endregion
    }
}
