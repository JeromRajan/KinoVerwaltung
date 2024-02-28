namespace KinoVerwaltungAPI.Dtos
{
    public class BenutzerUpdateDto
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public string Telefon {  get; set; }

        public AdresseDto Adresse { get; set; }
    }
}
