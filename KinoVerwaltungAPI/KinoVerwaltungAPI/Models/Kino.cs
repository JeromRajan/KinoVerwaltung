namespace KinoVerwaltungAPI.Models
{
    public class Kino
    {
        public int KinoId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Saal> Saele { get; set; }
        public virtual Adresse Adresse { get; set; }
    }
}
