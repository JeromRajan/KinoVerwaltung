using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Benutzer
    {
        [Key]
        public int BenutzerId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Vorname { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string TelefonNr { get; set; }


        [Required]
        [StringLength(255)]
        public string Password { get; set; }


        // Beziehung zu Tickets
        public virtual ICollection<Ticket> Tickets { get; set; }

        public Benutzer()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
