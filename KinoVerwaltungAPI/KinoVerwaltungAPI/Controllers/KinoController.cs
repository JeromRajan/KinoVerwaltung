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
            var kinos = await _kinoService.GetAllKinosAsync();
            return Ok(kinos);
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

        // Additional endpoints for Create, Update, and Delete
    }
}
