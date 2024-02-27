using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Rolle
    {
        [Key]
        public int RolleId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        // Optional: Beschreibung oder andere Eigenschaften

        // Beziehung zu Benutzern, falls es eine Benutzer-Rollen-Beziehung gibt
        public virtual ICollection<Benutzer> Benutzer { get; set; }

        public Rolle()
        {
            Benutzer = new HashSet<Benutzer>();
        }
    }

}
