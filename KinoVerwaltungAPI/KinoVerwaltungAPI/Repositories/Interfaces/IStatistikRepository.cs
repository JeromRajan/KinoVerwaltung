using KinoVerwaltungAPI.Dtos;

namespace KinoVerwaltungAPI.Repositories.Interfaces
{
    public interface IStatistikRepository
    {
   
        Task<BelegungDto> GetBelegungStatistikByKinoIdAsync(int kinoId);

        Task<IEnumerable<BelegungDto>> GetBelegungStatistikForKinosAsync();

        Task<BelegungDto> GetBelegungStatistikBySaalIdAsync(int saalId);

        Task<IEnumerable<BelegungDto>> GetBelegungStatistikForSaalsAsync(int kinoId);

        Task<UmsatzDto> GetUmsatzStatistikByKinoIdAsync(int kinoId);

        Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikForKinosAsync();

        Task<UmsatzDto> GetUmsatzStatistikBySaalIdAsync(int saalId);

        Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikForSaalsAsync(int kinoId);

        Task<UmsatzDto> GetUmsatzStatistikByBenutzerIdAsync(int benutzerId);

        Task<UmsatzDto> GetUmsatzStatistikByFilmIdAsync(int filmId);

        Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikForFilmsAsync();


    }
}
