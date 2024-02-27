using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Dtos;


namespace KinoVerwaltungAPI.Services
{
    public class KinoRepository : IKinoRepository
    {
        private readonly ApplicationDbContext _context;

        public KinoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Kino> GetByIdAsync(int id)
        {
            return await _context.Kinos.FindAsync(id);
        }

        public async Task<IEnumerable<Kino>> GetAllAsync()
        {
            return await _context.Kinos.ToListAsync();
        }

        public async Task AddAsync(Kino kino)
        {
            _context.Kinos.Add(kino);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Kino kino)
        {
            _context.Kinos.Update(kino);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var kino = await _context.Kinos.FindAsync(id);
            if (kino != null)
            {
                _context.Kinos.Remove(kino);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddSaalMitReihenUndSitzenAsync(int kinoId, Saal saal, int anzahlReihen, int anzahlSitzeProReihe)
        {
            var kino = await _context.Kinos.FindAsync(kinoId);
            if (kino == null) throw new Exception("Kino nicht gefunden.");

            saal.KinoId = kinoId;
            saal.Reihen = new List<Reihe>();

            for (int i = 0; i < anzahlReihen; i++)
            {
                var reihe = new Reihe { Sitze = new List<Sitz>() };
                reihe.Nummer = i;
                for (int j = 0; j < anzahlSitzeProReihe; j++)
                {
                    var newSitz = new Sitz();
                    newSitz.Nummer = j;
                    reihe.Sitze.Add(newSitz);
                }
                saal.Reihen.Add(reihe);
            }

            _context.Saele.Add(saal);
            await _context.SaveChangesAsync();
        }


        // Spezifische Methode
        public async Task<Kino> GetKinoWithSaeleAsync(int kinoId)
        {
            return await _context.Kinos
                .Include(k => k.Saele)
                    .ThenInclude(s => s.Reihen)
                        .ThenInclude(r => r.Sitze)
                .FirstOrDefaultAsync(k => k.KinoId == kinoId);
        }

        public async Task UpdateSaalAsync(int saalId, Saal aktualisierterSaal)
        {
            var saal = await _context.Saele
                .Include(s => s.Reihen)
                    .ThenInclude(r => r.Sitze)
                .FirstOrDefaultAsync(s => s.SaalId == saalId);

            if (saal == null) throw new Exception("Saal nicht gefunden.");

            // Aktualisieren Sie hier die Eigenschaften von `saal` mit denen von `aktualisierterSaal`
            // Beachten Sie, dass dies komplex sein kann, je nachdem, wie Sie Änderungen an Reihen/Sitzen behandeln möchten

            _context.Saele.Update(saal);
            await _context.SaveChangesAsync();
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

        public async Task UpdateSaalMitReihenUndSitzenAsync(int saalId, SaalDto saalDto)
        {
            var saal = await _context.Saele.Include(s => s.Reihen).ThenInclude(r => r.Sitze).FirstOrDefaultAsync(s => s.SaalId == saalId);

            if (saal == null) throw new Exception("Saal nicht gefunden.");

            //Zuweisung Saalname
            saal.Name = saalDto.Saal.Name;

            // Entfernen aller bestehenden Reihen (und deren Sitze) für diesen Saal
            _context.Reihen.RemoveRange(saal.Reihen);
            await _context.SaveChangesAsync();

            // Neuaufbau der Reihen und Sitze basierend auf dem DTO
            saal.Reihen = new List<Reihe>();
            for (int i = 0; i < saalDto.AnzahlReihen; i++)
            {
                var reihe = new Reihe { Sitze = new List<Sitz>() };
                for (int j = 0; j < saalDto.AnzahlSitzeProReihe; j++)
                {
                    reihe.Sitze.Add(new Sitz());
                }
                saal.Reihen.Add(reihe);
            }

            _context.Saele.Update(saal);
            await _context.SaveChangesAsync();
        }

        public async Task<KinoMitSaelenDto> GetKinoMitSaelenAsync(int kinoId)
        {
            var kino = await _context.Kinos
                .Include(k => k.Saele)
                    .ThenInclude(s => s.Reihen)
                        .ThenInclude(r => r.Sitze)
                .FirstOrDefaultAsync(k => k.KinoId == kinoId);

            if (kino == null) throw new Exception("Kino nicht gefunden.");

            // Transformieren Sie die Daten in das KinoMitSaelenDto
            var kinoMitSaelenDto = new KinoMitSaelenDto
            {
                KinoId = kino.KinoId,
                KinoName = kino.Name,
                Saele = kino.Saele.Select(s => new SaalMitReiheDto
                {
                    SaalId = s.SaalId,
                    SaalName = s.Name,
                    Reihen = s.Reihen.Select(r => new ReiheMitSitzeDto
                    {
                        ReiheId = r.ReiheId,
                        Sitze = r.Sitze.Select(sitz => new SitzDto
                        {
                            SitzId = sitz.SitzId
                            // Weitere Eigenschaften nach Bedarf
                        }).ToList()
                    }).ToList()
                }).ToList()
            };

            return kinoMitSaelenDto;
        }


    }

}
