namespace KinoVerwaltungAPI.Dtos
{
    public class AddTicketDto
    {
        public int BenutzerId { get; set; }
        public int VorführungId { get; set; }
        public int SitzId { get; set; }
        public int ZahlungsmethodeId { get; set; }
    }
}
