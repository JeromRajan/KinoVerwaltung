﻿using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required]
        public decimal Preis { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string ReferenzNummer { get; set; }

        // Foreign keys
        public int VorführungId { get; set; } 
        public int SitzId { get; set; } 
        public int BenutzerId { get; set; }
        public int ZahlungsmethodeId { get; set; }

        // Navigation properties
        public virtual Vorführung? Vorführung { get; set; } 
        
        public virtual Sitz? Sitz { get; set; } 
        
        public virtual Benutzer? Benutzer { get; set; }

        public virtual Zahlungsmethode? Zahlungsmethode { get; set; }
    }

}
