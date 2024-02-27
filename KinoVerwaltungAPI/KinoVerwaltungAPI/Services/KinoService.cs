using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Services
{
    public class KinoService
    {
        private readonly IRepository<Kino> _kinoRepository;

        public KinoService(IRepository<Kino> kinoRepository)
        {
            _kinoRepository = kinoRepository;
        }

        public async Task<IEnumerable<Kino>> GetAllKinosAsync()
        {
            return await _kinoRepository.GetAllAsync();
        }

        public async Task<Kino> GetKinoByIdAsync(int id)
        {
            return await _kinoRepository.GetByIdAsync(id);
        }

        public async Task AddKinoAsync(Kino kino)
        {
            await _kinoRepository.AddAsync(kino);
        }

        public async Task UpdateKinoAsync(Kino kino)
        {
            await _kinoRepository.UpdateAsync(kino);
        }

        public async Task DeleteKinoAsync(int id)
        {
            await _kinoRepository.DeleteAsync(id);
        }
    }

}
