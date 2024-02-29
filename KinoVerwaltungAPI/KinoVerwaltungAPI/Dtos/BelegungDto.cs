namespace KinoVerwaltungAPI.Dtos
{
    public class BelegungDto
    {
        public int Id { get; set; } 
        public DateTime Datum { get; set; }
        public int AnzahlBesucher { get; set; }
        public string Name { get; set; } 

        public string? Beschreibung { get; set; }

        public string Type { get; set; }

        public string? ZusatzInformation { get; set; }
    }
}
