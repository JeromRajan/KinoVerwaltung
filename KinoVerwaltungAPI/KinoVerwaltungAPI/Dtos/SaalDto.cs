using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Dtos
{
    public class SaalDto
    {
        public Saal Saal { get; set; }
        public int AnzahlReihen { get; set; }
        public int AnzahlSitzeProReihe { get; set; }
    }
}
