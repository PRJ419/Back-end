using System.Collections.Generic;
using Database;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApi.DTOs;
namespace WebApi.DTOs.Drinks
{
    /// <summary>
    /// Converter class for DrinkDto and Drink. 
    /// </summary>
    public static class DrinkDtoConverter
    {

        /// <summary>
        /// Converts a IEnumerable&lt;Drink&gt; to a List&lt;DrinkDto&gt;
        /// </summary>
        /// <param name="fromList">
        /// is a IEnumerable&lt;Drink&gt;
        /// </param>
        /// <returns>
        /// Returns the List&lt;DrinkDto&gt; equivalent of the parameter. 
        /// </returns>
        public static List<DrinkDto> ToDtoList(IEnumerable<Drink> fromList)
        {
            List<DrinkDto> dtoList = new List<DrinkDto>();

            foreach (var drink in fromList)
            {
                dtoList.Add(ToDto(drink));
            }

            return dtoList;
        }

        /// <summary>
        /// Utility method for converting for Drink object to DrinkDto object. 
        /// </summary>
        /// <param name="fromObject">
        /// is a Drink object. 
        /// </param>
        /// <returns>
        /// DrinkDto equivalent of the parameter. 
        /// </returns>
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

        /// <summary>
        /// Converts from a DrinkDto object to a Drink object
        /// </summary>
        /// <param name="dto">
        /// is a DrinkDto
        /// </param>
        /// <returns>
        /// Returns Drink equivalent of the parameter. 
        /// </returns>
        public static Drink ToDrink(DrinkDto dto)
        {
            var drink = new Drink()
            {
                BarName = dto.BarName,
                DrinksName = dto.DrinksName,
                Price = dto.Price,
            };
            return drink;
        }

        
    }


}