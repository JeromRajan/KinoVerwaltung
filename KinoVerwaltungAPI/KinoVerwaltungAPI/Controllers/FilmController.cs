using Microsoft.AspNetCore.Mvc;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories;
using System.Threading.Tasks;

namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;

        //Film-Enpunkte
        #region Film
        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilme()
        {
            var filme = await _filmRepository.GetAllFilmeAsync();
            return Ok(filme);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilmById(int id)
        {
            var film = await _filmRepository.GetFilmByIdAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost]
        public async Task<IActionResult> AddFilm([FromBody] Film film)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _filmRepository.AddFilmAsync(film);
            return CreatedAtAction(nameof(GetFilmById), new { id = film.FilmId }, film);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] Film film)
        {
            if (id != film.FilmId)
            {
                return BadRequest();
            }

            try
            {
                await _filmRepository.UpdateFilmAsync(film);
            }
            catch
            {
                if (await _filmRepository.GetFilmByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            try
            {
                await _filmRepository.DeleteFilmAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        // Genre-Endpunkte
        #region Genre
        [HttpGet("genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _filmRepository.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpPost("genres")]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            await _filmRepository.AddGenreAsync(genre);
            return CreatedAtAction("GetAllGenres", new { id = genre.GenreId }, genre);
        }

        [HttpPut("genres/{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return BadRequest();
            }

            await _filmRepository.UpdateGenreAsync(genre);
            return NoContent();
        }

        [HttpDelete("genres/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _filmRepository.DeleteGenreAsync(id);
            return NoContent();
        }
        #endregion

        // Sprachen-Endpunkte
        #region Sprachen
        [HttpGet("sprachen")]
        public async Task<IActionResult> GetAllSprachen()
        {
            var sprachen = await _filmRepository.GetAllSprachenAsync();
            return Ok(sprachen);
        }

        [HttpPost("sprachen")]
        public async Task<IActionResult> AddSprache([FromBody] Sprache sprache)
        {
            await _filmRepository.AddSpracheAsync(sprache);
            return CreatedAtAction("GetAllSprachen", new { id = sprache.SpracheId }, sprache);
        }

        [HttpPut("sprachen/{id}")]
        public async Task<IActionResult> UpdateSprache(int id, [FromBody] Sprache sprache)
        {
            if (id != sprache.SpracheId)
            {
                return BadRequest();
            }

            await _filmRepository.UpdateSpracheAsync(sprache);
            return NoContent();
        }

        [HttpDelete("sprachen/{id}")]
        public async Task<IActionResult> DeleteSprache(int id)
        {
            await _filmRepository.DeleteSpracheAsync(id);
            return NoContent();
        }
        #endregion
    }
}
