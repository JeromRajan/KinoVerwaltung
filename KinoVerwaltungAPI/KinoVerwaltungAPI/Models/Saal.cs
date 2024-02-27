using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Saal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Nummer { get; set; }

        // Foreign Key to Kino
        public int KinoId { get; set; }
        public Kino Kino { get; set; }

        // Relationships
        public virtual ICollection<Reihe> Reihen { get; set; }
        public virtual ICollection<Vorfuehrung> Vorfuehrungen { get; set; }

        // Constructor to initialize the collection properties
        public Saal()
        {
            Reihen = new HashSet<Reihe>();
            Vorfuehrungen = new HashSet<Vorfuehrung>();
        }
    }
}
