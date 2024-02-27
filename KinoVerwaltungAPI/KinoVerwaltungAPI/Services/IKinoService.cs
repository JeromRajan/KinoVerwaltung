using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Services
{
    public interface IKinoService
    {
        Task<IEnumerable<Kino>> GetAllKinosAsync();
        Task<Kino> GetKinoByIdAsync(int kinoId);
        Task<Kino> CreateKinoAsync(Kino kino);
    }
}
