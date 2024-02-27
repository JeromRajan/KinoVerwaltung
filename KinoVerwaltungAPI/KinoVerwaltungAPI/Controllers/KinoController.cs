using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Services;
using Microsoft.AspNetCore.Mvc;
using KinoVerwaltungAPI.Dtos;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KinoController : ControllerBase
    {
        private readonly IKinoRepository _kinoRepository;

        public KinoController(IKinoRepository kinoRepository)
        {
            _kinoRepository = kinoRepository;
        }

        // CRUD-Operationen 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kino>>> GetAllKinos()
        {
            return Ok(await _kinoRepository.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Kino>> AddKino([FromBody] Kino kino)
        {
            await _kinoRepository.AddAsync(kino);
            return CreatedAtAction(nameof(GetKinoById), new { id = kino.KinoId }, kino);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kino>> GetKinoById(int id)
        {
            var kino = await _kinoRepository.GetByIdAsync(id);
            if (kino == null) return NotFound();
            return Ok(kino);
        }

        //Spezifisch
        [HttpPost("{kinoId}/saal")]
        public async Task<IActionResult> AddSaalMitReihenUndSitzen(int kinoId, [FromBody] SaalDto saalDto)
        {
            try
            {
                await _kinoRepository.AddSaalMitReihenUndSitzenAsync(kinoId, saalDto.Saal, saalDto.AnzahlReihen, saalDto.AnzahlSitzeProReihe);
                return Ok();
            }
            catch (Exception ex)
            {
                // Geeignete Fehlerbehandlung
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("saal/{saalId}")]
        public async Task<ActionResult> DeleteSaal(int saalId)
        {
            await _kinoRepository.DeleteSaalAsync(saalId);
            return NoContent();
        }

        [HttpPut("saal/{saalId}")]
        public async Task<IActionResult> UpdateSaalMitReihenUndSitzen(int saalId, [FromBody] SaalDto saalDto)
        {
            try
            {
                await _kinoRepository.UpdateSaalMitReihenUndSitzenAsync(saalId, saalDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{kinoId}/saele")]
        public async Task<IActionResult> GetKinoMitSaelen(int kinoId)
        {
            try
            {
                var kinoMitSaelenDto = await _kinoRepository.GetKinoMitSaelenAsync(kinoId);
                return Ok(kinoMitSaelenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


}
