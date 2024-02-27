using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Kino
    {
        public int KinoId { get; set; }
        [Required]
        public string Name { get; set; }
        //Foreign keys
        #region Foreign key
        public int AdressId { get; set; }
        #endregion

        //Navigation propertys
        public Adresse Adresse { get; set; }

        public ICollection<Saal>? Saele { get; set; }
    }
}
