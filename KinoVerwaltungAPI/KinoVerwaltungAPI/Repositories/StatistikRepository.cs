using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoVerwaltungAPI.Data;
using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;
using KinoVerwaltungAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KinoVerwaltungAPI.Repositories
{
    public class StatistikRepository : IStatistikRepository
    {
        private readonly ApplicationDbContext _context;

        public StatistikRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Belegung Statistik Methoden
        #region Belegung Statistik

        public async Task<BelegungDto> GetBelegungStatistikByKinoIdAsync(int kinoId)
        {
            //Kino holen
            var kino = await _context.Kinos
                .Where(k => k.KinoId == kinoId)
                .FirstOrDefaultAsync();
            if (kino == null)
            {
                return null;
            }


            //All vorführungen des Kinos holen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.Saal.KinoId == kinoId)
                .ToListAsync();
            if (vorführungen == null)
            {
                return null;
            }

            //Anzahl der Tickets von allen Vorführungen zählen
            var anzahlTickets = 0;
            foreach (var vorführung in vorführungen)
            {
                //Tickets von Vorführung holen
                var tickets = await _context.Tickets
                    .Where(t => t.VorführungId == vorführung.VorführungId)
                    .ToListAsync();

                foreach (var ticket in tickets)
                {
                    anzahlTickets++;
                }
            }

            //BelegungDto erstellen
            var belegung = new BelegungDto
            {
                Id = kinoId,
                AnzahlBesucher = anzahlTickets,
                Name = kino.Name,
                Datum = DateTime.Now,
                Type = "Kino"
            };

            return belegung;
        }


        public async Task<BelegungDto> GetBelegungStatistikBySaalIdAsync(int saalId)
        {
            //Saal holen
            var saal = await _context.Saele
                .Where(s => s.SaalId == saalId)
                .FirstOrDefaultAsync();
            if (saal == null)
            {
                return null;
            }


            //All vorführungen des Saals holen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.SaalId == saalId)
                .ToListAsync();
            if (vorführungen == null)
            {
                return null;
            }

            //Anzahl der Tickets von allen Vorführungen zählen
            var anzahlTickets = 0;
            foreach (var vorführung in vorführungen)
            {
                //Tickets von Vorführung holen
                var tickets = await _context.Tickets
                    .Where(t => t.VorführungId == vorführung.VorführungId)
                    .ToListAsync();

                foreach (var ticket in tickets)
                {
                    anzahlTickets++;
                }
            }

            //BelegungDto erstellen
            var belegung = new BelegungDto
            {
                Id = saalId,
                AnzahlBesucher = anzahlTickets,
                Name = saal.Name,
                Datum = DateTime.Now,
                Type = "Saal"
            };

            return belegung;
        }

        Task<IEnumerable<BelegungDto>> IStatistikRepository.GetBelegungStatistikByFilmIdAsync(int filmId)
        {
            throw new NotImplementedException();
        }

        

        Task<IEnumerable<BelegungDto>> IStatistikRepository.GetBelegungStatistikByVorführungIdAsync(int vorführungId)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Umsatz Statistik Methoden
        #region Umsatz Statistik

        public async Task<UmsatzDto> GetUmsatzStatistikByKinoIdAsync(int kinoId)
        {
            //Kino holen
            var kino = await _context.Kinos
                .Where(k => k.KinoId == kinoId)
                .FirstOrDefaultAsync();
            if (kino == null)
            {
                return null;
            }

            //All vorführungen des Kinos holen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.Saal.KinoId == kinoId)
                .ToListAsync();
            if (vorführungen == null)
            {
                return null;
            }

            //Umsatz von allen Vorführungen zählen
            decimal umsatz = 0;
            foreach (var vorführung in vorführungen)
            {
                //Tickets von Vorführung holen
                var tickets = await _context.Tickets
                    .Where(t => t.VorführungId == vorführung.VorführungId)
                    .ToListAsync();

                foreach (var ticket in tickets)
                {
                    umsatz += ticket.Preis;
                }
            }

            //UmsatzDto erstellen
            var umsatzDto = new UmsatzDto
            {
                Id = kinoId,
                Umsatz = umsatz,
                Name = kino.Name,
                Datum = DateTime.Now,
                Type = "Kino"
            };

            return umsatzDto;
        }

        public async Task<UmsatzDto> GetUmsatzStatistikBySaalIdAsync(int saalId)
        {
            //Saal holen
            var saal = await _context.Saele
                .Where(s => s.SaalId == saalId)
                .FirstOrDefaultAsync();
            if (saal == null)
            {
                return null;
            }

            //All vorführungen des Saals holen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.SaalId == saalId)
                .ToListAsync();
            if (vorführungen == null)
            {
                return null;
            }

            //Umsatz von allen Vorführungen zählen
            decimal umsatz = 0;
            foreach (var vorführung in vorführungen)
            {
                //Tickets von Vorführung holen
                var tickets = await _context.Tickets
                    .Where(t => t.VorführungId == vorführung.VorführungId)
                    .ToListAsync();

                foreach (var ticket in tickets)
                {
                    umsatz += ticket.Preis;
                }
            }

            //UmsatzDto erstellen
            var umsatzDto = new UmsatzDto
            {
                Id = saalId,
                Umsatz = umsatz,
                Name = saal.Name,
                Datum = DateTime.Now,
                Type = "Saal"
            };

            return umsatzDto;
        }

        public async Task<UmsatzDto> GetUmsatzStatistikByFilmIdAsync(int filmId)
        {
            //Film holen
            var film = await _context.Filme
                .Where(f => f.FilmId == filmId)
                .FirstOrDefaultAsync();
            if (film == null)
            {
                return null;
            }

            //All vorführungen des Films holen
            var vorführungen = await _context.Vorführungen
                .Where(v => v.FilmId == filmId)
                .ToListAsync();
            if (vorführungen == null) 
            {
                return null; 
            }

            //Umsatz von allen Vorführungen zählen
            decimal umsatz = 0;

            foreach (var vorführung in vorführungen)
            {
                //Tickets von Vorführung holen
                var tickets = await _context.Tickets
                    .Where(t => t.VorführungId == vorführung.VorführungId)
                    .ToListAsync();

                foreach (var ticket in tickets)
                {
                    umsatz += ticket.Preis;
                }
            }

            //UmsatzDto erstellen
            var umsatzDto = new UmsatzDto
            {
                Id = filmId,
                Umsatz = umsatz,
                Name = film.Titel,
                Datum = DateTime.Now,
                Type = "Film"
            };

            return umsatzDto;
        }

        public async Task<UmsatzDto> GetUmsatzStatistikByBenutzerIdAsync(int benutzerId)
        {
            //Benutzer holen
            var benutzer = await _context.Benutzer
                .Where(b => b.BenutzerId == benutzerId)
                .FirstOrDefaultAsync();
            if (benutzer == null)
            {
                return null;
            }

            //All Tickets des Benutzers holen
            var tickets = await _context.Tickets
                .Where(t => t.BenutzerId == benutzerId)
                .ToListAsync();

            //Umsatz von allen Tickets zählen
            decimal umsatz = 0;
            foreach (var ticket in tickets)
            {
                umsatz += ticket.Preis;
            }

            //UmsatzDto erstellen
            var umsatzDto = new UmsatzDto
            {
                Id = benutzerId,
                Umsatz = umsatz,
                Name = benutzer.Vorname + " " + benutzer.Nachname,
                Datum = DateTime.Now,
                Type = "Benutzer",
                ZusatzInformation = benutzer.Email
            };

            return umsatzDto;
        }

        Task<IEnumerable<UmsatzDto>> IStatistikRepository.GetUmsatzStatistikByVorführungIdAsync(int vorführungId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
