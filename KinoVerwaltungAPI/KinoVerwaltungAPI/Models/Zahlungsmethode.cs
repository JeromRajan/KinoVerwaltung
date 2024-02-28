using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Zahlungsmethode
    {
        public int ZahlungsmethodeId { get; set; }

        [Required]
        public string Name { get; set; }


        // Navigation properties

        public virtual ICollection<Ticket>? Tickets { get; set; } 
        public virtual ICollection<Mitgliederkarte>? Mitgliederkarten { get; set; } 
    }
}
