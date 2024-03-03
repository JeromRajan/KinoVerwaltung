namespace KinoVerwaltungAPI.Dtos
{
    public class SaalInfoDto
    {
        public int SaalId { get; set; }
        public string Name { get; set; }
        public int? Nummer { get; set; }
        public int KinoId { get; set; }

        public int AnzahlReihen { get; set; }

        public int AnzahlSitzPlaetzeProReihe { get; set; }

        public int AnzahlSitzplaetze { get; set; }
    }
}
