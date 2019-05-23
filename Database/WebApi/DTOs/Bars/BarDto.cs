using System.ComponentModel.DataAnnotations;
using Database;

namespace WebApi.DTOs.Bars
{
    /// <summary>
    /// A data transfer object version of the Bar object from the database model layer. <para></para>
    /// Incoming requests holding Dtos that fail to comply will be returned BadRequest(400).
    /// </summary>
    public class BarDto
    {
        /// <summary>
        /// BarName property which is required and must be 150 characters long maximum.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string BarName { get; set; }

        /// <summary>
        /// Address property which is required and must be 255 characters maximum length.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        /// <summary>
        /// Age limit property which is required. Is int? so that it is set to null if no value is supplied.
        /// If value is null ASP.NET Core vil return BadRequest(400). Would not be able to detect that no value was set if
        /// int? was not used, since the value would just be set to 0 then. 
        /// </summary>
        [Required]
        public int? AgeLimit { get; set; }

        /// <summary>
        /// Educations property. Maxlength of 255 characters.
        /// </summary>
        [MaxLength(255)]
        public string Educations { get; set; }

        /// <summary>
        /// Property holding a short description, with a max length of 500 characters.
        /// </summary>
        [MaxLength(500)]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description with max length of 2500 characters. 
        /// </summary>
        [MaxLength(2500)]
        public string LongDescription { get; set; }

        /// <summary>
        /// Required CVR property. is int? for same reasons stated above describing AgeLimit. 
        /// </summary>
        [Required]
        public int? CVR { get; set; }

        /// <summary>
        /// Property for phone number. 
        /// </summary>
        public int PhoneNumber { get; set; }

        /// <summary>
        /// Email with maxlength of 150 characters.
        /// </summary>
        [MaxLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Rating of the bar, value must be between 0.0 and 5.0
        /// </summary>
        [Range(0.0, 5.0)]
        public double AvgRating { get; set; }

        /// <summary>
        /// image string which is used to load pictures of the internet in the front-end. 
        /// </summary>
        public string Image { get; set; }
    }
}