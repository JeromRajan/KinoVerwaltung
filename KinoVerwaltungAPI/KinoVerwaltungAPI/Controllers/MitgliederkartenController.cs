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

        [HttpPost("{identifikationsNummer}/aufladen")]
        public async Task<IActionResult> Aufladen(string identifikationsNummer, [FromBody] decimal betrag)
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
