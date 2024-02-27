using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Sitz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SitzId { get; set; }

        [Required]
        public int Nummer { get; set; }

        // Additional attributes like 'IstVipSitz' can be added here based on requirements

        // Foreign Key to Reihe
        public int ReiheId { get; set; }
        public Reihe Reihe { get; set; }
    }
}
