using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class Barrepresentative 
    {
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string BarName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; } //Skal krypteres!

        public Bar Bar { get; set; }
    }
}