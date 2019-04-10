
using System.Collections.Generic;

namespace WebApi.DTOs.BarEvent
{
    /// <summary>
    /// Static class with static converter functions. <para></para>
    /// Static class and functions enable calling the functions without
    /// allocating new memory every time for use. 
    /// </summary>
    public static class BarEventDtoConverter
    {
        /// <summary>
        /// Converts a BarEvent object to a BarEventDto object. 
        /// </summary>
        /// <param name="barEvent">
        /// is a BarEvent object
        /// </param>
        /// <returns>
        /// Returns a BarEventDto equivalent to the supplied BarEvent.
        /// Difference is the navigational properties of the BarEvent
        /// isn't present in the Dto version. 
        /// </returns>
        public static BarEventDto ToDto(Database.BarEvent barEvent)
        {
            var dto = new BarEventDto()
            {
                BarName = barEvent.BarName,
                EventName = barEvent.EventName,
                Date = barEvent.Date,
            };
            return dto;
        }

        /// <summary>
        /// Converts a BarEventDto to a BarEvent 
        /// </summary>
        /// <param name="dto">
        /// is a BarEventDto object. 
        /// </param>
        /// <returns>
        /// Returns a BarEvent equivalent to the supplied BarEventDto. 
        /// </returns>
        public static Database.BarEvent ToBarEvent(BarEventDto dto)
        {
            var barEvent = new Database.BarEvent
            {
                BarName = dto.BarName,
                EventName = dto.EventName,
                Date = dto.Date,
            };
            return barEvent; 
        }

        /// <summary>
        /// Converts a List&lt;BarEvent&gt; to a List&lt;BarEventDto&gt;
        /// </summary>
        /// <param name="barEvents">
        /// is a list of BarEvents
        /// </param>
        /// <returns>
        /// A list of BarEventDtos. 
        /// </returns>
        public static List<BarEventDto> ToDtoList(IEnumerable<Database.BarEvent> barEvents)
        {
            var dtoList = new List<BarEventDto>();
            foreach (var barEvent in barEvents)
            {
                dtoList.Add(ToDto(barEvent));
            }

            return dtoList;
        }

    }
}