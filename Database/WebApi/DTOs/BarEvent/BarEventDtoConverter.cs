
using System.Collections.Generic;

namespace WebApi.DTOs.BarEvent
{
    public static class BarEventDtoConverter
    {
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