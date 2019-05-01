using Database;
using WebApi.DTOs.Bars;

namespace LogonServer.DTOs.Bars
{
    /// <summary>
    /// Converter
    /// </summary>
    public static class BarDtoConverter
    {
        /// <summary>
        /// Yeeah boi
        /// </summary>
        /// <param name="bar"></param>
        /// <returns></returns>
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
            };
            return dto;
        }

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