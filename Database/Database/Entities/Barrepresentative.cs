using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class BarRepresentative 
    {
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string BarName { get; set; }

        public Bar Bar { get; set; }
    }
}