namespace KinoVerwaltungAPI.Models
{
    public class Sprache
    {
        public int SpracheId { get; set; }
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<Film> Filme { get; set; } 
    }

}
