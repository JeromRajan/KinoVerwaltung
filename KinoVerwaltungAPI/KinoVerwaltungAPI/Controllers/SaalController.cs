using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaalController : ControllerBase
    {
        private readonly ISaalService _saalService;

        public SaalController(ISaalService saalService)
        {
            _saalService = saalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Saal>>> GetAll()
        {
            return Ok(await _saalService.GetAllSäleAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Saal>> Get(int id)
        {
            var saal = await _saalService.GetSaalByIdAsync(id);
            if (saal == null)
            {
                return NotFound();
            }
            return Ok(saal);
        }

        // POST, PUT, DELETE endpoints to be implemented following the same structure

        // Additional actions can be added here based on requirements
    }
}
