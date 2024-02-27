using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Services
{
    public class SaalService : ISaalService
    {
        private readonly ApplicationDbContext _context;

        public SaalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Saal>> GetAllSäleAsync()
        {
            return await _context.Saele.ToListAsync();
        }

        public async Task<Saal> GetSaalByIdAsync(int saalId)
        {
            return await _context.Saele.FindAsync(saalId);
        }

        public async Task<Saal> CreateSaalAsync(Saal saal)
        {
            _context.Saele.Add(saal);
            await _context.SaveChangesAsync();
            return saal;
        }

        public async Task<Saal> UpdateSaalAsync(int saalId, Saal saal)
        {
            _context.Entry(saal).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return saal;
        }

        public async Task DeleteSaalAsync(int saalId)
        {
            var saal = await _context.Saele.FindAsync(saalId);
            if (saal != null)
            {
                _context.Saele.Remove(saal);
                await _context.SaveChangesAsync();
            }
        }
        // Additional methods implementation based on requirements
    }
}
