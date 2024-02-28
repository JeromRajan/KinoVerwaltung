namespace KinoVerwaltungAPI.Dtos
{
    public class VorführungSaalReiheDto
    {
        public int ReiheId { get; set; }

        public int ReiheNummer { get; set; }

        public List<VorführungSaalReiheSitzDto> Sitze { get; set; }

    }
}
