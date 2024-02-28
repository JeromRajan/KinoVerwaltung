using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZahlungController : ControllerBase
    {
        private readonly IZahlungsRepository _zahlungsmethodeRepository;

        public ZahlungController(IZahlungsRepository zahlungsmethodeRepository)
        {
            _zahlungsmethodeRepository = zahlungsmethodeRepository;
        }

        //Zahlungsmethode-Endpunkte
        #region Zahlungsmethode
        [HttpPost]
        public async Task<IActionResult> AddZahlungsmethode([FromBody] Zahlungsmethode zahlungsmethode)
        {
            try
            {
                var hinzugefügteZahlungsmethode = await _zahlungsmethodeRepository.AddZahlungsmethodeAsync(zahlungsmethode);
                return CreatedAtAction(nameof(AddZahlungsmethode), new { id = hinzugefügteZahlungsmethode.ZahlungsmethodeId }, hinzugefügteZahlungsmethode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetZahlungsmethodeById(int id)
        {
            var zahlungsmethode = await _zahlungsmethodeRepository.GetZahlungsmethodeByIdAsync(id);
            if (zahlungsmethode == null)
            {
                return NotFound();
            }
            return Ok(zahlungsmethode);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllZahlungsmethoden()
        {
            var zahlungsmethoden = await _zahlungsmethodeRepository.GetAllZahlungsmethodenAsync();
            return Ok(zahlungsmethoden);
        }
        #endregion

    }

}
