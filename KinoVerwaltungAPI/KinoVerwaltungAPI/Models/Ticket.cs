using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public int VorführungId { get; set; }

        [Required]
        public int BenutzerId { get; set; }

        [ForeignKey("VorführungId")]
        public virtual Vorführung Vorführung { get; set; }

        [ForeignKey("BenutzerId")]
        public virtual Benutzer Benutzer { get; set; }

        [Required]
        public decimal Preis { get; set; }

        // Weitere Eigenschaften wie Sitzplatz können hier hinzugefügt werden
    }
}
