namespace KinoVerwaltungAPI.Dtos
{
    public class UmsatzDto
    {
        public int Id { get; set; } 
        public decimal Umsatz { get; set; }
        public DateTime Datum { get; set; }
        public string Name { get; set; } 

        public string? Beschreibung { get; set; }

        public string Type { get; set; }
        
        public string? ZusatzInformation { get; set; }
    }
}
