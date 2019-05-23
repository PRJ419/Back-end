using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.BarRepresentative
{
    /// <summary>
    /// Data transfer object equivalent of the BarRepresentative saved in database. 
    /// Incoming requests holding Dtos that fail to comply will be returned BadRequest(400).
    /// </summary>
    public class BarRepresentativeDto
    {
        /// <summary>
        /// Property for saving the username of a representative. This has a max length of 50 and is required.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }


        /// <summary>
        /// Property for the name of the representative. This is required and has a max length of 150.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }


        /// <summary>
        /// Property for the foreign key to the bars bar name. Has a max length of 150 just like in bar.
        /// </summary>
        [MaxLength(150)]
        public string BarName { get; set; }
    }
}