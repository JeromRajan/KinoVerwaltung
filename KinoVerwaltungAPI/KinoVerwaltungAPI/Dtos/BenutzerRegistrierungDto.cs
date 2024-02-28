namespace KinoVerwaltungAPI.Dtos
{
    public class BenutzerRegistrierungDto
    {
        public string Email { get; set; }
        public string Passwort { get; set; }
        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Telefon { get; set; }

        public int RolleId { get; set; }

        public AdresseDto Adresse { get; set; }
    }

}
