using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Sprache
    {
        public int SpracheId { get; set; }

        [Required]
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<Film>? Filme { get; set; } 
    }

}
