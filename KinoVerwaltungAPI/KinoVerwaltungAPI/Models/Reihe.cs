﻿namespace KinoVerwaltungAPI.Models
{
    public class Reihe
    {
        public int ReiheId { get; set; }
        public int Nummer { get; set; }

        // Foreign key
        public int SaalId { get; set; }

        // Navigation properties
        public virtual ICollection<Sitz> Sitze { get; set; } 
        
        public virtual Saal Saal { get; set; } 
    }
}
