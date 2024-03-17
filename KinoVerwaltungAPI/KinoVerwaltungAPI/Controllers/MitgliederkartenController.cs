using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MitgliederkartenController : ControllerBase
    {
        private readonly IMitgliederkarteRepository _mitgliederkarteRepository;

        public MitgliederkartenController(IMitgliederkarteRepository mitgliederkarteRepository)
        {
            _mitgliederkarteRepository = mitgliederkarteRepository;
        }

        //Mitgliederkarten-Endpunkte
        #region Mitgliederkarte

        [HttpPost]
        public async Task<ActionResult<Mitgliederkarte>> AddMitgliederkarte()
        {
            var neueMitgliederkarte = await _mitgliederkarteRepository.AddMitgliederkarteAsync();
            return CreatedAtAction(nameof(AddMitgliederkarte), new { id = neueMitgliederkarte.MitgliederkarteId }, neueMitgliederkarte);
        }

        [HttpPost("{identifikationsNummer}/aufladen/{betrag}")]
        public async Task<IActionResult> Aufladen(string identifikationsNummer, decimal betrag)
        {
            try
            {
                await _mitgliederkarteRepository.AufladenAsync(identifikationsNummer, betrag);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{benutzerId}/betrag")]
        public async Task<ActionResult<Decimal>> GetMitgliederBetragByBenutzerId(int benutzerId)
        {
            var betrag = await _mitgliederkarteRepository.GetMitgliederBetragByBenutzerIdAsync(benutzerId);
            return Ok(betrag);
        }

        [HttpGet("{benutzerId}")]
        public async Task<ActionResult<Mitgliederkarte>> GetMitgliederkarteByBenutzerId(int benutzerId)
        {
            var mitgliederkarte = await _mitgliederkarteRepository.GetMitgliederkarteByBenutzerIdAsync(benutzerId);
            return Ok(mitgliederkarte);
        }

        #endregion

        //Mitgliederstatus-Endpunkte
        #region Mitgliederstatus
        [HttpPost("mitgliederstatus")]
        public async Task<IActionResult> AddMitgliederstatus([FromBody] Mitgliederstatus mitgliederstatus)
        {
            try
            {
                var hinzugefügterMitgliederstatus = await _mitgliederkarteRepository.AddMitgliederstatus(mitgliederstatus);
                return CreatedAtAction(nameof(AddMitgliederstatus), new { id = hinzugefügterMitgliederstatus.MitgliederstatusId }, hinzugefügterMitgliederstatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
