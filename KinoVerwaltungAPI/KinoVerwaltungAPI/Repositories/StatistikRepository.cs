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

        //GetBelegungStatistikForKinosAsync
        public async Task<IEnumerable<BelegungDto>> GetBelegungStatistikForKinosAsync()
        {
            //Alle Kinos holen
            var kinos = await _context.Kinos
                .ToListAsync();
            if (kinos == null)
            {
                return null; 
            }

            //Liste für BelegungDtos erstellen
            var belegungen = new List<BelegungDto>();

            //Für jedes Kino BelegungDto erstellen
            foreach (var kino in kinos)
            {
                //All vorführungen des Kinos holen
                var vorführungen = await _context.Vorführungen
                    .Where(v => v.Saal.KinoId == kino.KinoId)
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
                    Id = kino.KinoId,
                    AnzahlBesucher = anzahlTickets,
                    Name = kino.Name,
                    Datum = DateTime.Now,
                    Type = "Kino"
                };

                belegungen.Add(belegung);
            }

            return belegungen;
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

        public async Task<IEnumerable<BelegungDto>> GetBelegungStatistikForSaalsAsync(int kinoId)
        {
           //Alle Säle des Kinos holen
           var saele = await _context.Saele
                .Where(s => s.KinoId == kinoId)
                .ToListAsync();

            if (saele == null)
            {
                return null;
            }

            //Liste für BelegungDtos erstellen
            var belegungen = new List<BelegungDto>();

            //Für jeden Saal BelegungDto erstellen
            foreach (var saal in saele)
            {
                //All vorführungen des Saals holen
                var vorführungen = await _context.Vorführungen
                    .Where(v => v.SaalId == saal.SaalId)
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
                    Id = saal.SaalId,
                    AnzahlBesucher = anzahlTickets,
                    Name = saal.Name,
                    Datum = DateTime.Now,
                    Type = "Saal"
                };

                belegungen.Add(belegung);
            }

            return belegungen;
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

        //GetUmsatzStatistikForFilmsAsync
        public async Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikForFilmsAsync()
        {
            //Alle Filme holen
            var filme = await _context.Filme
                .ToListAsync();
            if (filme == null)
            {
                return null;
            }

            //Liste für UmsatzDtos erstellen
            var umsätze = new List<UmsatzDto>();

            //Für jeden Film UmsatzDto erstellen
            foreach (var film in filme)
            {
                //All vorführungen des Films holen
                var vorführungen = await _context.Vorführungen
                    .Where(v => v.FilmId == film.FilmId)
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
                    Id = film.FilmId,
                    Umsatz = umsatz,
                    Name = film.Titel,
                    Datum = DateTime.Now,
                    Type = "Film"
                };

                umsätze.Add(umsatzDto);
            }

            return umsätze;
        }

        //GetUmsatzStatistikForKinosAsync
        public async Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikForKinosAsync()
        {
            //Alle Kinos holen
            var kinos = await _context.Kinos
                .ToListAsync();
            if (kinos == null)
            {
                return null;
            }

            //Liste für UmsatzDtos erstellen
            var umsätze = new List<UmsatzDto>();

            //Für jedes Kino UmsatzDto erstellen
            foreach (var kino in kinos)
            {
                //All vorführungen des Kinos holen
                var vorführungen = await _context.Vorführungen
                    .Where(v => v.Saal.KinoId == kino.KinoId)
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
                    Id = kino.KinoId,
                    Umsatz = umsatz,
                    Name = kino.Name,
                    Datum = DateTime.Now,
                    Type = "Kino"
                };

                umsätze.Add(umsatzDto);
            }

            return umsätze;
        }
        
        //GetUmsatzStatistikForSaalsAsync
        public async Task<IEnumerable<UmsatzDto>> GetUmsatzStatistikForSaalsAsync(int kinoId)
        {
            //Alle Säle des Kinos holen
            var saele = await _context.Saele
                .Where(s => s.KinoId == kinoId)
                .ToListAsync();
            if (saele == null)
            {
                return null;
            }

            //Liste für UmsatzDtos erstellen
            var umsätze = new List<UmsatzDto>();

            //Für jeden Saal UmsatzDto erstellen
            foreach (var saal in saele)
            {
                //All vorführungen des Saals holen
                var vorführungen = await _context.Vorführungen
                    .Where(v => v.SaalId == saal.SaalId)
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
                    Id = saal.SaalId,
                    Umsatz = umsatz,
                    Name = saal.Name,
                    Datum = DateTime.Now,
                    Type = "Saal"
                };

                umsätze.Add(umsatzDto);
            }

            return umsätze;
        }

        #endregion
    }
}
