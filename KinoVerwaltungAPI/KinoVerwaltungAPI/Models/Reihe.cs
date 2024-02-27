using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Reihe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReiheId { get; set; }

        [Required]
        public int Nummer { get; set; }

        // Foreign Key to Saal
        public int SaalId { get; set; }
        public Saal Saal { get; set; }

        // Relationships
        public virtual ICollection<Sitz> Sitze { get; set; }

        // Constructor to initialize the collection properties
        public Reihe()
        {
            Sitze = new HashSet<Sitz>();
        }
    }
}
