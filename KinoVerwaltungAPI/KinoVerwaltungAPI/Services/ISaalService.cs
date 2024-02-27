using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Services
{
    public interface ISaalService
    {
        Task<IEnumerable<Saal>> GetAllSäleAsync();
        Task<Saal> GetSaalByIdAsync(int saalId);
        Task<Saal> CreateSaalAsync(Saal saal);
        Task<Saal> UpdateSaalAsync(int saalId, Saal saal);
        Task DeleteSaalAsync(int saalId);
        // Additional methods based on requirements can be defined here
    }
}