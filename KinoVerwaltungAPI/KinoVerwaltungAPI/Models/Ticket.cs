namespace KinoVerwaltungAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public decimal Preis { get; set; }

        // Foreign keys
        public int VorführungId { get; set; } 
        public int SitzId { get; set; } 
        public int BenutzerId { get; set; }

        public int ZahlungsmethodeId { get; set; }

        // Navigation properties
        public virtual Vorführung Vorführung { get; set; } 
        
        public virtual Sitz Sitz { get; set; } 
        
        public virtual Benutzer Benutzer { get; set; }

        public virtual Zahlungsmethode Zahlungsmethode { get; set; }
    }

}
