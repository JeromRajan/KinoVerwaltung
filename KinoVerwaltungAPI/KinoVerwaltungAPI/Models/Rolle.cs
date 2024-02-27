using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Rolle
    {
        public int RolleId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
