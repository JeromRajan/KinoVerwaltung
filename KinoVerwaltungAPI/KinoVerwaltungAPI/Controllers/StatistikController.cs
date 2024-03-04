using Microsoft.AspNetCore.Mvc;
using KinoVerwaltungAPI.Repositories.Interfaces;


namespace KinoVerwaltungAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistikController : Controller
    {
        private readonly IStatistikRepository _statistikRepository;

        public StatistikController(IStatistikRepository statistikRepository)
        {
            _statistikRepository = statistikRepository;
        }

        //Statistik-Belegung-Endpunkte
        #region Statistik-Belegung
        [HttpGet("belegung/kino/{kinoId}")]
        public async Task<IActionResult> GetBelegungStatistikByKinoId(int kinoId)
        {
            var belegungStatistik = await _statistikRepository.GetBelegungStatistikByKinoIdAsync(kinoId);
            return Ok(belegungStatistik);
        }

        [HttpGet("belegung/kinos")]
        public async Task<IActionResult> GetBelegungStatistikForKinos()
        {
            var belegungStatistik = await _statistikRepository.GetBelegungStatistikForKinosAsync();
            return Ok(belegungStatistik);
        }


        [HttpGet("belegung/saal/{saalId}")]
        public async Task<IActionResult> GetBelegungStatistikBySaalId(int saalId)
        {
            var belegungStatistik = await _statistikRepository.GetBelegungStatistikBySaalIdAsync(saalId);
            return Ok(belegungStatistik);
        }

        [HttpGet("belegung/saals/{kinoId}")]
        public async Task<IActionResult> GetBelegungStatistikForSaals(int kinoId)
        {
            var belegungStatistik = await _statistikRepository.GetBelegungStatistikForSaalsAsync(kinoId);
            return Ok(belegungStatistik);
        }

        #endregion

        //Statistik-Umsatz-Endpunkte
        #region Statistik-Umsatz

        [HttpGet("umsatz/kino/{kinoId}")]
        public async Task<IActionResult> GetUmsatzStatistikByKinoId(int kinoId)
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikByKinoIdAsync(kinoId);
            return Ok(umsatzStatistik);
        }

        [HttpGet("umsatz/saal/{saalId}")]
        public async Task<IActionResult> GetUmsatzStatistikBySaalId(int saalId)
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikBySaalIdAsync(saalId);
            return Ok(umsatzStatistik);
        }

        [HttpGet("umsatz/benutzer/{benutzerId}")]
        public async Task<IActionResult> GetUmsatzStatistikByBenutzerId(int benutzerId)
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikByBenutzerIdAsync(benutzerId);
            return Ok(umsatzStatistik);
        }

        [HttpGet("umsatz/film/{filmId}")]
        public async Task<IActionResult> GetUmsatzStatistikByFilmId(int filmId)
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikByFilmIdAsync(filmId);
            return Ok(umsatzStatistik);
        }

        [HttpGet("umsatz/films")]
        public async Task<IActionResult> GetUmsatzStatistikForFilms()
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikForFilmsAsync();
            return Ok(umsatzStatistik);
        }

        [HttpGet("umsatz/kinos")]
        public async Task<IActionResult> GetUmsatzStatistikForKinos()
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikForKinosAsync();
            return Ok(umsatzStatistik);
        }

        [HttpGet("umsatz/saals/{kinoId}")]
        public async Task<IActionResult> GetUmsatzStatistikForSaals(int kinoId)
        {
            var umsatzStatistik = await _statistikRepository.GetUmsatzStatistikForSaalsAsync(kinoId);
            return Ok(umsatzStatistik);
        }

        #endregion
    }
}
