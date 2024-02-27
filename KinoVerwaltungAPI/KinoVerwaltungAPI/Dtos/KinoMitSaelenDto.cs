namespace KinoVerwaltungAPI.Dtos
{
    public class KinoMitSaelenDto
    {
        public int KinoId { get; set; }
        public string KinoName { get; set; }
        public List<SaalMitReiheDto> Saele { get; set; }
    }
}
