namespace KinoVerwaltungAPI.Dtos
{
    public class ReiheMitSitzeDto
    {
        public int ReiheId { get; set; }

        public int ReiheNum { get; set; }
        public List<SitzDto> Sitze { get; set; }
    }
}
