using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Barrepræsentant 
    {
        [MaxLength(50)]
        public string BrugerNavn { get; set; }

        [Required]
        [MaxLength(150)]
        public string Navn { get; set; }

        [MaxLength(150)]
        public string BarNavn { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } //Skal krypteres!

        public Bar Bar { get; set; }
    }
}