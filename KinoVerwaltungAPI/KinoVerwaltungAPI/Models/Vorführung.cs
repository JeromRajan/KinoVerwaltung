using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace KinoVerwaltungAPI.Models
{
    public class Vorführung
    {
        public int VorführungId { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        public decimal Preis { get; set; }
        [Required]
        public DateTime StartZeit { get; set; }
 
        public DateTime? EndZeit { get; set; }

        // Foreign keys
        public int SaalId { get; set; } 
        public int FilmId { get; set; } 
        // Navigation properties
        public virtual Saal Saal { get; set; } 
        
        public virtual Film Film { get; set; } 
        public virtual ICollection<Ticket> Tickets { get; set; } 
    }
}
