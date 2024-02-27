namespace KinoVerwaltungAPI.Dtos
{
    public class SaalMitReiheDto
    {
        public int SaalId { get; set; }
        public string SaalName { get; set; }
        public List<ReiheMitSitzeDto> Reihen { get; set; }
    }
}
