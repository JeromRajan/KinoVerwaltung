using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        // Beziehung zu Filmen, falls Filme Genres haben
        public virtual ICollection<Film> Filme { get; set; }

        public Genre()
        {
            Filme = new HashSet<Film>();
        }
    }
}
