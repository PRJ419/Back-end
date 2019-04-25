using Database;

namespace WebApi.DTOs.Bars
{
    /// <summary>
    /// Converter for BarDto objects. <para></para>
    /// Objects are converted before they are sent. 
    /// </summary>
    public static class BarDtoConverterREMOVED
    {
        /// <summary>
        /// Converts a Bar object (from database model layer) to a BarDto object. <para></para>
        /// </summary>
        /// <param name="bar">
        /// is a Bar object. 
        /// </param>
        /// <returns>
        /// Returns a BarDto equivalent of the supplied Bar object. 
        /// </returns>
        public static BarDto ToDto(Bar bar)
        {
            var dto = new BarDto()
            {
                Address = bar.Address,
                AvgRating = bar.AvgRating,
                AgeLimit = bar.AgeLimit,
                BarName = bar.BarName,
                CVR = bar.CVR,
                Educations = bar.Educations,
                Email = bar.Email,
                LongDescription = bar.LongDescription,
                PhoneNumber = bar.PhoneNumber,
                ShortDescription = bar.ShortDescription,
                Image = bar.Image,
            };
            return dto;
        }

        /// <summary>
        /// Converts a BarDto object to a Bar object. 
        /// </summary>
        /// <param name="dto">
        /// is a BarDto object. 
        /// </param>
        /// <returns>
        /// Returns a Bar object equivalent of the supplied BarDto. 
        /// </returns>
        public static Bar ToBar(BarDto dto)
        {
            var bar = new Bar()
            {
                Address = dto.Address,
                AvgRating = dto.AvgRating,
                AgeLimit = dto.AgeLimit,
                BarName = dto.BarName,
                CVR = dto.CVR,
                Educations = dto.Educations,
                Email = dto.Email,
                LongDescription = dto.LongDescription,
                PhoneNumber = dto.PhoneNumber,
                ShortDescription = dto.ShortDescription,
                Image =  dto.Image,
                Drinks = null, 
                Coupons = null, 
                BarEvents = null, 
                Reviews = null, 
                BarRepresentatives = null,
            };
            return bar;
        }
    }
}