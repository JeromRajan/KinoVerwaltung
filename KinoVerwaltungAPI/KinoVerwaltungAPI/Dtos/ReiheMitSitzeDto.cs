namespace KinoVerwaltungAPI.Dtos
{
    public class ReiheMitSitzeDto
    {
        public int ReiheId { get; set; }
        public List<SitzDto> Sitze { get; set; }
    }
}
