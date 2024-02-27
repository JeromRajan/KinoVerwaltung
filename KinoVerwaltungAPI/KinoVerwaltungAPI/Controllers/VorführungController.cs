using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories;
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
        public async Task<IActionResult> AddVorführung([FromBody] Vorführung Vorführung)
        {
            await _VorführungRepository.AddVorführungAsync(Vorführung);
            return CreatedAtAction(nameof(GetVorführungById), new { id = Vorführung.VorführungId }, Vorführung);
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
    }
}
