using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public int VorfuehrungId { get; set; }

        [Required]
        public int BenutzerId { get; set; }
        
        [Required]
        public decimal Preis { get; set; }

        // Fremdschlüsselbeziehungen

        [ForeignKey("VorfuehrungId")]
        public virtual Vorfuehrung Vorfuehrung { get; set; }

        [ForeignKey("BenutzerId")]
        public virtual Benutzer Benutzer { get; set; }

        [ForeignKey("SitzId")] 
        public Sitz Sitz { get; set; } 

    }
}
