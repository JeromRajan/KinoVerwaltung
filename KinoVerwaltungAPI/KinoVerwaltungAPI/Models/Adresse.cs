using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Adresse
    {
        [Key]
        public int AdresseId { get; set; }

        [Required]
        [StringLength(255)]
        public string Strasse { get; set; }

        [Required]
        [StringLength(10)]
        public string Hausnummer { get; set; }

        [Required]
        [StringLength(5)]
        public string PLZ { get; set; }

        [Required]
        [StringLength(255)]
        public string Stadt { get; set; }

        [StringLength(255)]
        public string Land { get; set; }

        // Navigation properties
        public ICollection<Kino>? Kinos { get; set; }
    }
}
