using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Film
    {
        [Key]
        public int FilmId { get; set; }

        [Required]
        [StringLength(255)]
        public string Titel { get; set; }

        [Required]
        public DateTime Erscheinungsdatum { get; set; }

        [StringLength(1024)]
        public string Beschreibung { get; set; }

        // Beziehung zu Vorführungen
        public virtual ICollection<Vorfuehrung> Vorführungen { get; set; }

        public Film()
        {
            Vorführungen = new HashSet<Vorfuehrung>();
        }
    }
}
