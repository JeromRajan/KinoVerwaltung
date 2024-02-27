using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Benutzer
    {
        public int BenutzerId { get; set; }
        [Required]
        public string Vorname { get; set; }
        [Required]
        public string Nachname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefon { get; set; }
        [Required]
        public string Passwort { get; set; }

        // Foreign key
        public int AdresseId { get; set; } 

        public int RolleId { get; set; }

        // Navigation properties
        public virtual Adresse Adresse { get; set; } 
        public virtual ICollection<Ticket>? Tickets { get; set; }

        public virtual ICollection<Rolle> Rollen { get; set; }

        public virtual Rolle Rolle { get; set; }
        public virtual Mitgliederkarte Mitgliederkarte { get; set; }
    }
}
