using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Mitgliederkarte
    {
        public int MitgliederkarteId { get; set; }

        public decimal? VerfügbareBetrag { get; set; }
        public DateTime? Ablaufdatum { get; set; }
        [Required]
        public string IdentifikationsNummer { get; set; }

        [Required]
        public int AnzahlGekaufterTickets { get; set; }
        
        // Foreign keys
        public int? BenutzerId { get; set; } 

        public int MitgliederstatusId { get; set; }
        public int? ZahlungsmethodeId { get; set; }

        // Navigation property
        public virtual Benutzer? Benutzer { get; set; }

        public virtual Mitgliederstatus? Mitgliederstatus { get; set; }

        public virtual Zahlungsmethode? Zahlungsmethode { get; set; }

    }

}
