using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Saal
    {
        public int SaalId { get; set; }
        [Required]
        public string Name { get; set; }

        public int? Nummer { get; set; }
        // Foreign key
        public int KinoId { get; set; }

        // Navigation properties
        public virtual ICollection<Reihe>? Reihen { get; set; } 
        public virtual Kino Kino { get; set; } 
    }
}
