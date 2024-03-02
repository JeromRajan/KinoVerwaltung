using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VorführungController : ControllerBase
    {
        private readonly IVorführungRepository _VorführungRepository;

        public VorführungController(IVorführungRepository VorführungRepository)
        {
            _VorführungRepository = VorführungRepository;
        }

        //Vorführungen-Endpunkte
        #region Vorführungen
        [HttpGet]
        public async Task<IActionResult> GetAllVorführungen()
        {
            var Vorführungen = await _VorführungRepository.GetAllVorführungenAsync();
            return Ok(Vorführungen);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVorführungById(int id)
        {
            var Vorführung = await _VorführungRepository.GetVorführungByIdAsync(id);
            if (Vorführung == null)
            {
                return NotFound();
            }
            return Ok(Vorführung);
        }

        [HttpPost]
        public async Task<IActionResult> AddVorführung([FromBody] Vorführung vorführung)
        {
            try
            {
                await _VorführungRepository.AddVorführungAsync(vorführung);
                return CreatedAtAction(nameof(GetVorführungById), new { id = vorführung.VorführungId }, vorführung);
            }
            catch (Exception ex)
            {
                // Rückgabe einer Fehlermeldung bei Konflikten
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVorführung(int id, [FromBody] Vorführung Vorführung)
        {
            if (id != Vorführung.VorführungId)
            {
                return BadRequest();
            }

            await _VorführungRepository.UpdateVorführungAsync(Vorführung);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVorführung(int id)
        {
            await _VorführungRepository.DeleteVorführungAsync(id);
            return NoContent();
        }

        [HttpGet("kino/{kinoId}/saal/{saalId}")]
        public async Task<IActionResult> GetVorführungenFürKinoUndSaal(int kinoId, int saalId)
        {
            var vorführungen = await _VorführungRepository.GetVorführungenFürKinoUndSaalAsync(kinoId, saalId);
            return Ok(vorführungen);
        }


        #endregion

        //Programm-Endpunkte
        #region Programm
        [HttpGet("woche")]
        public async Task<IActionResult> GetProgrammFuerAktuelleWoche()
        {
            var programm = await _VorführungRepository.GetProgrammFürAktuelleWocheAsync();
            return Ok(programm);
        }

        [HttpGet("heute")]
        public async Task<IActionResult> GetProgrammFuerHeute()
        {
            var programm = await _VorführungRepository.GetProgrammFürHeuteAsync();
            return Ok(programm);
        }

        [HttpGet("heute/{kinoId}/{saalId}")]
        public async Task<IActionResult> GetProgrammFuerHeute(int kinoId, int saalId)
        {
            var programm = await _VorführungRepository.GetProgrammFürHeuteAsync(kinoId, saalId);
            return Ok(programm);
        }

        [HttpGet("woche/{kinoId}/{saalId}")]
        public async Task<IActionResult> GetProgrammFuerAktuelleWoche(int kinoId, int saalId)
        {
            var programm = await _VorführungRepository.GetProgrammFürAktuelleWocheAsync(kinoId, saalId);
            return Ok(programm);
        }

        #endregion
        //Sitze-Endpunkte
        #region Sitze
        [HttpGet("{id}/sitze")]
        public async Task<IActionResult> GetVorführungSitze(int id)
        {
            var vorführung = await _VorführungRepository.GetVorführungSitzeAsync(id);
            return Ok(vorführung);
        }

        #endregion
    }
}
