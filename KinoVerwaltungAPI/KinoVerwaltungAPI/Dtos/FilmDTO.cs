namespace KinoVerwaltungAPI.Dtos
{
    public class FilmDTO
    {
        public int FilmId { get; set; }
        public string Titel { get; set; }
        public int Dauer { get; set; }
        public string Beschreibung { get; set; }
        public string Altersfreigabe { get; set; }
        public string GenreName { get; set; }

        public string SpracheName { get; set; }

    }
}
