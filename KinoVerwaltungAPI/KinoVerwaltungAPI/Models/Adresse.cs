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

        // Optional: Weitere Eigenschaften wie Bundesland oder spezifische Anweisungen

        // Beziehung zu Kino, falls jedes Kino eine eindeutige Adresse hat
        // public virtual Kino Kino { get; set; }
    }
}
