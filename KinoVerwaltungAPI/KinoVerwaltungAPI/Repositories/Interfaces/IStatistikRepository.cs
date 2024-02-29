using KinoVerwaltungAPI.Dtos;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface IStatistikRepository
    {
   
        Task<BelegungDto> GetBelegungStatistikByKinoIdAsync(int kinoId);

        Task<BelegungDto> GetBelegungStatistikBySaalIdAsync(int saalId);

        Task<IEnumerable<BelegungDto>> GetBelegungStatistikByVorführungIdAsync(int vorführungId);

        Task<IEnumerable<BelegungDto>> GetBelegungStatistikByFilmIdAsync(int filmId);

        Task<UmsatzDto> GetUmsatzStatistikByKinoIdAsync(int kinoId);

        Task<UmsatzDto> GetUmsatzStatistikBySaalIdAsync(int saalId);

        Task<UmsatzDto> GetUmsatzStatistikByBenutzerIdAsync(int benutzerId);

        Task<UmsatzDto> GetUmsatzStatistikByFilmIdAsync(int filmId);

        Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikByVorführungIdAsync(int vorführungId);       

    }
}
