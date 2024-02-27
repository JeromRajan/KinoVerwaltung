using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Vorfuehrung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VorfuehrungId { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        [Required]
        public decimal Preis { get; set; }

        // Additional properties like Startzeit, Endzeit can be added here

        // Foreign Key to Saal
        public int SaalId { get; set; }
        public Saal Saal { get; set; }

        // Foreign Key to Film
        public int FilmId { get; set; }
        public Film Film { get; set; }

        // Additional relationships like Tickets can be added here
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
