

namespace KinoVerwaltungAPI.Dtos
{
    public class TicketByUserDto
    {
        public int TicketId { get; set; }
        
        public int SitzNummer { get; set; }

        public int SitzReihe { get; set; }

        public string FilmTitel { get; set; }

        public string Vorführungsdatum { get; set; }

        public string Vorführungszeit { get; set; }

        public string SaalName { get; set; }

        public string Zahlungsmethode { get; set; }

        public string Kinoname { get; set; }

        public string ReferenzNummer { get; set; }

        public decimal Preis { get; set; }

        public string TicketStatus { get; set; }

    }
}
