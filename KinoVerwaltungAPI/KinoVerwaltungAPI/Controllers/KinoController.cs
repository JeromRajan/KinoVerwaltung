using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KinoController : ControllerBase
    {
        private readonly KinoService _kinoService;

        public KinoController(KinoService kinoService)
        {
            _kinoService = kinoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kino>>> GetAll()
        {
            return Ok(await _kinoService.GetAllKinosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kino>> Get(int id)
        {
            var kino = await _kinoService.GetKinoByIdAsync(id);
            if (kino == null)
            {
                return NotFound();
            }
            return Ok(kino);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Kino kino)
        {
            await _kinoService.AddKinoAsync(kino);
            return CreatedAtAction(nameof(Get), new { id = kino.KinoId }, kino);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Kino kino)
        {
            if (id != kino.KinoId)
            {
                return BadRequest();
            }
            await _kinoService.UpdateKinoAsync(kino);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _kinoService.DeleteKinoAsync(id);
            return NoContent();
        }
    }

}
