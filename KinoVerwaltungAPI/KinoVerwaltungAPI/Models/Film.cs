using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        [Required]
        public string Titel { get; set; }
        [Required]
        public int Dauer { get; set; }
        public string? Beschreibung { get; set; }
        [Required]
        public string Altersfreigabe { get; set; }
        // Foreign keys
        public int GenreId { get; set; } 

        public int SpracheId { get; set; } 

        // Navigation properties
        public virtual Genre Genre { get; set; } 
        
        public virtual Sprache Sprache { get; set; } 
    }

}
