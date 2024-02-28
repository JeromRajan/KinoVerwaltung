using System.ComponentModel.DataAnnotations;

namespace KinoVerwaltungAPI.Models
{
    public class Mitgliederstatus
    {
        public int MitgliederstatusId { get; set; }
        [Required]
        public string StatusName { get; set; }
        public decimal? Rabatt { get; set; }
    }

}
