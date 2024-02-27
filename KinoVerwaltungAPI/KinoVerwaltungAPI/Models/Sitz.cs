namespace KinoVerwaltungAPI.Models
{
    public class Sitz
    {
        public int SitzId { get; set; }
        public int Nummer { get; set; }

        // Foreign key
        public int ReiheId { get; set; }

        // Navigation property
        public virtual Reihe Reihe { get; set; } 
    }
}
