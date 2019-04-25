using System.Collections.Generic;
using Database;

namespace WebApi.DTOs.Bars
{
    /// <summary>
    /// Converter class for BarSimpleDto class. 
    /// </summary>
    public static class BarSimpleDtoConverterREMOVED
    {
        /// <summary>
        /// Converts a IEnumerable&lt;Bar&gt; to a List&lt;BarSimpleDto&gt;
        /// </summary>
        /// <param name="fromList">
        /// is a IEnumerable&lt;Bar&gt;
        /// </param>
        /// <returns>
        /// Returns a List&lt;BarSimpleDto&gt; equivalent of the parameter. 
        /// </returns>
        public static List<BarSimpleDto> ToDtoList(IEnumerable<Bar> fromList)
        {
            var dtoList = new List<BarSimpleDto>();
            foreach (var bar in fromList)
            {
                dtoList.Add(ToDto(bar));
            }

            return dtoList;
        }

        /// <summary>
        /// Private utility method used to convert from a Bar to a BarSimpleDto
        /// </summary>
        /// <param name="bar">
        /// is a Bar object. 
        /// </param>
        /// <returns>
        /// The BarSimpleDto equivalent to the Bar object. <para></para>
        /// The equivalent has the BarName, AvgRating, ShortDescription and Picture properties of the Bar object. 
        /// </returns>
        private static BarSimpleDto ToDto(Bar bar)
        {
            var barDTO = new BarSimpleDto()
            {
                BarName = bar.BarName,
                AvgRating = bar.AvgRating,
                ShortDescription = bar.ShortDescription,
                Image = bar.Image,
            };

            return barDTO;
        }
    }
}