namespace KinoVerwaltungAPI.Dtos
{
    public class VorführungSaalDto
    {
        public int VorführungId { get; set; }
        public int SaalId { get; set; }
        
        public List<VorführungSaalReiheDto> Reihen { get; set; }
    }
}
