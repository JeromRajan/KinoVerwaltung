using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KinoController : ControllerBase
    {
        private readonly IKinoService _kinoService;

        public KinoController(IKinoService kinoService)
        {
            _kinoService = kinoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kino>>> Get()
        {
            return Ok(await _kinoService.GetAllKinosAsync());
        }

        // Weitere Endpunkte
    }
}
