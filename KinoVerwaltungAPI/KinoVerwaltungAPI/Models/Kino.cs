using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Kino
    {
        public int KinoId { get; set; }
        [Required]
        public string Name { get; set; }

        //Foreign keys
        public int AdressId { get; set; }

        //Navigation propertys
        public Adresse Adresse { get; set; }

        public ICollection<Saal>? Saele { get; set; }
    }
}
