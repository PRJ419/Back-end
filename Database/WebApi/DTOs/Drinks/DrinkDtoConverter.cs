using System.Collections.Generic;
using Database;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApi.DTOs;
namespace WebApi.DTOs.Drinks
{
    public class DrinkDtoConverter :  IDtoConverter<DrinkDto, Drink> //IDtoListConverter<DrinkDto, Drink>,
    {
        public static List<DrinkDto> ToDto(List<Drink> drinks)
        {
            var dtoList = new List<DrinkDto>();
            foreach (var drink in drinks)
            {
                var dto = new DrinkDto()
                {
                    BarName = drink.BarName,
                    DrinksName = drink.DrinksName,
                    Price = drink.Price
                };
                
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public Drink ToDto(DrinkDto fromObject)
        {
            throw new System.NotImplementedException();
        }
    }


}