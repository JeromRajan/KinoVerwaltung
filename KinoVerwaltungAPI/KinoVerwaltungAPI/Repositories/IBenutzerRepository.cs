using KinoVerwaltungAPI.Dtos;
using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories
{
    public interface IBenutzerRepository
    {
        Task<Rolle> AddRolleAsync(Rolle rolle);
        Task<Benutzer> RegistrierenAsync(Benutzer benutzer, string passwort, Adresse adresse);
        Task<Benutzer> LoginAsync(string email, string passwort);
        Task<Benutzer> GetBenutzerDatenAsync(int benutzerId);
        Task UpdateBenutzerAsync(Benutzer benutzer, AdresseDto adresseDto);
    }
}
