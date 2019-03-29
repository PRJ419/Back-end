using System.Collections.Generic;
using Database;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApi.DTOs;
namespace WebApi.DTOs.Drinks
{
    public static class DrinkDtoConverter
    {

        public static List<DrinkDto> ToDtoList(List<Drink> fromList)
        {
            List<DrinkDto> dtoList = new List<DrinkDto>();

            foreach (var drink in fromList)
            {
                dtoList.Add(ToDto(drink));
            }

            return dtoList;
        }

        private static DrinkDto ToDto(Drink fromObject)
        {
            var dto = new DrinkDto()
            {
                BarName = fromObject.BarName,
                DrinksName = fromObject.DrinksName,
                Price = fromObject.Price,
            };
            return dto;
        }

        
    }


}