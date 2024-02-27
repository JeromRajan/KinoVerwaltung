using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Services
{
    public class KinoService : IKinoService
    {
        private readonly ApplicationDbContext _context;

        public KinoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kino>> GetAllKinosAsync()
        {
            return await _context.Kinos.ToListAsync();
        }

        public async Task<Kino> GetKinoByIdAsync(int kinoId)
        {
            return await _context.Kinos.FindAsync(kinoId);
        }

        public async Task<Kino> CreateKinoAsync(Kino kino)
        {
            _context.Kinos.Add(kino);
            await _context.SaveChangesAsync();
            return kino;
        }


    }
}
