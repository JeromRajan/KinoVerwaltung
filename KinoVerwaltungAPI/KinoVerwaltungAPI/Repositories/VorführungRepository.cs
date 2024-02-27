using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Repositories
{
    public class VorführungRepository : IVorführungRepository
    {
        private readonly ApplicationDbContext _context;

        public VorführungRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vorführung>> GetAllVorführungenAsync()
        {
            return await _context.Vorführungen.ToListAsync();
        }

        public async Task<Vorführung> GetVorführungByIdAsync(int VorführungId)
        {
            return await _context.Vorführungen.FindAsync(VorführungId);
        }

        public async Task AddVorführungAsync(Vorführung Vorführung)
        {
            _context.Vorführungen.Add(Vorführung);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVorführungAsync(Vorführung Vorführung)
        {
            _context.Vorführungen.Update(Vorführung);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVorführungAsync(int VorführungId)
        {
            var Vorführung = await _context.Vorführungen.FindAsync(VorführungId);
            if (Vorführung != null)
            {
                _context.Vorführungen.Remove(Vorführung);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Vorführung>> GetProgrammFürAktuelleWocheAsync()
        {
            var startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);
            return await _context.Vorführungen
                .Where(v => v.StartZeit >= startOfWeek && v.StartZeit < endOfWeek)
            .ToListAsync();
        }

        public async Task<IEnumerable<Vorführung>> GetProgrammFürHeuteAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return await _context.Vorführungen
                .Where(v => v.StartZeit >= today && v.StartZeit < tomorrow)
                .ToListAsync();
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
