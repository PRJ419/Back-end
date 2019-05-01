using System.ComponentModel.DataAnnotations;
using Database;

namespace LogonServer.DTOs.Bars
{
    public class BarDto
    {
        [MaxLength(150)]
        public string BarName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        public int AgeLimit { get; set; }

        [MaxLength(255)]
        public string Educations { get; set; }

        [MaxLength(500)]
        public string ShortDescription { get; set; }

        [MaxLength(2500)]
        public string LongDescription { get; set; }

        //[MaxLength(8)]
        [Required]
        public int CVR { get; set; }

        //[MaxLength(10)]
        public int PhoneNumber { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }
    }
}