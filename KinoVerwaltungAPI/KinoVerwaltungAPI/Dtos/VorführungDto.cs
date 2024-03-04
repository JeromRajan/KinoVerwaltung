namespace KinoVerwaltungAPI.Dtos
{
    public class VorführungDto
    {
        public int VorführungId { get; set; }

        public int SaalId { get; set; }

        public DateTime Datum { get; set; }

        public DateTime StartZeit { get; set; }
        public decimal Preis { get; set; }

        public int FilmId { get; set; }

        public string FilmTitel { get; set; }
        public string FilmBeschreibung { get; set; }
        public string FilmGenre { get; set; }

        public int FilmDauer { get; set; }

        public string FilmFSK { get; set; }

        public string FilmSprache { get; set; }

        public int AnzahlSitzplaetze { get; set; }

        public int AnzahlReservierungen { get; set; }

        public int AnzahlFreieSitzplaetze { get; set; }

    }
}
