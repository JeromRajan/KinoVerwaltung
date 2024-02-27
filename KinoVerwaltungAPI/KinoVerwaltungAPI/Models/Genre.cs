using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<Film>? Filme { get; set; } 
    }
}
