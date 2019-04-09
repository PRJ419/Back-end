﻿using System.Collections.Generic;
using Database;

namespace WebApi.DTOs.Bars
{
    public static class BarSimpleDtoConverter
    {
        public static List<BarSimpleDto> ToDtoList(IEnumerable<Bar> fromList)
        {
            var dtoList = new List<BarSimpleDto>();
            foreach (var bar in fromList)
            {
                dtoList.Add(ToDto(bar));
            }

            return dtoList;
        }

        private static BarSimpleDto ToDto(Bar bar)
        {
            var barDTO = new BarSimpleDto()
            {
                BarName = bar.BarName,
                AvgRating = bar.AvgRating,
                ShortDescription = bar.ShortDescription,
                Picture = bar.Picture,
            };

            return barDTO;
        }
    }
}